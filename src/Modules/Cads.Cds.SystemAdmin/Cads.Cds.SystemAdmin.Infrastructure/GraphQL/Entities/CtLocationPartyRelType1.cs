using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationPartyRelType1
{
    public decimal LptId { get; set; }

    public string? LptCode { get; set; }

    public string? LptDescription { get; set; }

    public string? LptGapsAllowed { get; set; }

    public string? LptMandatory { get; set; }

    public string? LptPrimarySingleLink { get; set; }

    public string? LptSecondSingleLink { get; set; }

    public string? LptHierarchicalLink { get; set; }

    public string? LptRelshipTextDown { get; set; }

    public string? LptRelshipTextUp { get; set; }

    public string? LptCurrentUser { get; set; }

    public string? LptCurrentStatus { get; set; }

    public DateOnly? LptCurrentModifiedDate { get; set; }

    public decimal? LptCurrentPid { get; set; }

    public decimal? LptVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LptAudId { get; set; }

    public string? LptAudType { get; set; }

    public DateTime? LptAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}