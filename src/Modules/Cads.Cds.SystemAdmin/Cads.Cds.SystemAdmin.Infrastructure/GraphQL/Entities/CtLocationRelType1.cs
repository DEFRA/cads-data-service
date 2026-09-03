using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationRelType1
{
    public decimal LrtId { get; set; }

    public string? LrtCode { get; set; }

    public string? LrtDescription { get; set; }

    public string? LrtSecondSingleLink { get; set; }

    public string? LrtMandatory { get; set; }

    public string? LrtGapsAllowed { get; set; }

    public string? LrtPrimarySingleLink { get; set; }

    public string? LrtHierarchicalLink { get; set; }

    public string? LrtRelshipTextDown { get; set; }

    public string? LrtRelshipTextUp { get; set; }

    public DateOnly? LrtCurrentModifiedDate { get; set; }

    public string? LrtCurrentStatus { get; set; }

    public string? LrtCurrentUser { get; set; }

    public decimal? LrtCurrentPid { get; set; }

    public decimal? LrtVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LrtAudId { get; set; }

    public string? LrtAudType { get; set; }

    public DateTime? LrtAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}