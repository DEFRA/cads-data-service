using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails;

public record GetDestinationDetailsQuery(string DestinationType, Guid DestinationId) : IJsonResponseDataQuery<DestinationDetailsDto>;