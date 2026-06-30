using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Npgsql;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;

public class TestableS3BulkLoadCommandFactory(NpgsqlConnection connection,
    IEnumerable<string> columns) : S3ImportCommandFactory(connection)
{
    private readonly List<string> _columns = [.. columns];

    /// <summary>
    /// Deterministic for unit tests. Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="importDataType"></param>
    /// <param name="schemaName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<List<string>> GetColumnNamesAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
        => _columns;

    public string SqlForTempTable(ImportDataType importDataType, SchemaName schemaName)
        => GenerateTempTableSql(importDataType, schemaName);

    public Task<string> SqlForInsert(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
        => GenerateInsertSqlAsync(importDataType, schemaName, cancellationToken);

    public Task<string> SqlForUpdate(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
        => GenerateUpdateSqlAsync(importDataType, schemaName, cancellationToken);

    public Task<string> SqlForUpsert(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
        => GenerateUpsertSqlAsync(importDataType, schemaName, cancellationToken);

    public Task<string> SqlForQuery(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
        => GenerateTempTableQuerySqlAsync(importDataType, schemaName, cancellationToken);
}