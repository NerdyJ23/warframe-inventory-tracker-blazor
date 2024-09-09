using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Abstractions.Data.Repository;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;

namespace WarframeInventory.Common.Infrastructure.Data.Repository.Factory;

internal class WritableRepositoryFactory<TContext, T> : ReadOnlyRepositoryFactory<TContext, T>, IWritableRepositoryFactory<T>
where T : class
where TContext : DbContext
{
	public WritableRepositoryFactory(IDbContextFactory<TContext> factory) : base(factory) { }

	public IWritableRepository<T> CreateRepository() =>
		new GenericWritableRepository<TContext, T>(_factory.CreateDbContext());

	public async Task<IWritableRepository<T>> CreateRepositoryAsync(CancellationToken cancellationToken = default) =>
		new GenericWritableRepository<TContext, T>(await _factory.CreateDbContextAsync(cancellationToken));
}