using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs;

public class DepartureDetailsDto : Amsl2Dto
{
    [JsonPropertyName("cph")]
    public required string Cph { get; set; }
}