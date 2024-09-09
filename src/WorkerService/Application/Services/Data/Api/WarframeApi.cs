using System.Net.Http.Json;
using EasyCompressor;
using Microsoft.Extensions.Options;
using WarframeInventory.Common.Domain.Entities.JsonData;
using Entities = WarframeInventory.Common.Domain.Entities;
using WarframeInventory.WorkerService.Application.Common.Data.Api.Warframes;
using WarframeInventory.Common.Abstractions.Data.Warframe;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text;

namespace WarframeInventory.WorkerService.Application.Services.Data.Api;

internal sealed partial class WarframeApi : IWarframeApi
{
	private readonly WarframeApiOptions _options;
	private readonly HttpClient _client;
	private readonly ILogger<WarframeApi> _logger;

	public WarframeApi(IOptions<WarframeApiOptions> options, HttpClient client, ILogger<WarframeApi> logger)
	{
		_options = options.Value;
		_client = client;
		_logger = logger;
	}

	public async Task<ICollection<ApiUrlHistory>> FetchManifestData(CancellationToken cancellationToken = default)
	{
		var response = await _client.GetAsync($"{_options.BaseUri}{_options.Mainfest}", cancellationToken);

		if (response.IsSuccessStatusCode)
		{
			// var stream = new MemoryStream();
			// await response.Content.CopyToAsync(stream, cancellationToken);
			Console.WriteLine("== RAW ==");
			Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
			Console.WriteLine("== DECOMPRESSED ==");

			var decompressor = new LZMACompressor();
			var manifestStream = new MemoryStream();
			await decompressor.DecompressAsync(await response.Content.ReadAsStreamAsync(cancellationToken), manifestStream, cancellationToken);

			var manifest = new string([.. manifestStream.ToArray().Select(x => (char)x)]);
			Console.WriteLine(manifest);

			var items = manifest.Split('\n');

			var apiUrls = items.Where(x => !x.Contains("Manifest"))
			.Select(x => new ApiUrlHistory()
			{
				Name = x[x.IndexOf("Export")..x.IndexOf('_')].Replace("Export", ""),
				Hash = x[x.IndexOf('!')..x.Length],
				Uri = x,
				UpdatedAt = DateTimeOffset.Now
			});

			foreach (var api in apiUrls)
			{
				Console.WriteLine("Name: {0}", api.Name);
				Console.WriteLine("Hash: {0}", api.Hash);
				Console.WriteLine("Uri: {0}", api.Uri);
			}

			return [.. apiUrls];
		}

		return [];
	}

	public async Task<ICollection<Entities.CachedData.Warframe>> FetchWarframeData(ApiUrlHistory endpoint, CancellationToken cancellationToken = default)
	{
		if (!endpoint.Name.Contains("Warframe"))
		{
			throw new InvalidOperationException($"Given Uri must be for Warframes, was {endpoint.Name}");
		}

		var response = await _client.GetAsync($"{_options.BaseUri}/Manifest/{endpoint.Uri}", cancellationToken);

		if (response.IsSuccessStatusCode)
		{
			var warframes = new List<Entities.CachedData.Warframe>();

			var content = await response.Content.ReadAsStringAsync(cancellationToken);
			// var utf8Content = Encoding.UTF8.GetString(await content.ReadAllBytesAsync(cancellationToken));

			var stream = new MemoryStream([.. content.Replace("\r", "").Replace("\n", "").Replace("’", "\'").Select(x => (byte)x)]);
			var jsonContent = await JsonSerializer.DeserializeAsync<WarframeResponseJson>(stream, cancellationToken: cancellationToken);

			// Utf8JsonReader reader = new Utf8JsonReader(await stream.ReadAllBytesAsync(cancellationToken));


			// foreach (JsonElement warframe in jsonDocument.RootElement.GetProperty("ExportWarframes").EnumerateArray())
			// {
			// 	_logger.LogDebug("Warframe: {Warframe}", warframe);
			// 	if (!warframe.TryGetProperty("uniqueName", out JsonElement uniqueName))
			// 	{
			// 		_logger.LogWarning("Failed to get warframe unique name");
			// 		continue;
			// 	}

			// 	if (!warframe.TryGetProperty("name", out JsonElement name))
			// 	{
			// 		_logger.LogWarning("Failed to get warframe display name");
			// 	}

			// 	warframes.Add(new Entities.CachedData.Warframe()
			// 	{
			// 		UniqueName = uniqueName.GetString()!,
			// 		DisplayName = name.GetString()!
			// 	});
			// }
			// Console.WriteLine(new string([.. (await stream.ReadAllBytesAsync(cancellationToken)).Select(x => (char)x)]));
			// stream.Position = 0;
			// var deserializer = new Utf8JsonReader();
			// var jsonContent =

			if (jsonContent is null)
			{
				_logger.LogWarning("Failed to parse json content for Warframe API");
				return [];
			}

			warframes.AddRange(jsonContent.Warframes.Select(x => new Entities.CachedData.Warframe()
			{
				UniqueName = x.UniqueName,
				DisplayName = x.DisplayName
			}));

			return warframes;
		}

		return [];
	}
}