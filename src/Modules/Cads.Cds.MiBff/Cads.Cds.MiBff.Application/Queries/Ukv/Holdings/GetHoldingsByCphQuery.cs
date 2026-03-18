using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Holdings;

public record GetHoldingsByCphQuery(string cph) : IJsonResponseDataQuery<HoldingDto>;