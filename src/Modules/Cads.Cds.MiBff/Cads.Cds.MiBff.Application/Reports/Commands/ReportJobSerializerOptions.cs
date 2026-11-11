using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Commands;

 static class ReportJobSerializerOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}