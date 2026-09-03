using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollective
{
    public string AnimalIdentifier { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string HomeCollectiveSiteIdentifier { get; set; } = null!;

    public string CurrentCollectiveSiteIdentifier { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual AnimalCollectiveRef AnimalCollectiveRef { get; set; } = null!;

    public virtual AnimalCollectiveRef AnimalCollectiveRefNavigation { get; set; } = null!;

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;
}