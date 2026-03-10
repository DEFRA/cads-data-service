using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs;

public class DestinationDetailsDto : Amsl2Dto
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}