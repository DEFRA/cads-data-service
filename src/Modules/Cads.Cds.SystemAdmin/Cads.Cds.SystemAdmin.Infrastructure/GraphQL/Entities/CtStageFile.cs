using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageFile
{
    public decimal StfId { get; set; }

    public string? StfFileName { get; set; }

    public string? StfFileType { get; set; }

    public decimal? StfLineNumber { get; set; }

    public string? StfRecord { get; set; }

    public DateOnly? StfTimestamp { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}