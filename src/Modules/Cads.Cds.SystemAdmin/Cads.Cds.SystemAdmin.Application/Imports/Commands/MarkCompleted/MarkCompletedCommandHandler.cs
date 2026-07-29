using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkCompleted;

public sealed class MarkCompletedCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<MarkCompletedCommand, Unit>
{
    public async Task<Unit> Handle(MarkCompletedCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkCompleted();

        return Unit.Value;
    }
}