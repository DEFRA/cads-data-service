using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCounty
{
    public decimal? CtyCurrentPid { get; set; }

    public decimal? CtyVersion { get; set; }

    public decimal CtyId { get; set; }

    public string? CtyCode { get; set; }

    public string? CtyName { get; set; }

    public string? CtyUkArea { get; set; }

    public string? CtyVetArea { get; set; }

    public string? CtyPassportArea { get; set; }

    public string? CtyAdminOffice { get; set; }

    public string? CtyBcmsTeam { get; set; }

    public string? CtyInspectionArea { get; set; }

    public string? CtyDataMgtArea { get; set; }

    public string? CtyCurrentUser { get; set; }

    public string? CtyCurrentStatus { get; set; }

    public DateOnly? CtyCurrentModifiedDate { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtLocation> CtLocations { get; set; } = new List<CtLocation>();
}
