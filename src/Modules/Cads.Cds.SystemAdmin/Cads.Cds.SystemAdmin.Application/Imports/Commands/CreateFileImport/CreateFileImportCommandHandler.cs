using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.Extensions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, FileImport>
{
    public async Task<FileImport> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        await CheckFileNameAlreadyExistsRule(command.FileName, cancellationToken);

        var destinationTableNameWithSchema = SchemaHelper.GetDestinationTableNameFromFilename(command.FileName);

        var fileImport = FileImport.Create(
            destinationTableNameWithSchema,
            command.FileName,
            command.TotalRowsToProcess,
            command.RowsFound);

        await fileImportRepository.AddAsync(fileImport, cancellationToken);

        return fileImport;
    }

    private async Task CheckFileNameAlreadyExistsRule(string fileName, CancellationToken cancellationToken)
    {
        var normalisedFileName = StringExtensions.NormalizeToUpper(fileName)!;

        var fileImport = await fileImportRepository.GetByFileNameAsync(normalisedFileName, cancellationToken);

        if (fileImport == null)
            return;

        throw new DomainException($"A record exists with matching file name. ImportStatus: '{fileImport.ImportStatus}'. ProcessingStatus: '{fileImport.ProcessingStatus}'.");
    }
}