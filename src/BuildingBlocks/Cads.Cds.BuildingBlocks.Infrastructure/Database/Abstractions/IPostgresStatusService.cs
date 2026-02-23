namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

public interface IPostgresStatusService
{
    Task<bool> CanConnect(CancellationToken cancellationToken = default);
}