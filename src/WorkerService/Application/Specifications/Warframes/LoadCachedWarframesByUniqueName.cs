using Ardalis.Specification;
using WarframeInventory.Common.Domain.Entities.CachedData;

namespace WarframeInventory.WorkerService.Application.Specifications.Warframes;

internal sealed class LoadCachedWarframesByUniqueName : Specification<Warframe>
{
	public LoadCachedWarframesByUniqueName(string[] names)
	{
		Query.Where(x => names.Any(y => y == x.UniqueName));
	}

	public LoadCachedWarframesByUniqueName(string name)
	{
		Query.Where(x => x.UniqueName == name);
	}
}