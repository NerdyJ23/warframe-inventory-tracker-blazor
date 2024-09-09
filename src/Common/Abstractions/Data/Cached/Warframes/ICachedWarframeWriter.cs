using Entities = WarframeInventory.Common.Domain.Entities;

namespace WarframeInventory.Common.Abstractions.Data.Cached.Warframes;

public interface ICachedWarframeWriter
{
	public Task UpsertWarframes(ICollection<Entities.CachedData.Warframe> warframes, CancellationToken cancellationToken = default);
}