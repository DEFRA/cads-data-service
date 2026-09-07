using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtElectronicIdentifier
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal EidId { get; set; }

    public decimal? EidElectronicIdentifier { get; set; }

    public decimal? EidIsaId { get; set; }

    public string? EidUniqueNumber { get; set; }

    public string? EidCurrentStatus { get; set; }

    public string? EidCurrentUser { get; set; }

    public decimal? EidCurrentPid { get; set; }

    public DateOnly? EidCurrentModifiedDate { get; set; }

    public decimal? EidVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? EidAudId { get; set; }

    public string? EidAudType { get; set; }

    public DateTime? EidAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
