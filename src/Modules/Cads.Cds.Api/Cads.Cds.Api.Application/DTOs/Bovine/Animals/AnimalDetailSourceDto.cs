using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Bovine.Animals;

public class AnimalDetailSourceDto
{
    [JsonPropertyName("system")]
    public string? System { get; set; }

    [JsonPropertyName("schema")]
    public string? Schema { get; set; }

    [JsonPropertyName("schemaVersion")]
    public string? SchemaVersion { get; set; }
}