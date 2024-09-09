using System;

namespace WarframeInventory.Common.Domain.Entities.JsonData;

public sealed class ApiUrlHistory
{
	public required string Name { get; set; }
	public required string Hash { get; set; }
	public required string Uri { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}