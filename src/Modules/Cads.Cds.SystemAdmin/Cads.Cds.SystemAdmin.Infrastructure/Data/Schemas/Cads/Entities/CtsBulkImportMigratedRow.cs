using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class CtsBulkImportMigratedRow
{
    public string TableName { get; set; } = null!;

    public long TransactionRowId { get; set; }

    public DateTime MigratedAt { get; set; }
}