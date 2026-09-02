using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtBreed
{
    public decimal BrdId { get; set; }

    public string? BrdCode { get; set; }

    public string? BrdType { get; set; }

    public string? BrdLongDescription { get; set; }

    public string? BrdSchemeEligibility { get; set; }

    public string? BrdShortDescription { get; set; }

    public string? BrdCurrentUser { get; set; }

    public string? BrdCurrentStatus { get; set; }

    public decimal? BrdCurrentPid { get; set; }

    public DateOnly? BrdCurrentModifiedDate { get; set; }

    public decimal? BrdVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtRegisteredAnimal> CtRegisteredAnimals { get; set; } = new List<CtRegisteredAnimal>();
}
