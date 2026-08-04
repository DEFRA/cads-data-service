namespace Cads.Cds.SystemAdmin.Core.Configuration;

public class ModuleConfigurationSection
{
    public const string ModuleSectionName = "Modules:SystemAdmin";

    public static readonly string QueuesSectionName = $"{ModuleSectionName}:Queues";

    public static readonly string ImportsDeduplicationSectionName = $"{ModuleSectionName}:ImportsDeduplication";
}