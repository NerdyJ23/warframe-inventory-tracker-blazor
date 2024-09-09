using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;
using WarframeInventory.Common.Infrastructure.Data.Context;

namespace WarframeInventory.Common.Infrastructure.Data.Repository.Factory;

internal sealed class CachedWarframeDataRepositoryFactory<T> : WritableRepositoryFactory<CachedWarframeDataContext, T>, ICachedWarframeDataRepositoryFactory<T> where T : class
{
	public CachedWarframeDataRepositoryFactory(IDbContextFactory<CachedWarframeDataContext> factory) : base(factory)
	{
	}
}