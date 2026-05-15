using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3BulkLoadCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public BulkLoadDataType BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';

    public ImportActionType ImportActionType { get; set; } = ImportActionType.Insert | ImportActionType.Update;
}