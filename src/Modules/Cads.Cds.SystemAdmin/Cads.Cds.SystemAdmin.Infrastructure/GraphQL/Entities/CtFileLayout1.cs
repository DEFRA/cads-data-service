using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtFileLayout1
{
    public decimal? FltId { get; set; }

    public string? FltProcessName { get; set; }

    public string? FltElementName { get; set; }

    public string? FltElementDesc { get; set; }

    public decimal? FltElementIndex { get; set; }

    public string? FltElementTests { get; set; }

    public string? FltDataType { get; set; }

    public decimal? FltDataLength { get; set; }

    public decimal? FltDataPrecision { get; set; }

    public string? FltFileFormat { get; set; }

    public string? FltConversionFormat { get; set; }

    public string? FltUnidataName { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? FltAudId { get; set; }

    public string? FltAudType { get; set; }

    public DateTime? FltAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public string? FltRecordType { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}