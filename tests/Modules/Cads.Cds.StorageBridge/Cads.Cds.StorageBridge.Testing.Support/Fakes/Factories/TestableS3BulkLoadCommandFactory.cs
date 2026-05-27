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

    public string SqlForTempTable(BulkLoadDataTypes bulkImportType) => GenerateTempTableSql(bulkImportType);

    public Task<string> SqlForInsert(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
        => GenerateInsertSqlAsync(bulkImportType, cancellationToken);

    public Task<string> SqlForUpdate(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
        => GenerateUpdateSqlAsync(bulkImportType, cancellationToken);

    public Task<string> SqlForUpsert(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
        => GenerateUpsertSqlAsync(bulkImportType, cancellationToken);

    public Task<string> SqlForDelete(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
        => GenerateDeleteSqlAsync(bulkImportType, cancellationToken);

    public Task<string> SqlForQuery(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
        => GenerateTempTableQuerySqlAsync(bulkImportType, cancellationToken);
}