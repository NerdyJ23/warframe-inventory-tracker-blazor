using System.Text.Json.Serialization;

namespace WarframeInventory.WorkerService.Application.Common.Data.Api.Warframes;

public sealed record WarframeResponseJson
{
	[JsonPropertyName("ExportWarframes")] public ICollection<WarframeJson> Warframes { get; set; } = [];
}