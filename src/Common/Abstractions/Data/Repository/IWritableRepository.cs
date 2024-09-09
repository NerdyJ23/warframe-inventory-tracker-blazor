using Ardalis.Specification;

namespace WarframeInventory.Common.Abstractions.Data.Repository;

public interface IWritableRepository<T> : IRepositoryBase<T>, IDisposable, IAsyncDisposable where T : class;