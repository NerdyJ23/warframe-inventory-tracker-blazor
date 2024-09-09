namespace WarframeInventory.Common.Abstractions.Data.Repository;

public interface ICachedWarframeDataRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class;