namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class RedirectReportResult(string url, DateTimeOffset expiresAt) : IReportResult
{
    public string Url { get; } = url;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}