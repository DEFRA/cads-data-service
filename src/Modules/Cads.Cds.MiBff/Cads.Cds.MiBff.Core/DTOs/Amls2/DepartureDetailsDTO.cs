using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Amls2;

public class DepartureDetailsDto : Amsl2Dto
{
    [JsonPropertyName("cph")]
    public required string Cph { get; set; }
}