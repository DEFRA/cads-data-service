using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed class GetFileImportByFileNameQueryHandler(
    IFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportByFileNameQuery, FileImportDto?>
{
    public async Task<FileImportDto?> Handle(GetFileImportByFileNameQuery query, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetByFileName(query.FileName, cancellationToken);

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
                fileImport.ProcessingEndAt);
    }
}