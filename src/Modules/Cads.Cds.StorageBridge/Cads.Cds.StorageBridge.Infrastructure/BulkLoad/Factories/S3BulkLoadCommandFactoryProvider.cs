using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

[ExcludeFromCodeCoverage]
public class S3BulkLoadCommandFactoryProvider : IS3BulkLoadCommandFactoryProvider
{
    public IS3BulkLoadCommandFactory Create(NpgsqlConnection connection)
        => new S3BulkLoadCommandFactory(connection);
}