using Cads.Cds.StorageBridge.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommand : IStorageBridgeCommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public char Delimiter { get; set; }
}