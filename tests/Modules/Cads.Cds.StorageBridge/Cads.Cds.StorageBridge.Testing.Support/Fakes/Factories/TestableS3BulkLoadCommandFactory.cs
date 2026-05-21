using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using Npgsql;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;

public class TestableS3BulkLoadCommandFactory(NpgsqlConnection connection,
    IEnumerable<string> columns) : S3BulkLoadCommandFactory(connection)
{
    private readonly List<string> _columns = [.. columns];

    /// <summary>
    /// Deterministic for unit tests. Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="bulkImportType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<List<string>> GetColumnNamesAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
        => _columns;
}