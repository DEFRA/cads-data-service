using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Uow;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;

public class BatchUpdateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<BatchUpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(BatchUpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        await fileImportRepository.BatchUpdateAsync(command.GroupKey, command.TotalRowsToProcess, command.RowsFound, command.ImportStatus);

        return Unit.Value;
    }
} 