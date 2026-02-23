using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

public interface IPostgresStatusService
{
    Task<PostgresStatusServiceResult> CanConnect(CancellationToken cancellationToken = default);
}