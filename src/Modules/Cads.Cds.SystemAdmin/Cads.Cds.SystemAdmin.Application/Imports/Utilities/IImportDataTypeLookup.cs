using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;

namespace Cads.Cds.SystemAdmin.Application.Imports.Utilities;

public interface IImportDataTypeLookup
{
    /// <summary>
    /// Finds the <see cref="ImportDataType"/> mapped to <paramref name="tableName"/> in <paramref name="schemaName"/>,
    /// or <see cref="ImportDataType.None"/> when there is none.
    /// </summary>
    ImportDataType FindByTableName(string tableName, SchemaName schemaName);

    /// <summary>
    /// The business key of a <c>cts_transactions</c> table: the <c>Cts</c> schema primary key of the same table
    /// (e.g. <c>loc_id</c> for <c>ct_locations</c>). Throws when the table or its key isn't mapped.
    /// </summary>
    string GetBusinessKey(string ctsTransactionsTableName);
}