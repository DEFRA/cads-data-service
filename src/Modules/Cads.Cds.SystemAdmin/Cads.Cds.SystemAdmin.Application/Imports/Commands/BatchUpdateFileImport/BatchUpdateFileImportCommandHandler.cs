using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;

public class BatchUpdateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<BatchUpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(BatchUpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        await fileImportRepository.BatchUpdateAsync(command.GroupKey, command.TotalRowsToProcess, command.RowsFound, command.RowsImported, command.LastFilePartImported, command.ImportStatus);

        return Unit.Value;
    }
}