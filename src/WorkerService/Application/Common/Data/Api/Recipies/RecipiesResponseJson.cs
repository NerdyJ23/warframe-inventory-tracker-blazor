using System.Text.Json.Serialization;

namespace WarframeInventory.WorkerService.Application.Common.Data.Api.Recipies;

public sealed record RecipiesResponseJson
{
	[JsonPropertyName("ExportRecipies")] public ICollection<RecipieJson> Recipies { get; set; } = [];
}