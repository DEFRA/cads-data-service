using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Core.DTOs.Common;

public class IngestionDTO
{
    [JsonPropertyName("ingestionId")]
    public string IngestionId { get; set; } = Guid.NewGuid().ToString();
    [JsonPropertyName("receivedAt")]
    public DateTimeOffset ReceivedAt { get; set; } = DateTimeOffset.UtcNow;
    [JsonPropertyName("recordCount")]
    public int RecordCount { get; set; }
}