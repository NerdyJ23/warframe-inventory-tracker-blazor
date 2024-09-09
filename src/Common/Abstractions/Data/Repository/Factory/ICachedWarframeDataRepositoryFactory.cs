namespace WarframeInventory.Common.Abstractions.Data.Repository.Factory;

public interface ICachedWarframeDataRepositoryFactory<T> : IWritableRepositoryFactory<T> where T : class;