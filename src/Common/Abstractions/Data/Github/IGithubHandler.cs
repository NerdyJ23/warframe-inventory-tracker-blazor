using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using WarframeInventory.Common.Domain.Entities.JsonData;

namespace WarframeInventory.Common.Abstractions.Data.Github;

public interface IGithubHandler
{
	//Pulls the info from warframe-inventory repo and returns file names with hashes
	public Task<ICollection<FileHistory>> GitPull(CancellationToken cancellationToken = default);
}