namespace WarframeInventory.Common.Domain.Entities.CachedData;

public sealed class Warframe
{
	public int Id { get; set; }
	public required string UniqueName { get; set; }
	public required string DisplayName { get; set; }
}