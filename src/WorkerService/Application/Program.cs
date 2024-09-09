using Microsoft.Extensions.Options;
using WarframeInventory.WorkerService.Application.Services;
using WarframeInventory.WorkerService.Application.Services.Data.Api;
using WarframeInventory.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Infrastructure.Data.Context;
using WarframeInventory.WorkerService.Application.Jobs;
using Serilog;

var builder = Host.CreateDefaultBuilder(args)
.ConfigureServices((context, services) =>
	services
	.AddServices(context.Configuration)
	.AddDatabases(context.Configuration)
)
.UseSerilog((host, loggerConfig) =>
{
	loggerConfig.ReadFrom.Configuration(host.Configuration);
});

var host = builder.Build();

var dbFactory = host.Services.GetRequiredService<IDbContextFactory<CachedWarframeDataContext>>();
await using var context = await dbFactory.CreateDbContextAsync();
// await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

var job = host.Services.GetRequiredService<SyncWarframeDataJob>();
await job.StartAsync(CancellationToken.None);

await host.RunAsync();
