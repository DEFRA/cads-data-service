using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkImporting;

public sealed class MarkFileImportingCommandHandler(
    IFileImportRepository fileImportRepository)
    : ICommandHandler<MarkFileImportingCommand, Unit>
{
    public async Task<Unit> Handle(MarkFileImportingCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkImporting();

        return Unit.Value;
    }
}