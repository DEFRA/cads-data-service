using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspAnimalError
{
    public decimal SaeId { get; set; }

    public decimal? SaeSanId { get; set; }

    public string? SaeErrorCode { get; set; }

    public string? SaeAttributeName { get; set; }

    public DateOnly? SaeCurrentModifiedDate { get; set; }

    public string? SaeCurrentUser { get; set; }

    public string? SaeCurrentStatus { get; set; }

    public decimal? SaeCurrentPid { get; set; }

    public decimal? SaeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtSuspendedAnimal? SaeSan { get; set; }
}
