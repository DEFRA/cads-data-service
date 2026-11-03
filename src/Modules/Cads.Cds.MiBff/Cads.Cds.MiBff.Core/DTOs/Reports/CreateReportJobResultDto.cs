using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class CreateReportJobResultDto
{
    [JsonPropertyName("jobId")]
    public required Guid JobId { get; set; }
}