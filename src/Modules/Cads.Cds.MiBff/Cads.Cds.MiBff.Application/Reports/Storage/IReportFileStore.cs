namespace Cads.Cds.MiBff.Application.Reports.Storage;

public interface IReportFileStore
{
    Task SaveAsync(string key, Stream content, CancellationToken cancellationToken = default);

    Task<Stream> GetAsync(string key, CancellationToken cancellationToken = default);
}