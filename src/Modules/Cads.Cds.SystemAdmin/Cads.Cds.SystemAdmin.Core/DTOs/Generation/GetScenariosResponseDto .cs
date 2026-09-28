using System.Text.Json.Serialization;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class GetScenariosResponseDto
{
    [JsonPropertyName("scenarios")]
    public required IEnumerable<ScenarioDto> Scenarios { get; set; }
}