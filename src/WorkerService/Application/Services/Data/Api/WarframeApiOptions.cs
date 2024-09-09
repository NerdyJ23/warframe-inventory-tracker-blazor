namespace WarframeInventory.WorkerService.Application.Services.Data.Api;

public sealed record WarframeApiOptions
{
	public string BaseUri { get; set; } = "https://content.warframe.com/PublicExport/";
	public string Mainfest { get; set; } = "index_en.txt.lzma";
}