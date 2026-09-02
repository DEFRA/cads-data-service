using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteParty
{
    public string SiteIdentifier { get; set; } = null!;

    public long PartyIdentifier { get; set; }

    public string SiteRole { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual LocationPartyRef PartyIdentifierNavigation { get; set; } = null!;

    public virtual LocationSite SiteIdentifierNavigation { get; set; } = null!;

    public virtual LocationSiteRole SiteRoleNavigation { get; set; } = null!;
}
