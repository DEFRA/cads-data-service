using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public interface IS3BulkLoadCommandFactoryProvider
{
    IS3BulkLoadCommandFactory Create(NpgsqlConnection connection);
}