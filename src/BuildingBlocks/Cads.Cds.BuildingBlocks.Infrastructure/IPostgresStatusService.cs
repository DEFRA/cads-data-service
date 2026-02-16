namespace Cads.Cds.BuildingBlocks.Infrastructure;

public interface IPostgresStatusService
{
    public Task<bool> CanConnect(CancellationToken cancellationToken = default);
}