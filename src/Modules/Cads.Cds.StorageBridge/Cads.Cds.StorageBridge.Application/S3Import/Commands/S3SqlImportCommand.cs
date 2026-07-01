using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3SqlImportCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;
}