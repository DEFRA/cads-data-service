using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveParty
{
    public string Species { get; set; } = null!;

    public string SiteIdentifier { get; set; } = null!;

    public int PartyIdentifier { get; set; }

    public string CollectiveRole { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual AnimalCollectiveRef AnimalCollectiveRef { get; set; } = null!;

    public virtual AnimalCollectiveRole CollectiveRoleNavigation { get; set; } = null!;

    public virtual AnimalPartyRef PartyIdentifierNavigation { get; set; } = null!;
}