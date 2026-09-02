using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalParty
{
    public string AnimalIdentifier { get; set; } = null!;

    public int PartyIdentifier { get; set; }

    public string AnimalRole { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalRole AnimalRoleNavigation { get; set; } = null!;

    public virtual AnimalPartyRef PartyIdentifierNavigation { get; set; } = null!;
}
