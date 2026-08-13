using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.BusinessRules;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using System.Globalization;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, FileImport>
{
    public async Task<FileImport> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        BusinessRuleChecker.CheckRule(new FileNameMustBeUniqueRule(fileImportRepository, command.FileName, cancellationToken));

        var parsedFileName = CtsmFilenameParser.Parse(command.FileName);
        if (parsedFileName is null)
        {
            throw new UnprocessableException($"Unable to parse file name '{command.FileName}'.");
        }

        var destinationTableName = parsedFileName.GetDestinationTableName();

        // Validate timestamp before parsing and allow null batchDate
        DateTimeOffset? batchDate = null;
        if (!string.IsNullOrEmpty(parsedFileName.Timestamp))
        {
            if (!DateTimeOffset.TryParseExact(parsedFileName.Timestamp, "yyyy-MM-dd-HHmmss", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out var parsedBatchDate))
            {
                throw new UnprocessableException($"Invalid or missing timestamp '{parsedFileName.Timestamp}' derived from file name '{parsedFileName}'.");
            }

            batchDate = parsedBatchDate;
        }

        var fileImport = new FileImport
        {
            FileName = command.FileName,
            DestinationTableName = destinationTableName ?? "UNKNOWN",
            TotalRowsToProcess = command.TotalRowsToProcess,
            RowsFound = command.RowsFound,
            GroupKey = parsedFileName.GetGroupKey(),
            ImportType = parsedFileName.Type,
            BatchDate = batchDate,
        };

        if (destinationTableName is null)
        {
            fileImport.SetImportStatus(FileImportStatus.Failed);
        }

        await fileImportRepository.AddAsync(fileImport, cancellationToken);

        return fileImport;
    }
}