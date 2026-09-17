namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cts.Entities;

public partial class CtBatchRetentionConf
{
    public string? BrtItemId { get; set; }

    public decimal? BrtRetentionDays { get; set; }

    public string? BrtDescription { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}