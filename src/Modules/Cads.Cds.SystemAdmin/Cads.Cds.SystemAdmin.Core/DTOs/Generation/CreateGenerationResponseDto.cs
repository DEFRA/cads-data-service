using System.Text.Json.Serialization;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Generation;

public class CreateGenerationResponseDto
{
    [JsonPropertyName("filename")]
    public required string FileName { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonPropertyName("businessKeys")]
    public required IEnumerable<decimal> BusinessKeys { get; set; }
}