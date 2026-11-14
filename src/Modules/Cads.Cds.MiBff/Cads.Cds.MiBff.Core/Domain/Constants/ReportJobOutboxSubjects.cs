namespace Cads.Cds.MiBff.Core.Domain.Constants;

public static class ReportJobOutboxSubjects
{
    /// <summary>
    /// Matches the SQS "Subject" attribute, which is derived from the message type name
    /// (ReportJobRequestedMessage with the "Message" suffix removed).
    /// </summary>
    public const string ReportJobRequested = "ReportJobRequested";
}