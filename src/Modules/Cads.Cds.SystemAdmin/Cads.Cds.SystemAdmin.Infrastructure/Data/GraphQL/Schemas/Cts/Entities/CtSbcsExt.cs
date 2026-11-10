namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cts.Entities;

public partial class CtSbcsExt
{
    public string? SxtId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public string TransType { get; set; } = null!;
}