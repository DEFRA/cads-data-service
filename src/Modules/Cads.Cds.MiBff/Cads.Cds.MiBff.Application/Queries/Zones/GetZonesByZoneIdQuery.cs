using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Zones;

public record GetZonesByZoneIdQuery(string ZoneId) : IDefaultQuery<UkvDto>;