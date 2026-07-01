using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public ImportDataType ImportDataType { get; set; }

    public ImportActionType ImportActionType { get; set; }

    public char Delimiter { get; set; }
}