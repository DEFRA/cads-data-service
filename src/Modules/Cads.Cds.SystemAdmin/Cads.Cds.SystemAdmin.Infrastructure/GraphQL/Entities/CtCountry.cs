using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCountry
{
    public decimal CryId { get; set; }

    public string? CryCode { get; set; }

    public string? CryName { get; set; }

    public string? CryEuMember { get; set; }

    public string? CryImportExport { get; set; }

    public decimal? CryCryIdMainEu { get; set; }

    public string? CryBackCapture { get; set; }

    public string? CryCurrentUser { get; set; }

    public string? CryCurrentStatus { get; set; }

    public DateOnly? CryCurrentModifiedDate { get; set; }

    public decimal? CryCurrentPid { get; set; }

    public decimal? CryVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtCountry? CryCryIdMainEuNavigation { get; set; }

    public virtual ICollection<CtRegisteredAnimal> CtRegisteredAnimals { get; set; } = new List<CtRegisteredAnimal>();

    public virtual ICollection<CtRegisteredMovement> CtRegisteredMovements { get; set; } = new List<CtRegisteredMovement>();

    public virtual ICollection<CtCountry> InverseCryCryIdMainEuNavigation { get; set; } = new List<CtCountry>();
}
