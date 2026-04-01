using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class AnimalDetail
{
    [JsonPropertyName("AnimalID")]
    public string? AnimalId { get; set; }

    [JsonPropertyName("AnimalIDType")]
    public string? AnimalIdType { get; set; }

    [JsonPropertyName("AnimalIDTransponderHex")]
    public string? AnimalIdTransponderHex { get; set; }

    [JsonPropertyName("AnimalIDTransponderISO")]
    public string? AnimalIdTransponderIso { get; set; }

    [JsonPropertyName("TagIssueNumber")]
    public string? TagIssueNumber { get; set; }

    [JsonPropertyName("DOA")]
    public bool Doa { get; set; }
}