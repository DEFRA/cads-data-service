namespace Cads.Cds.ApiSurface.Messages.Reports;

public class ReportJobRequestedMessage : MessageType
{
    public Guid JobId { get; init; }
    public string ReportKey { get; init; } = string.Empty;
}