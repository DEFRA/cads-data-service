namespace Cads.Cds.MiBff.Application.Reports.Storage;

public record ReportDownloadLink(string Url, DateTimeOffset ExpiresAt);

public interface ICadsMiReportPublisher
{
    Task<ReportDownloadLink> PublishXlsxAsync(
        string reportKey, Guid jobId, MemoryStream xlsxContent, CancellationToken cancellationToken = default);
}