using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalCorrSummError
{
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

    public long? TransId { get; set; }

    public virtual CtAnimalCorrectSummary? AseAcs { get; set; }
}
