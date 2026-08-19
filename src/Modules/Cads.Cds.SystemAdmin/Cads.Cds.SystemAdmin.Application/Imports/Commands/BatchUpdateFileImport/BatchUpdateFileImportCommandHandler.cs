using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Uow;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;

public class BatchUpdateFileImportCommandHandler(
    ISystemAdminUnitOfWork uow,
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<BatchUpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(BatchUpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        await uow.ExecuteInTransactionAsync(_ =>
        {
            fileImportRepository.BatchUpdateAsync(command.GroupKey, command.TotalRowsToProcess, command.RowsFound, command.ImportStatus);

            return Task.FromResult(Unit.Value);
        }, cancellationToken);

        return Unit.Value;
    }
}