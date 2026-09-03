using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalRelationship
{
    public DateOnly? AarCurrentModifiedDate { get; set; }

    public decimal? AarCurrentPid { get; set; }

    public decimal? AarVersion { get; set; }

    public decimal AarId { get; set; }

    public string? AarRelType { get; set; }

    public decimal? AarLocId { get; set; }

    public decimal? AarConfidenceIndicator { get; set; }

    public DateOnly? AarEffectiveFromDate { get; set; }

    public DateOnly? AarEffectiveToDate { get; set; }

    public decimal? AarRanIdChild { get; set; }

    public decimal? AarRanIdParent { get; set; }

    public string? AarParentIdentifier { get; set; }

    public string? AarParentIdentifierType { get; set; }

    public string? AarCancelledReason { get; set; }

    public string? AarCurrentUser { get; set; }

    public string? AarCurrentStatus { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocation? AarLoc { get; set; }

    public virtual CtRegisteredAnimal? AarRanIdChildNavigation { get; set; }

    public virtual CtRegisteredAnimal? AarRanIdParentNavigation { get; set; }
}