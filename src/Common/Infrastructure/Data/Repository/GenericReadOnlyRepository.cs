using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Abstractions.Data.Repository;

namespace WarframeInventory.Common.Infrastructure.Data.Repository;

internal class GenericReadOnlyRepository<TContext, TEntity> : IReadOnlyRepository<TEntity>
where TEntity : class
where TContext : DbContext
{
	private bool disposedValue;
	protected TContext _context;

	public GenericReadOnlyRepository(TContext context)
	{
		_context = context;
	}

	public Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).AnyAsync(cancellationToken);

	public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().AnyAsync(cancellationToken);

	public IAsyncEnumerable<TEntity> AsAsyncEnumerable(ISpecification<TEntity> specification) =>
		_context.Set<TEntity>().WithSpecification(specification).AsAsyncEnumerable();

	public Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).CountAsync(cancellationToken);

	public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().CountAsync(cancellationToken);

	public ValueTask DisposeAsync()
	{
		Dispose(true);
		return ValueTask.CompletedTask;
	}

	public Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	public async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
		await _context.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);

	public Task<TEntity?> GetBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	public Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().ToListAsync(cancellationToken);

	public Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).ToListAsync(cancellationToken);

	public Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).ToListAsync(cancellationToken);

	public Task<TEntity?> SingleOrDefaultAsync(ISingleResultSpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default) =>
		_context.Set<TEntity>().WithSpecification(specification).FirstOrDefaultAsync(cancellationToken);

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_context.Dispose();
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}