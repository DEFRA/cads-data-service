using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.Extensions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed class GetFileImportByFileNameQueryHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportByFileNameQuery, FileImportDto?>
{
    public async Task<FileImportDto?> Handle(GetFileImportByFileNameQuery query, CancellationToken cancellationToken)
    {
        var fileName = StringExtensions.NormalizeToUpper(query.FileName)!;
        var fileImport = await fileImportRepository.GetByFileName(fileName, cancellationToken);

        return fileImport is null
            ? null
            : new FileImportDto(
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
                fileImport.ProcessingEndAt,
                fileImport.FailedAttempts,
                fileImport.LastErrorReason);
    }
}