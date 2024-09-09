using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Infrastructure.Data.Context.Builders;

namespace WarframeInventory.Common.Infrastructure.Data.Context;

public sealed class CachedWarframeDataContext : DbContext
{
	public CachedWarframeDataContext() { }

	public CachedWarframeDataContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new WarframeEntityBuilder());
		modelBuilder.ApplyConfiguration(new ApiUrlHistoryBuilder());
	}
}