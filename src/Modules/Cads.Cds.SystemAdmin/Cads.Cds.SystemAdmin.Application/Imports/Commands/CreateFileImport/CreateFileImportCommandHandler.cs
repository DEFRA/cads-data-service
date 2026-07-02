using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.Extensions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    IFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, FileImportDto>
{
    public async Task<FileImportDto> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        await CheckFileNameAlreadyExistsRule(command.FileName, cancellationToken);

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
                fileImport.ProcessingEndAt);
    }

    private async Task CheckFileNameAlreadyExistsRule(string fileName, CancellationToken cancellationToken)
    {
        fileName = StringExtensions.NormalizeToUpper(fileName)!;

        var fileImport = await fileImportRepository.GetByFileName(fileName, cancellationToken);

        if (fileImport == null)
            return;

        throw new DomainException($"A record exists with matching file name. ImportStatus: '{fileImport.ImportStatus}'. ProcessingStatus: '{fileImport.ProcessingStatus}'.");
    }
}