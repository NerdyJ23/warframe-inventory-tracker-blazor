using Ardalis.Specification;
using WarframeInventory.Common.Domain.Entities.JsonData;

namespace WarframeInventory.WorkerService.Application.Specifications.Api;

internal sealed class GetApiEndpointByName : Specification<ApiUrlHistory>
{
	public GetApiEndpointByName(string name)
	{
		Query.Where(x => x.Name == name);
	}
}