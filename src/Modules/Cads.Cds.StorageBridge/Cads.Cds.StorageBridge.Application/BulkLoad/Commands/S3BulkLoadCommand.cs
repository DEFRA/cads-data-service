using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3BulkLoadCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public BulkLoadDataTypes BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';

    public ImportActions ImportActionType { get; set; } = ImportActions.Insert | ImportActions.Update;
}