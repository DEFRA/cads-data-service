using Cads.Cds.BuildingBlocks.Core.OpenXml;

namespace Cads.Cds.BuildingBlocks.Application.OpenXml;

public class OpenXmlReportGenerator(IReportDefinitionRegistry registry) : IOpenXmlReportGenerator
{
    private readonly IReportDefinitionRegistry _registry = registry;

    public MemoryStream Generate<T>(List<T> data)
    {
        var definition = _registry.GetDefinition<T>();
        var report = new OpenXmlReport<T>(definition, data);
        return report.Generate();
    }
}