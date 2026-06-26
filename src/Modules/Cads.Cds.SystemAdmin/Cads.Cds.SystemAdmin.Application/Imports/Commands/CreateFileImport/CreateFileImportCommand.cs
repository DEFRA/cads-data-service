using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed record CreateFileImportCommand(
    string DestinationTableName,
    string FileName,
    long TotalRowsToProcess,
    long RowsFound
) : ICommand<long>, ITransactionalCommand;