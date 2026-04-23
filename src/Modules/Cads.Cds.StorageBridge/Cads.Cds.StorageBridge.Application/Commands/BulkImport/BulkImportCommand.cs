using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.Commands.BulkImport;

public class BulkImportCommand : ICommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public BulkImportType BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';
}
