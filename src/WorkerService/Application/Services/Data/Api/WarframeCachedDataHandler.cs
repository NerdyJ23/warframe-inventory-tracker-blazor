using WarframeInventory.Common.Domain.Entities.JsonData;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;
using WarframeInventory.WorkerService.Application.Specifications.Api;
using WarframeInventory.Common.Abstractions.Data.Cached.Warframes;
using WarframeInventory.Common.Abstractions.Data.Warframe;

namespace WarframeInventory.WorkerService.Application.Services.Data.Api;

internal sealed class WarframeCachedDataHandler
{
	private readonly ILogger<WarframeCachedDataHandler> _logger;
	private readonly IWarframeApi _api;
	private readonly ICachedWarframeDataRepositoryFactory<ApiUrlHistory> _factory;
	private readonly ICachedWarframeWriter _warframeWriter;

	public WarframeCachedDataHandler(ILogger<WarframeCachedDataHandler> logger, IWarframeApi api, ICachedWarframeDataRepositoryFactory<ApiUrlHistory> factory, ICachedWarframeWriter warframeWriter)
	{
		_logger = logger;
		_api = api;
		_factory = factory;
		_warframeWriter = warframeWriter;
	}

	public async Task<ICollection<ApiUrlHistory>> DataNeedsUpdate(CancellationToken cancellationToken = default)
	{
		var endpoints = await _api.FetchManifestData(cancellationToken);

		if (endpoints.Count == 0)
		{
			throw new InvalidDataException("Somehow returned 0 endpoints from api fetch");
		}

		await using var context = await _factory.CreateReadOnlyRepositoryAsync(cancellationToken);
		var endpointsToUpdate = new List<ApiUrlHistory>();

		foreach (var endpoint in endpoints)
		{
			var cachedEndpoint = await context.FirstOrDefaultAsync(new GetApiEndpointByName(endpoint.Name), cancellationToken);

			if (cachedEndpoint is null || cachedEndpoint.Hash != endpoint.Hash)
			{
				endpointsToUpdate.Add(endpoint);
			}
		}

		return endpointsToUpdate;
	}


	public async Task UpdateCachedData(ApiUrlHistory endpoint, CancellationToken cancellationToken = default)
	{
		await using var context = await _factory.CreateRepositoryAsync(cancellationToken);

		if (endpoint.Name == "Warframes")
		{
			_logger.LogTrace("Updating Warframes");
			await _warframeWriter.UpsertWarframes(await _api.FetchWarframeData(endpoint, cancellationToken), cancellationToken);
			await context.AddAsync(endpoint, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return;
		}
	}
}