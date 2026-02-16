namespace Cads.Cds.BuildingBlocks.Infrastructure;

public class PostgresStatusService : IPostgresStatusService
{
    private readonly ApplicationDbContext _context;
    public PostgresStatusService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<bool> CanConnect(CancellationToken cancellationToken = default)
    {
        return _context.Database.CanConnectAsync(cancellationToken);
    }
}