using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    IFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, long>
{
    public async Task<long> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        var fileImport = FileImport.Create(
            command.DestinationTableName,
            command.FileName,
            command.TotalRowsToProcess,
            command.RowsFound);

        await fileImportRepository.Add(fileImport, cancellationToken);

        return fileImport.Id;
    }
}