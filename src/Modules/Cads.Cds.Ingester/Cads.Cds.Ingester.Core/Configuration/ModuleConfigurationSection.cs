namespace Cads.Cds.Ingester.Core.Configuration;

public static class ModuleConfigurationSection
{
    public const string ModuleSectionName = "Modules:Ingester";

    public static readonly string QueuesSectionName = $"{ModuleSectionName}:Queues";
}