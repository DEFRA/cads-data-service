using Npgsql;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public interface IPostgresDataSourceFactory
{
    NpgsqlDataSource CreateDataSource(string connectionIdentifier);
}