using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtIssuingAuthority
{
    public decimal IsaId { get; set; }

    public string? IsaCountryName { get; set; }

    public string? IsaManufacturersName { get; set; }

    public string? IsaType { get; set; }

    public string? IsaCurrentStatus { get; set; }

    public string? IsaCurrentUser { get; set; }

    public DateOnly? IsaCurrentModifiedDate { get; set; }

    public decimal? IsaCurrentPid { get; set; }

    public decimal? IsaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtElectronicIdentifier> CtElectronicIdentifiers { get; set; } = new List<CtElectronicIdentifier>();
}