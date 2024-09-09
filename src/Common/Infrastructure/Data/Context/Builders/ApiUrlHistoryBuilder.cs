using Microsoft.EntityFrameworkCore;
using WarframeInventory.Common.Domain.Entities.JsonData;

namespace WarframeInventory.Common.Infrastructure.Data.Context.Builders;

internal sealed class ApiUrlHistoryBuilder : IEntityTypeConfiguration<ApiUrlHistory>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApiUrlHistory> builder)
	{
		builder.ToTable("ApiUriData");
		builder.HasKey(x => x.Uri);
	}
}