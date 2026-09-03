using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtExtNiDistrict
{
    public string? NidElectoralDistrict { get; set; }

    public decimal? NidVersion { get; set; }

    public string? NidHerdCode { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}