using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarframeInventory.Common.Abstractions.Data.Repository;
using WarframeInventory.Common.Abstractions.Data.Repository.Factory;
using WarframeInventory.Common.Infrastructure.Data.Context;
using WarframeInventory.Common.Infrastructure.Data.Repository;
using WarframeInventory.Common.Infrastructure.Data.Repository.Factory;

namespace WarframeInventory.Common.Infrastructure.Data;

public static class ConfigureDatabases
{
	public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration config)
	{
		services.AddTransient(typeof(ICachedWarframeDataRepository<>), typeof(CachedWarframeDataRepository<>));
		services.AddTransient(typeof(ICachedWarframeDataRepositoryFactory<>), typeof(CachedWarframeDataRepositoryFactory<>));

		services.AddDbContextFactory<CachedWarframeDataContext>(options =>
			options.UseSqlServer(config.GetConnectionString("CachedWarframeData"))
			.EnableDetailedErrors()
			.EnableSensitiveDataLogging()
		);

		return services;
	}
}