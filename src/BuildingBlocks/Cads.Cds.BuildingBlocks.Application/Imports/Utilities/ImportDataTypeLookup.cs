using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public sealed class ImportDataTypeLookup : IImportDataTypeLookup
{
    public ImportDataType FindByTableName(string tableName, SchemaName schemaName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tableName);

        return Enum.GetValues<ImportDataType>()
            .FirstOrDefault(v => string.Equals(v.GetTableName(schemaName), tableName, StringComparison.OrdinalIgnoreCase));
    }

    public string GetBusinessKey(string ctsTransactionsTableName)
    {
        var importDataType = FindByTableName(ctsTransactionsTableName, SchemaName.CtsTransactions);

        if (importDataType == ImportDataType.None)
        {
            throw new InvalidOperationException(
                $"Table '{ctsTransactionsTableName}' has no {nameof(ImportDataType)} mapping for schema {SchemaName.CtsTransactions}.");
        }

        return importDataType.GetTableKey(SchemaName.Cts)
            ?? throw new InvalidOperationException(
                $"Table '{ctsTransactionsTableName}' has no primary key defined for schema {SchemaName.Cts}.");
    }
}
