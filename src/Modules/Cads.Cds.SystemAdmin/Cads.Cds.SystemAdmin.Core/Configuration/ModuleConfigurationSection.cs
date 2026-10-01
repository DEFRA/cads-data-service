namespace Cads.Cds.SystemAdmin.Core.Configuration;

public static class ModuleConfigurationSection
{
    public const string ModuleSectionName = "Modules:SystemAdmin";

    public static readonly string QueuesSectionName = $"{ModuleSectionName}:Queues";

    public static readonly string ImportsDeduplicationSectionName = $"{ModuleSectionName}:ImportsDeduplication";

    public static readonly string SqsAdminQueuesSectionName = $"{ModuleSectionName}:SqsAdmin:Queues";
}