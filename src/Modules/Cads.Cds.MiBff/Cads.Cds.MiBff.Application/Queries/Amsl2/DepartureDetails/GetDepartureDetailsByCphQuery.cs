using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails;

public record GetDepartureDetailsByCphQuery(string Cph) : IDefaultQuery<DepartureDetailsDto>;