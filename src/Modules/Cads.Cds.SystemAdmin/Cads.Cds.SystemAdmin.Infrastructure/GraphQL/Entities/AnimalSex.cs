using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalSex
{
    public string Sex { get; set; } = null!;

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
