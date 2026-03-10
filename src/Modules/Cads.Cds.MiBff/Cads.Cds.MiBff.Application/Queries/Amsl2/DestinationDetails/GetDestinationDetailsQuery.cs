using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails;

public record GetDestinationDetailsQuery(string DestinationType, Guid DestinationId) : IDefaultQuery<DestinationDetailsDto>;