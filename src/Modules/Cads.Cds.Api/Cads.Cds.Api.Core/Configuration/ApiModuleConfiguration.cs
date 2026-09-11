using Cads.Cds.BuildingBlocks.Core.Configuration;

namespace Cads.Cds.Api.Core.Configuration;

public class ApiModuleConfiguration
{
    public StaticDataConfig StaticData { get; set; } = new();
}