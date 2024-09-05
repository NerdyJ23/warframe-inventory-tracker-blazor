using System;

namespace WarframeInventory.Common.Domain.Entities.JsonData;

public sealed class FileHistory
{
	public required string FileName { get; set; }
	public required string FileHash { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}