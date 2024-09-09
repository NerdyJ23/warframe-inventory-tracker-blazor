using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WarframeInventory.Common.Abstractions.Data.Repository;

namespace WarframeInventory.Common.Infrastructure.Data.Repository;

internal class GenericWritableRepository<TContext, TEntity> : GenericReadOnlyRepository<TContext, TEntity>, IWritableRepository<TEntity>
where TEntity : class
where TContext : DbContext
{
	private bool disposedValue;

	public GenericWritableRepository(TContext context) : base(context)
	{
		_context = context;
	}

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
		(await _context.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;

	public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
		return entities;
	}

	public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		_context.Set<TEntity>().Remove(entity);
		return Task.CompletedTask;
	}

	public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		_context.Set<TEntity>().RemoveRange(entities);
		return Task.CompletedTask;
	}

	public async Task DeleteRangeAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		var items = await _context.Set<TEntity>().WithSpecification(specification).ToListAsync(cancellationToken);
		_context.RemoveRange(items);
	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
		_context.SaveChangesAsync(cancellationToken);

	public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		_context.Set<TEntity>().Update(entity);
		return Task.CompletedTask;
	}

	public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		_context.Set<TEntity>().UpdateRange(entities);
		return Task.CompletedTask;
	}
}