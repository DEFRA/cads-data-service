using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagFormat
{
    public decimal EtfId { get; set; }

    public string? EtfDescription { get; set; }

    public string? EtfFormatPattern { get; set; }

    public decimal? EtfMaxInputLength { get; set; }

    public string? EtfExtraCharsAllowed { get; set; }

    public string? EtfCurrentUser { get; set; }

    public string? EtfCurrentStatus { get; set; }

    public DateOnly? EtfCurrentModifiedDate { get; set; }

    public decimal? EtfCurrentPid { get; set; }

    public decimal? EtfVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtEartagType> CtEartagTypes { get; set; } = new List<CtEartagType>();
}