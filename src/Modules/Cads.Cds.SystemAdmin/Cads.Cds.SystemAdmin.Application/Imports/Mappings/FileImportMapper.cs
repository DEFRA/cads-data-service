using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Mappings;

public static class FileImportMapper
{
    public static IEnumerable<FileImportDto> MapToDto(this IEnumerable<FileImport> fileImports)
    {
        return fileImports.Select(fileImport => fileImport.MapToDto());
    }

    public static FileImportDto MapToDto(this FileImport fileImport)
    {
        return new FileImportDto
        {
            Id = fileImport.Id,
            DestinationTableName = fileImport.DestinationTableName,
            FileName = fileImport.FileName,
            GroupKey = fileImport.GroupKey,
            TotalRowsToProcess = fileImport.TotalRowsToProcess,
            RowsFound = fileImport.RowsFound,
            ImportStatus = fileImport.ImportStatus,
            ProcessingStatus = fileImport.ProcessingStatus,
            AddedAt = fileImport.AddedAt,
            ImportStartAt = fileImport.ImportStartAt,
            ImportEndAt = fileImport.ImportEndAt,
            ProcessingStartAt = fileImport.ProcessingStartAt,
            ProcessingEndAt = fileImport.ProcessingEndAt,
            FailedAttempts = fileImport.FailedAttempts,
            LastErrorReason = fileImport.LastErrorReason
        };
    }
}