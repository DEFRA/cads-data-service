using Npgsql;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

public interface IPostgresDataSourceFactory
{
    NpgsqlDataSource CreateDataSource(string connectionIdentifier);
}