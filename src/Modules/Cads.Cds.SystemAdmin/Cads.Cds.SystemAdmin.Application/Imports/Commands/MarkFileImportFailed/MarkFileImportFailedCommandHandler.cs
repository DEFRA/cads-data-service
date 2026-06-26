using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFileImportComplete;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFileImportFailed;

public sealed class MarkFileImportFailedCommandHandler(
    IFileImportRepository fileImportRepository)
    : ICommandHandler<MarkFileImportCompleteCommand, Unit>
{
    public async Task<Unit> Handle(MarkFileImportCompleteCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkImportFailed();

        return Unit.Value;
    }
}