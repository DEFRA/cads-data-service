using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class AnimalRegistrationCategory
{
    public string Category { get; set; } = null!;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}