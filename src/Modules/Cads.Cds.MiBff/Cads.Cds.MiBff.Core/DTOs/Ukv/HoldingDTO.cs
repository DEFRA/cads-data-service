using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Ukv;

public class HoldingDto : UkvDto
{
    [JsonPropertyName("cph")]
    public required string Cph { get; set; }
}