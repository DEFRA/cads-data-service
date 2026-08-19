using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportById;

public sealed record GetFileImportByIdQuery(long Id,bool IncludeSiblings = false)
    : IQuery<IEnumerable<FileImportDto>>;