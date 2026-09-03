using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationAssociatedSite
{
    public string SiteIdentifier { get; set; } = null!;

    public string AssociatedSiteIdentifier { get; set; } = null!;

    public string AssociatedSiteType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual LocationSite AssociatedSiteIdentifierNavigation { get; set; } = null!;

    public virtual LocationAssociatedSiteType AssociatedSiteTypeNavigation { get; set; } = null!;

    public virtual LocationSite SiteIdentifierNavigation { get; set; } = null!;
}