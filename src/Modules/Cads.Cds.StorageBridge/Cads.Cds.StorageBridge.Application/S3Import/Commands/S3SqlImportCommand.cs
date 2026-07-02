using Cads.Cds.StorageBridge.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3SqlImportCommand : IStorageBridgeCommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;
}