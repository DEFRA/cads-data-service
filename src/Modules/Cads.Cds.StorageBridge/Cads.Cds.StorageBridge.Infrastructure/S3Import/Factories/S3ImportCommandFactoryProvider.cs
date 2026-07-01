using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

[ExcludeFromCodeCoverage]
public class S3ImportCommandFactoryProvider : IS3ImportCommandFactoryProvider
{
    public IS3ImportCommandFactory Create(NpgsqlConnection connection)
        => new S3ImportCommandFactory(connection);
}