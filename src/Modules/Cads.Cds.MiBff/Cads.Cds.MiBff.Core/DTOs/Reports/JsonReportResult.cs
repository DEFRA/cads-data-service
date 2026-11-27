namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class JsonReportResult(object payload) : IReportResult
{
    public object Payload { get; } = payload;
}