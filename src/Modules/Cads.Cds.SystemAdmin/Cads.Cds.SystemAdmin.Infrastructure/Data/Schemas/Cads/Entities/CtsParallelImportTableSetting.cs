namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class CtsParallelImportTableSetting
{
    public string TableName { get; set; } = null!;

    public int MaxWorkers { get; set; }

    public int? BatchSize { get; set; }
}