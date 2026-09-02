using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAddress1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AdrId { get; set; }

    public decimal? AdrLocId { get; set; }

    public decimal? AdrParId { get; set; }

    public string? AdrName { get; set; }

    public string? AdrAddress2 { get; set; }

    public string? AdrAddress3 { get; set; }

    public string? AdrAddress4 { get; set; }

    public string? AdrAddress5 { get; set; }

    public string? AdrPostCode { get; set; }

    public DateOnly? AdrCurrentModifiedDate { get; set; }

    public string? AdrCurrentStatus { get; set; }

    public string? AdrCurrentUser { get; set; }

    public decimal? AdrCurrentPid { get; set; }

    public decimal? AdrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AdrAudId { get; set; }

    public string? AdrAudType { get; set; }

    public DateTime? AdrAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAddress2> CtAddress2s { get; set; } = new List<CtAddress2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
