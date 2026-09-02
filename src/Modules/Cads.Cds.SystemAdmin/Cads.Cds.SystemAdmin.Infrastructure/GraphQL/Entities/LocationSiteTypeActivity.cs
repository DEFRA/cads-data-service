using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteTypeActivity
{
    public string SiteType { get; set; } = null!;

    public string Activity { get; set; } = null!;

    public virtual LocationActivity ActivityNavigation { get; set; } = null!;

    public virtual ICollection<LocationSiteActivity> LocationSiteActivities { get; set; } = new List<LocationSiteActivity>();

    public virtual LocationSiteType SiteTypeNavigation { get; set; } = null!;
}
