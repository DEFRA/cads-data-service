using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class AnimalDetail
{
    [JsonPropertyName("AnimalID")]
    public string? AnimalID { get; set; }

    [JsonPropertyName("AnimalIDType")]
    public string? AnimalIDType { get; set; }

    [JsonPropertyName("AnimalIDTransponderHex")]
    public string? AnimalIDTransponderHex { get; set; }

    [JsonPropertyName("AnimalIDTransponderISO")]
    public string? AnimalIDTransponderISO { get; set; }

    [JsonPropertyName("TagIssueNumber")]
    public string? TagIssueNumber { get; set; }

    [JsonPropertyName("DOA")]
    public bool DOA { get; set; }
}