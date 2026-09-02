using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMgtControlError
{
    public decimal MceId { get; set; }

    public decimal? MceRanId { get; set; }

    public string? MceErrorCode { get; set; }

    public decimal? McePassportVersionIssued { get; set; }

    public decimal? MceNumberOfDaysLate { get; set; }

    public string? MceCurrentUser { get; set; }

    public string? MceCurrentStatus { get; set; }

    public DateOnly? MceCurrentModifiedDate { get; set; }

    public decimal? MceCurrentPid { get; set; }

    public decimal? MceVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtRegisteredAnimal? MceRan { get; set; }
}
