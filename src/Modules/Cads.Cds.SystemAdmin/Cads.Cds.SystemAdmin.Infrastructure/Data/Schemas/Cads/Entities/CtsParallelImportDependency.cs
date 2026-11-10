namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class CtsParallelImportDependency
{
    public long RunId { get; set; }

    public string ChildTable { get; set; } = null!;

    public string ParentTable { get; set; } = null!;

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}