using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class LocationPostcode
{
    public string Postcode { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}