using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    IFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, FileImportDto>
{
    public async Task<FileImportDto> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        var destinationTableName = "TODO - Function to derive this in future Story LANI-167";

        var fileImport = FileImport.Create(
            destinationTableName,
            command.FileName,
            command.TotalRowsToProcess,
            command.RowsFound);

        await fileImportRepository.Add(fileImport, cancellationToken);

        return new FileImportDto(
                fileImport.Id,
                fileImport.DestinationTableName,
                fileImport.FileName,
                fileImport.TotalRowsToProcess,
                fileImport.RowsFound,
                fileImport.ImportStatus,
                fileImport.ProcessingStatus,
                fileImport.AddedAt,
                fileImport.ImportStartAt,
                fileImport.ImportEndAt,
                fileImport.ProcessingStartAt,
                fileImport.ProcessingEndAt); ;
    }
}