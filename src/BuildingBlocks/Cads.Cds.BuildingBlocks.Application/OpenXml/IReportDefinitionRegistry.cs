using Cads.Cds.BuildingBlocks.Core.OpenXml;

namespace Cads.Cds.BuildingBlocks.Application.OpenXml;

public interface IReportDefinitionRegistry
{
    IReportDefinition<T> GetDefinition<T>();
}