using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

public record GetHoldingsByCphQuery(string cph) : IDefaultQuery<HoldingDto>;