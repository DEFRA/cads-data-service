using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteIdentifier
{
    public string SiteIdentifier { get; set; } = null!;

    public string IdentifierType { get; set; } = null!;

    public string IdentifierValue { get; set; } = null!;

    public virtual LocationSiteIdentifierType IdentifierTypeNavigation { get; set; } = null!;

    public virtual LocationSite SiteIdentifierNavigation { get; set; } = null!;
}