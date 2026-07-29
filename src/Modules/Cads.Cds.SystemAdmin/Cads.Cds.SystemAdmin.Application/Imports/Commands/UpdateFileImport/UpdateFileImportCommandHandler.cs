using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;

public class UpdateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<UpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(UpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.SetTotalRowsToProcess(command.TotalRowsToProcess);
        fileImport.SetRowsFound(command.RowsFound);
        fileImport.SetImportStatus(command.ImportStatus);

        return Unit.Value;
    }
}