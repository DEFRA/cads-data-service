using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs;

public class HoldingDto : UkvDto
{
    [JsonPropertyName("cph")]
    public required string Cph { get; set; }
}