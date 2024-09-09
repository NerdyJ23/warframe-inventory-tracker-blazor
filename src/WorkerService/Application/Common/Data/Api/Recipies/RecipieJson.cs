using System.Text.Json.Serialization;

namespace WarframeInventory.WorkerService.Application.Common.Data.Api.Recipies;

public sealed record RecipieJson
{
	[JsonPropertyName("uniqueName")] public required string UniqueName { get; set; }
	[JsonPropertyName("resultType")] public required string ResultType { get; set; }
	[JsonPropertyName("primeSellingPrice")] public int? DucatsPrice { get; set; }
}