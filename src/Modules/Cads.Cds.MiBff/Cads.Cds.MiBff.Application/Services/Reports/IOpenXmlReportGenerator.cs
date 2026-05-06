namespace Cads.Cds.MiBff.Application.Services.Reports;

public interface IOpenXmlReportGenerator
{
    MemoryStream Generate<T>(List<T> data);
}