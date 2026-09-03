using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtFileLayout
{
    public decimal? FltId { get; set; }

    public string? FltProcessName { get; set; }

    public string? FltRecordType { get; set; }

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

    public long? TransId { get; set; }
}