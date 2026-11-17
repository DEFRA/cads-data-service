using System.Text.Json;

namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class PagedReportPayload
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required bool HasMore { get; init; }
    public required IReadOnlyList<JsonElement> Items { get; init; }
}

