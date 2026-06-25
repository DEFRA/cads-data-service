using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries;

public sealed record GetFileImportByFileNameQuery(string FileName)
    : IQuery<FileImportDto?>;