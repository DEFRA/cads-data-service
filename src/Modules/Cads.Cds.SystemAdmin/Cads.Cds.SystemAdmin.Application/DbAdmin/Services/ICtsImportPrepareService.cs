namespace Cads.Cds.SystemAdmin.Application.DbAdmin.Services;

public interface ICtsImportPrepareService
{
    /// <summary>
    /// Plans a new CTS parallel bulk import run. This is long running (20-30 minutes on full data)
    /// and executes as a single database transaction.
    /// </summary>
    /// <returns>The id of the newly created run.</returns>
    Task<long> PrepareAsync(CancellationToken cancellationToken = default);
}