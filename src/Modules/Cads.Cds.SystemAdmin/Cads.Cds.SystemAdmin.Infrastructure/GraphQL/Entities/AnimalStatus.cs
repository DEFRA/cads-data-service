using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalStatus
{
    public string AnimalIdentifier { get; set; } = null!;

    public string AnimalState { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalState AnimalStateNavigation { get; set; } = null!;
}
