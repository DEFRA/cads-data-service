using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs;

public class LocationDto
{
    [JsonPropertyName("lid_identifier")]
    public string? LidIdentifier { get; set; }

    [JsonPropertyName("lid_full_identifier")]
    public string? LidFullIdentifier { get; set; }
}