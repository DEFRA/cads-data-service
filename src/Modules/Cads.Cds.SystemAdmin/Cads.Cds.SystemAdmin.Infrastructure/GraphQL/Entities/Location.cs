using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class Location
{
    public string Identifier { get; set; } = null!;

    public long? Uprn { get; set; }

    public string? SingleLineAddress { get; set; }

    public string? Postcode { get; set; }

    public string? OsMapReference { get; set; }

    public int? Easting { get; set; }

    public int? Northing { get; set; }

    public string? CountryCode { get; set; }

    public virtual LocationCountry? CountryCodeNavigation { get; set; }

    public virtual ICollection<LocationSite> LocationSites { get; set; } = new List<LocationSite>();

    public virtual LocationPostcode? PostcodeNavigation { get; set; }
}