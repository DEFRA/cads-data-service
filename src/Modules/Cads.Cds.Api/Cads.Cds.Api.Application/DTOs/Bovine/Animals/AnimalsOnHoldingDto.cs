using Cads.Cds.Api.Application.DTOs.Holdings;
using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Bovine.Animals;

public class AnimalsOnHoldingDto
{
    [JsonPropertyName("resourceType")]
    public string? ResourceType { get; set; }

    [JsonPropertyName("CPH")]
    public HoldingIdentifierDto? Cph { get; init; }

    [JsonPropertyName("animals")]
    public IReadOnlyList<AnimalSummaryDto> Animals { get; init; } = [];

    [JsonPropertyName("totalRecords")]
    public long TotalRecords { get; init; }

    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }
}