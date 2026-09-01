using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.StorageBridge.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommand : IStorageBridgeCommand<Guid>
{
    public long? FileImportId { get; set; }

    public string? SourceKey { get; set; }

    public char Delimiter { get; set; }

    public FileImportStatus? ForceResetImportStatus { get; set; }
}