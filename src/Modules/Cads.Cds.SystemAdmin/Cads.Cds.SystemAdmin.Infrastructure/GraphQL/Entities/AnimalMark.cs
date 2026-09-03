using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalMark
{
    public string Species { get; set; } = null!;

    public string CollectiveSiteIdentifier { get; set; } = null!;

    public string Mark { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<AnimalBirth> AnimalBirths { get; set; } = new List<AnimalBirth>();

    public virtual ICollection<AnimalCollectiveDeath> AnimalCollectiveDeaths { get; set; } = new List<AnimalCollectiveDeath>();

    public virtual AnimalCollectiveRef AnimalCollectiveRef { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveRegistration> AnimalCollectiveRegistrations { get; set; } = new List<AnimalCollectiveRegistration>();
}