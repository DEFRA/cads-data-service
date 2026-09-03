using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionVariant1
{
    public string? CovConditionVariant { get; set; }

    public decimal CovId { get; set; }

    public decimal? CovConId { get; set; }

    public decimal? CovCurrentPid { get; set; }

    public string? CovAlertMovement { get; set; }

    public string? CovReportMovement { get; set; }

    public string? CovShortDescription { get; set; }

    public string? CovLongDescription { get; set; }

    public decimal? CovDefaultPeriod { get; set; }

    public DateOnly? CovEffectiveFromDate { get; set; }

    public string? CovAccessRestricted { get; set; }

    public string? CovScope { get; set; }

    public string? CovAutoSnaffle { get; set; }

    public string? CovAlertMarkerCreation { get; set; }

    public DateOnly? CovEffectiveToDate { get; set; }

    public string? CovLetterType { get; set; }

    public string? CovLetterDataSource { get; set; }

    public string? CovLiveIndicator { get; set; }

    public decimal? CovLetterMaxAnimals { get; set; }

    public string? CovMultipleUsage { get; set; }

    public string? CovMovtRestrictType { get; set; }

    public string? CovCessationReason { get; set; }

    public string? CovCurrentStatus { get; set; }

    public string? CovCurrentUser { get; set; }

    public DateOnly? CovCurrentModifiedDate { get; set; }

    public decimal? CovVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? CovAudId { get; set; }

    public string? CovAudType { get; set; }

    public DateTime? CovAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}