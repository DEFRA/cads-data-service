using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFailed;

public sealed class MarkFailedCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<MarkFailedCommand, Unit>
{
    public async Task<Unit> Handle(MarkFailedCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkFailed(command.Reason);

        return Unit.Value;
    }
}