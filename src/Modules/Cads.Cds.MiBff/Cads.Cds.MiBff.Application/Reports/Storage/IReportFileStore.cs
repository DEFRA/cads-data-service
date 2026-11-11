namespace Cads.Cds.MiBff.Application.Reports.Storage;

/// <summary>
/// Persists generated report output. Implementations encrypt the content before it is stored.
/// </summary>
public interface IReportFileStore
{
    /// <summary>
    /// Encrypts <paramref name="content"/> and stores it under <paramref name="key"/>.
    /// The stream is read from its current position and is not closed.
    /// </summary>
    Task SaveAsync(string key, Stream content, CancellationToken cancellationToken = default);
}
