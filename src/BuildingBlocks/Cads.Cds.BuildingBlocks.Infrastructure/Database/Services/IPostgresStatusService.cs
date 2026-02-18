namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public interface IPostgresStatusService
{
    Task<bool> CanConnect(CancellationToken cancellationToken = default);
}