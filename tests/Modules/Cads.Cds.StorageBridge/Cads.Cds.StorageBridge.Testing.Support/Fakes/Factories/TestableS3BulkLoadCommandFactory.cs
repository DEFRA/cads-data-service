using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using Npgsql;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;

public class TestableS3BulkLoadCommandFactory(NpgsqlConnection connection,
    IEnumerable<string> columns) : S3BulkLoadCommandFactory(connection)
{
    private readonly List<string> _columns = [.. columns];

    //public DbCommand CreateTempTableCommand(BulkLoadDataTypes t)
    //    => throw new NotImplementedException();

    //public StreamWriter CreateTextImport(BulkLoadDataTypes bulkImportType, char delimiter)
    //    => throw new NotImplementedException();

    //public StreamWriter CreateTextImport(BulkLoadDataTypes type, char delimiter, IEnumerable<string> columns)
    //    => throw new NotImplementedException();

    //public Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataTypes t, CancellationToken ct)
    //    => throw new NotImplementedException();

    //public Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataTypes t, CancellationToken ct)
    //    => throw new NotImplementedException();

    //public Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataTypes t, CancellationToken ct)
    //    => throw new NotImplementedException();

    //public Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataTypes t, CancellationToken ct)
    //    => throw new NotImplementedException();    

    //public string GetTableName(BulkLoadDataTypes bulkImportType, bool isTemp = false)
    //    => throw new NotImplementedException();

    //public Task<List<string>> FilterColumnsToTableAsync(BulkLoadDataTypes type, IEnumerable<string> fileColumns, CancellationToken ct)
    //    => throw new NotImplementedException();

    /// <summary>
    /// Deterministic for unit tests. Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="bulkImportType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<List<string>> GetColumnNamesAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
        => _columns;

    //public Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    //    => throw new NotImplementedException();
}