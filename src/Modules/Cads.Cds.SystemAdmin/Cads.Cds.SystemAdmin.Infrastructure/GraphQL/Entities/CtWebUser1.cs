using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWebUser1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal? WurCurrentPid { get; set; }

    public decimal? WurVersion { get; set; }

    public decimal WurId { get; set; }

    public string? WurAccessNumber { get; set; }

    public decimal? WurBadLoginResetCount { get; set; }

    public decimal? WurBadLoginPerDayCount { get; set; }

    public string? WurPasswordIssueFlag { get; set; }

    public string? WurUserType { get; set; }

    public decimal? WurLprIdKeeper { get; set; }

    public string? WurEncryptedPassword { get; set; }

    public string? WurStaffNumber { get; set; }

    public string? WurWelshIndicator { get; set; }

    public string? WurIssuedToIdentifier { get; set; }

    public string? WurSecurityFilename { get; set; }

    public string? WurMobileNumber { get; set; }

    public string? WurTelephoneNumber { get; set; }

    public string? WurUserName { get; set; }

    public string? WurUserLocation { get; set; }

    public string? WurAddress2 { get; set; }

    public string? WurAddress3 { get; set; }

    public string? WurAddress4 { get; set; }

    public string? WurAddress5 { get; set; }

    public string? WurPostCode { get; set; }

    public string? WurEmailAddress { get; set; }

    public DateOnly? WurExpiryDate { get; set; }

    public string? WurPasswordFilename { get; set; }

    public string? WurCurrentUser { get; set; }

    public string? WurCurrentStatus { get; set; }

    public DateOnly? WurCurrentModifiedDate { get; set; }

    public decimal? RowNumber { get; set; }

    public long? WurAudId { get; set; }

    public string? WurAudType { get; set; }

    public DateTime? WurAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtWebUser2> CtWebUser2s { get; set; } = new List<CtWebUser2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}