namespace Cads.Cds.BuildingBlocks.Core.OpenXml;

public interface IOpenXmlReportGenerator
{
    MemoryStream Generate<T>(List<T> data);
}