using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagStaging
{
    public decimal EstId { get; set; }

    public string? EstEartag { get; set; }

    public string? EstUsageCode { get; set; }

    public string? EstIdentifierAvailability { get; set; }

    public string? EstOrderLocationRepd { get; set; }

    public decimal? EstLocIdOrder { get; set; }

    public string? EstEartagReasonCode { get; set; }

    public decimal? EstErfId { get; set; }

    public DateOnly? EstCurrentModifiedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtEartagReasonFlag? EstErf { get; set; }

    public virtual CtLocation? EstLocIdOrderNavigation { get; set; }
}
