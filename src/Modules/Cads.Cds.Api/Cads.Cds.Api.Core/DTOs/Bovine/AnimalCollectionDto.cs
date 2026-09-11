using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalCollectionDto
{
    [JsonPropertyName("resourceType")]
    public string? ResourceType { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("animals")]
    public IEnumerable<AnimalSummaryDto> Animals { get; set; } = [];
}
