namespace Cads.Cds.MiBff.Core.Configuration;

public static class ModuleConfigurationSection
{
    public const string ModuleSectionName = "Modules:MiBff";
    public static readonly string QueuesSectionName = $"{ModuleSectionName}:Queues";
    public static readonly string ReportJobOutboxDispatcherSectionName = $"{ModuleSectionName}:ReportJobOutboxDispatcher";
    public static readonly string StorageSectionName = $"{ModuleSectionName}:Storage";
}