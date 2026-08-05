using System.Globalization;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
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

        var parsedFileName = CtsmFilenameParser.Parse(command.FileName)!;

        var fileImport = FileImport.Create(new FileImportCreate
        {
            FileName = command.FileName,
            DestinationTableName = parsedFileName.GetDestinationTableName(),
            TotalRowsToProcess = command.TotalRowsToProcess,
            RowsFound = command.RowsFound,
            GroupKey = parsedFileName.GetGroupKey(),
            ImportType = parsedFileName.Type,
            BatchDate = DateTimeOffset.ParseExact(parsedFileName.Timestamp, "yyyy-MM-dd-HHmmss", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal),
        });
        
        await fileImportRepository.Add(fileImport, cancellationToken);

        return fileImport;
    }

    private async Task CheckFileNameAlreadyExistsRule(string fileName, CancellationToken cancellationToken)
    {
        var normalisedFileName = StringExtensions.NormalizeToUpper(fileName)!;

        var fileImport = await fileImportRepository.GetByFileName(normalisedFileName, cancellationToken);

        if (fileImport == null)
            return;

        throw new DomainException($"A record exists with matching file name. ImportStatus: '{fileImport.ImportStatus}'. ProcessingStatus: '{fileImport.ProcessingStatus}'.");
    }
}