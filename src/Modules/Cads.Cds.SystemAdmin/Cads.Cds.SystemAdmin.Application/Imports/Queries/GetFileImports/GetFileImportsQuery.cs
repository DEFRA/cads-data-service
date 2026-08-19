using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImports;

public sealed record GetFileImportsQuery(
    string? FileName = null,
    string? GroupKey = null,
    FileImportStatus? FileImportStatus = null, 
    FileProcessingStatus? FileProcessingStatus = null)
    : IQuery<IEnumerable<FileImportDto>>;