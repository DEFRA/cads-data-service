using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQuery : IQuery<IEnumerable<LocationDto>>
{
    public string? Cph { get; set; }
    public DateOnly? LastModifiedDate { get; set; }
}