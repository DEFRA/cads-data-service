using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalHoldingDto
{
    [JsonPropertyName("cph")]
    public string? Cph { get; set; }

    [JsonPropertyName("animals")]
    public IEnumerable<AnimalOnCphDto> Animals { get; set; } = [];
}