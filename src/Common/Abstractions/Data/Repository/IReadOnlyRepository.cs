using Ardalis.Specification;

namespace WarframeInventory.Common.Abstractions.Data.Repository;

public interface IReadOnlyRepository<T> : IReadRepositoryBase<T>, IDisposable, IAsyncDisposable where T : class;