using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using System.Globalization;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using Cads.Cds.SystemAdmin.Application.Imports.BusinessRules;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed class CreateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : ICommandHandler<CreateFileImportCommand, FileImport>
{
    public async Task<FileImport> Handle(CreateFileImportCommand command, CancellationToken cancellationToken)
    {
        BusinessRuleChecker.CheckRule(new FileNameMustBeUniqueRule(fileImportRepository, command.FileName, cancellationToken));

        var parsedFileName = CtsmFilenameParser.Parse(command.FileName)!;

        var fileImport = new FileImport
        {
            FileName = command.FileName,
            DestinationTableName = parsedFileName.GetDestinationTableName(),
            TotalRowsToProcess = command.TotalRowsToProcess,
            RowsFound = command.RowsFound,
            GroupKey = parsedFileName.GetGroupKey(),
            ImportType = parsedFileName.Type,
            BatchDate = DateTimeOffset.ParseExact(parsedFileName.Timestamp, "yyyy-MM-dd-HHmmss", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal),
        };

        await fileImportRepository.AddAsync(fileImport, cancellationToken);

        return fileImport;
    }
}