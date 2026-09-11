using System.Text.Json.Serialization;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class CreateGenerationResponseDto
{
    [JsonPropertyName("type")]
    public required string FileName { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonPropertyName("businessKeys")]
    public required IEnumerable<long> BusinessKeys { get; set; }
}