namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public interface IReportResult { }

public class FileReportResult(MemoryStream stream, string fileName, string contentType) : IReportResult
{
    public MemoryStream Stream { get; } = stream;
    public string FileName { get; } = fileName;
    public string ContentType { get; } = contentType;
}

public class JsonReportResult(object payload) : IReportResult
{
    public object Payload { get; } = payload;
}