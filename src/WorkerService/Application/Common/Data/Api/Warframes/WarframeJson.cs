using System.Text.Json.Serialization;

namespace WarframeInventory.WorkerService.Application.Common.Data.Api.Warframes;

public sealed record WarframeJson
{
	[JsonPropertyName("uniqueName")] public string UniqueName { get; set; } = "";
	[JsonPropertyName("name")] public string DisplayName { get; set; } = "";
}