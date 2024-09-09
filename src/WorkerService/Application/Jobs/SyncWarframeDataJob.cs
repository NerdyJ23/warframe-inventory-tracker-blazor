
using System.Diagnostics;
using WarframeInventory.Common.Abstractions.Data.Warframe;
using WarframeInventory.WorkerService.Application.Services.Data.Api;

namespace WarframeInventory.WorkerService.Application.Jobs;

internal sealed class SyncWarframeDataJob : IHostedService
{
	private readonly ILogger<SyncWarframeDataJob> _logger;
	private readonly IWarframeApi _api;
	private readonly WarframeCachedDataHandler _dataHandler;

	public SyncWarframeDataJob(ILogger<SyncWarframeDataJob> logger, IWarframeApi api, WarframeCachedDataHandler dataHandler)
	{
		_logger = logger;
		_api = api;
		_dataHandler = dataHandler;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogDebug("Starting Job {Name} at {DateTime}", nameof(SyncWarframeDataJob), DateTimeOffset.Now);
		var endpoints = await _dataHandler.DataNeedsUpdate(cancellationToken);
		var timer = Stopwatch.StartNew();

		foreach (var endpoint in endpoints)
		{
			_logger.LogDebug("Updating {EndpointName}", endpoint.Name);
			await _dataHandler.UpdateCachedData(endpoint, cancellationToken);
		}

		timer.Stop();
		_logger.LogDebug("Updated {EndpointCount} in {Time}ms", endpoints.Count, timer.ElapsedMilliseconds);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}