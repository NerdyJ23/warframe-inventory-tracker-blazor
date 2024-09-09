namespace WarframeInventory.Common.Abstractions.Data.Repository.Factory;

public interface IWritableRepositoryFactory<T> : IReadOnlyRepositoryFactory<T> where T : class
{
	public IWritableRepository<T> CreateRepository();
	public Task<IWritableRepository<T>> CreateRepositoryAsync(CancellationToken cancellationToken = default);
}