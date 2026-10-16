namespace Cads.Cds.SystemAdmin.Application.DbAdmin.Services;

public interface ICtsImportPrepareJobQueue
{
    /// <summary>
    /// Queues a CTS parallel bulk import prepare job for background execution.
    /// </summary>
    /// <param name="requestedBy">The user who requested the job, used for logging.</param>
    /// <returns>
    /// <c>false</c> if a prepare job is already queued or running in this instance (nothing is queued).
    /// </returns>
    bool TryEnqueue(string? requestedBy);
}