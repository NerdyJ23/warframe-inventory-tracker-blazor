using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Abstractions.Data.Repository;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;

namespace WarframeInventory.Common.Infrastructure.Data.Repository.Factory;

internal class ReadOnlyRepositoryFactory<TContext, TEntity> : IReadOnlyRepositoryFactory<TEntity>
where TEntity : class
where TContext : DbContext
{
	protected readonly IDbContextFactory<TContext> _factory;

	public ReadOnlyRepositoryFactory(IDbContextFactory<TContext> factory)
	{
		_factory = factory;
	}

	public IReadOnlyRepository<TEntity> CreateReadOnlyRepository() =>
		new GenericReadOnlyRepository<TContext, TEntity>(_factory.CreateDbContext());

	public async Task<IReadOnlyRepository<TEntity>> CreateReadOnlyRepositoryAsync(CancellationToken cancellationToken = default) =>
		new GenericReadOnlyRepository<TContext, TEntity>(await _factory.CreateDbContextAsync(cancellationToken));
}