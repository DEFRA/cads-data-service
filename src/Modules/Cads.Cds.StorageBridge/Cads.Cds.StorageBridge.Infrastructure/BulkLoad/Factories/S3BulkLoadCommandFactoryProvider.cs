using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public class S3BulkLoadCommandFactoryProvider : IS3BulkLoadCommandFactoryProvider
{
    public IS3BulkLoadCommandFactory Create(NpgsqlConnection connection)
        => new S3BulkLoadCommandFactory(connection);
}