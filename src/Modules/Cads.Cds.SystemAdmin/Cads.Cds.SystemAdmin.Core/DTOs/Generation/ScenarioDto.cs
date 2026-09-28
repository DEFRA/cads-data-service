using System.Text.Json.Serialization;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class ScenarioDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("tableName")]
    public required string TableName { get; set; }
}