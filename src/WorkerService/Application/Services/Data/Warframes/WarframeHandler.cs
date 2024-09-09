using WarframeInventory.Common.Abstractions.Data.Cached.Warframes;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;
using WarframeInventory.WorkerService.Application.Specifications.Warframes;
using Entities = WarframeInventory.Common.Domain.Entities;

namespace WarframeInventory.WorkerService.Application.Services.Data.Warframes;

internal sealed class WarframeHandler : ICachedWarframeWriter
{
	private readonly ICachedWarframeDataRepositoryFactory<Entities.CachedData.Warframe> _warframeFactory;
	private readonly ILogger<WarframeHandler> _logger;

	public WarframeHandler(ICachedWarframeDataRepositoryFactory<Entities.CachedData.Warframe> warframeFactory, ILogger<WarframeHandler> logger)
	{
		_warframeFactory = warframeFactory;
		_logger = logger;
	}

	public async Task UpsertWarframes(ICollection<Entities.CachedData.Warframe> warframes, CancellationToken cancellationToken = default)
	{
		await using var context = await _warframeFactory.CreateRepositoryAsync(cancellationToken);
		var cachedWarframes = (await context.ListAsync(new LoadCachedWarframesByUniqueName([.. warframes.Select(x => x.UniqueName)]), cancellationToken)).Select(x => x.UniqueName);

		_logger.LogDebug("Found {CachedCount} cached warframes to update", cachedWarframes.ToList().Count);

		if (_logger.IsEnabled(LogLevel.Trace))
		{
			foreach (var uniqueName in cachedWarframes)
			{
				_logger.LogTrace("Updating {UniqueName}", uniqueName);
			}

			foreach (var warframe in warframes.Where(x => !cachedWarframes.Any(y => y == x.UniqueName)))
			{
				_logger.LogTrace("Adding warframe {UniqueName}", warframe.UniqueName);
			}
		}

		await context.UpdateRangeAsync(warframes.Where(x => cachedWarframes.Any(y => y == x.UniqueName)), cancellationToken);
		await context.AddRangeAsync(warframes.Where(x => !cachedWarframes.Any(y => y == x.UniqueName)), cancellationToken);
		await context.SaveChangesAsync(cancellationToken);
	}
}