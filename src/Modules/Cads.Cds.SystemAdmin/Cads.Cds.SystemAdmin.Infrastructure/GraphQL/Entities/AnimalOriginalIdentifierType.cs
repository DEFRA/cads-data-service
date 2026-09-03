using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalOriginalIdentifierType
{
    public string Type { get; set; } = null!;

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; } = new List<AnimalNoticeToIdentify>();
}