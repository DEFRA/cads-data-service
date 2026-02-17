namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public interface IPostgresStatusService
{
    public Task<bool> CanConnect(CancellationToken cancellationToken = default);
}