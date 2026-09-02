using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationsFaker1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? LocTelNumber { get; set; }

    public string? LocMobileNumber { get; set; }

    public string? LocFaxNumber { get; set; }

    public string? LocEmailAddress { get; set; }

    public string? LocSourceReference { get; set; }

    public string? LocComments { get; set; }

    public long? LocAudId { get; set; }

    public string? LocAudType { get; set; }

    public DateTime? LocAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLocationsFaker2> CtLocationsFaker2s { get; set; } = new List<CtLocationsFaker2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
