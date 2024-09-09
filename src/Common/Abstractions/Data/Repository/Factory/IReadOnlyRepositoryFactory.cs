namespace WarframeInventory.Common.Abstractions.Data.Repository.Factory;

public interface IReadOnlyRepositoryFactory<T> where T : class
{
	public IReadOnlyRepository<T> CreateReadOnlyRepository();
	public Task<IReadOnlyRepository<T>> CreateReadOnlyRepositoryAsync(CancellationToken cancellationToken = default);
}