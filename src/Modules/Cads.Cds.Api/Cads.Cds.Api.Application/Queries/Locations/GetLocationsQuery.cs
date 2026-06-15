using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQuery : IQuery<IEnumerable<LocationDto>>
{
    /// <summary>
    /// CPH property is the LID_FULL_IDENTIFIER which is a case sensitive string in this format: AA-12/345/6789
    /// ie. the Cph with a prefix of the location type
    /// </summary>
    public string? Cph { get; set; }
    public DateOnly? LastModifiedDate { get; set; }
}