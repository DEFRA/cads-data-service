using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed record GetFileImportByFileNameQuery(string FileName)
    : IQuery<FileImportDto?>;