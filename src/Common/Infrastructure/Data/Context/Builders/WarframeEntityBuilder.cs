using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarframeInventory.Common.Domain.Entities.CachedData;

namespace WarframeInventory.Common.Infrastructure.Data.Context.Builders;

internal sealed class WarframeEntityBuilder : IEntityTypeConfiguration<Warframe>
{
	public void Configure(EntityTypeBuilder<Warframe> builder)
	{
		builder.ToTable("Warframes");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).UseIdentityColumn();
	}
}