using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationType1
{
    public string? LtyCiiLocationType { get; set; }

    public string? LtyOwnership { get; set; }

    public string? LtyCurrentStatus { get; set; }

    public DateOnly? LtyCurrentModifiedDate { get; set; }

    public string? LtyCurrentUser { get; set; }

    public decimal? LtyCurrentPid { get; set; }

    public decimal? LtyVersion { get; set; }

    public decimal LtyId { get; set; }

    public string? LtyLocType { get; set; }

    public decimal? LtyLifId { get; set; }

    public decimal? LtyLocationTypeReqd { get; set; }

    public string? LtyShortDescription { get; set; }

    public decimal? LtySublocTypeReqd { get; set; }

    public string? LtyLongDescription { get; set; }

    public string? LtyPremisesGroup { get; set; }

    public string? LtyHierLinkPermitted { get; set; }

    public string? LtyMovementLocInd { get; set; }

    public string? LtyPeerLinkPermitted { get; set; }

    public string? LtyPerformAnomalyCheck { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LtyAudId { get; set; }

    public string? LtyAudType { get; set; }

    public DateTime? LtyAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
