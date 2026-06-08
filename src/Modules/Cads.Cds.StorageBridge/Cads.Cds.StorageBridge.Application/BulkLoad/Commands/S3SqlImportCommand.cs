using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3SqlImportCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;
}