using WarframeInventory.Common.Abstractions.Data.Repository;
using WarframeInventory.Common.Infrastructure.Data.Context;

namespace WarframeInventory.Common.Infrastructure.Data.Repository;

internal sealed class CachedWarframeDataRepository<TEntity> : GenericWritableRepository<CachedWarframeDataContext, TEntity>, ICachedWarframeDataRepository<TEntity>
where TEntity : class
{
	public CachedWarframeDataRepository(CachedWarframeDataContext context) : base(context) { }
}