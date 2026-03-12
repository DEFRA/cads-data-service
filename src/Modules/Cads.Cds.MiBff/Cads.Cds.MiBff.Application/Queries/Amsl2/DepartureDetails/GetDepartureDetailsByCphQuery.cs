using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails;

public record GetDepartureDetailsByCphQuery(string Cph) : IJsonResponseDataQuery<DepartureDetailsDto>;