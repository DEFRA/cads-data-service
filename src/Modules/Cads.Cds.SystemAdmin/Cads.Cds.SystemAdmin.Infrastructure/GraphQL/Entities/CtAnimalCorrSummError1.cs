using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalCorrSummError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AseId { get; set; }

    public decimal? AseAcsId { get; set; }

    public string? AseCurrentUser { get; set; }

    public string? AseCurrentStatus { get; set; }

    public DateOnly? AseCurrentModifiedDate { get; set; }

    public decimal? AseCurrentPid { get; set; }

    public string? AseAttributeName { get; set; }

    public string? AseErrorCode { get; set; }

    public decimal? AseVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AseAudId { get; set; }

    public string? AseAudType { get; set; }

    public DateTime? AseAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAnimalCorrSummError2> CtAnimalCorrSummError2s { get; set; } = new List<CtAnimalCorrSummError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}