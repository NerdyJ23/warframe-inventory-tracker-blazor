using WarframeInventory.Common.Domain.Entities.JsonData;
using Entities = WarframeInventory.Common.Domain.Entities;

namespace WarframeInventory.Common.Abstractions.Data.Warframe;

public interface IWarframeApi
{
	public Task<ICollection<ApiUrlHistory>> FetchManifestData(CancellationToken cancellationToken = default);
	public Task<ICollection<Entities.CachedData.Warframe>> FetchWarframeData(ApiUrlHistory endpoint, CancellationToken cancellationToken = default);
}