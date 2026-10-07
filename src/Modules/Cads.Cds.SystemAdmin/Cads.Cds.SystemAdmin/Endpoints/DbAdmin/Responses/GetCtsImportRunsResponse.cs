using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;

public record GetCtsImportRunsResponse(IReadOnlyList<CtsImportRunDto> Runs);