using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQuery : PagedQuery<LocationDto>
{
    public string? Cph { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}