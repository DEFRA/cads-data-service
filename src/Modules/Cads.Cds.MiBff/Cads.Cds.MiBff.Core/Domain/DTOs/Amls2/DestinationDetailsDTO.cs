using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

public class DestinationDetailsDto : Amsl2Dto
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}