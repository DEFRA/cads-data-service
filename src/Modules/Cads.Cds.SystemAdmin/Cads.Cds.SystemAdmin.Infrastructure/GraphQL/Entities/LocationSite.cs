using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSite
{
    public string Identifier { get; set; } = null!;

    public string SiteType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LocationIdentifier { get; set; } = null!;

    public string SiteSource { get; set; } = null!;

    public bool DestroyIdentityDocumentsFlag { get; set; }

    public string State { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<LocationAssociatedSite> LocationAssociatedSiteAssociatedSiteIdentifierNavigations { get; set; } = new List<LocationAssociatedSite>();

    public virtual ICollection<LocationAssociatedSite> LocationAssociatedSiteSiteIdentifierNavigations { get; set; } = new List<LocationAssociatedSite>();

    public virtual Location LocationIdentifierNavigation { get; set; } = null!;

    public virtual ICollection<LocationSiteActivity> LocationSiteActivities { get; set; } = new List<LocationSiteActivity>();

    public virtual ICollection<LocationSiteIdentifier> LocationSiteIdentifiers { get; set; } = new List<LocationSiteIdentifier>();

    public virtual ICollection<LocationSiteParty> LocationSiteParties { get; set; } = new List<LocationSiteParty>();

    public virtual LocationSiteSource SiteSourceNavigation { get; set; } = null!;

    public virtual LocationSiteType SiteTypeNavigation { get; set; } = null!;

    public virtual LocationSiteState StateNavigation { get; set; } = null!;
}