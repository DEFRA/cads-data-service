using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Commands;

/// <summary>
/// Options used to write and read <c>MiReportJob.RequestPayload</c>. The job is written by the API
/// and read back by the queue consumer, so both sides must use the same settings.
/// </summary>
internal static class ReportJobSerializerOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}
