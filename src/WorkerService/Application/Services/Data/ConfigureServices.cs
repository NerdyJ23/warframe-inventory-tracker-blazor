using Microsoft.Extensions.Options;
using WarframeInventory.Common.Abstractions.Data.Cached.Warframes;
using WarframeInventory.Common.Abstractions.Data.Warframe;
using WarframeInventory.WorkerService.Application.Jobs;
using WarframeInventory.WorkerService.Application.Services.Data.Api;
using WarframeInventory.WorkerService.Application.Services.Data.Warframes;

namespace WarframeInventory.WorkerService.Application.Services;

public static class ConfigureServices
{
	public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddHttpClient();
		services.AddTransient<ICachedWarframeWriter, WarframeHandler>();
		services.AddTransient<IWarframeApi, WarframeApi>();
		services.AddTransient<WarframeCachedDataHandler>();

		services.AddTransient<SyncWarframeDataJob>();

		services.Configure<IOptions<WarframeApiOptions>>(config.GetSection("Options:WarframeApi"));
		return services;
	}
}