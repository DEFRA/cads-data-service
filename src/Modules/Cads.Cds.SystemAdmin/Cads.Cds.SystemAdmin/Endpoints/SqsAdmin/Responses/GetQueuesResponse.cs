using Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;

namespace Cads.Cds.SystemAdmin.Endpoints.SqsAdmin.Responses;

public record GetQueuesResponse(IReadOnlyList<QueueInfoDto> Queues);

