using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalLostOrStolenStatus
{
    public string AnimalIdentifier { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string State { get; set; } = null!;

    public string? CrimeReferenceNumber { get; set; }

    public string HomeSiteIdentifier { get; set; } = null!;

    public bool FoundDeadFlag { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalSiteRef HomeSiteIdentifierNavigation { get; set; } = null!;

    public virtual AnimalLostOrStolenState StateNavigation { get; set; } = null!;
}
