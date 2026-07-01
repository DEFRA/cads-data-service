using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public interface IS3ImportCommandFactoryProvider
{
    IS3ImportCommandFactory Create(NpgsqlConnection connection);
}