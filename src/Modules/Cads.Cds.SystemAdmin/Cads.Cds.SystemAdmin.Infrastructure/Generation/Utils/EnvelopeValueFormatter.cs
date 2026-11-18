using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using System;
using System.Globalization;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public sealed class EnvelopeValueFormatter : IEnvelopeValueFormatter
{
    /// <summary>H and T record datetime, e.g. <c>07112020 13:01:02</c>.</summary>
    public const string EnvelopeDateTimeFormat = "ddMMyyyy HH:mm:ss";

    /// <summary>Business column dates, Oracle <c>DD-MON-YY</c>, e.g. <c>08-NOV-18</c>.</summary>
    public const string FieldDateFormat = "dd-MMM-yy";

    /// <summary>Delta aud datetime, Oracle <c>DD-MON-YYYY HH24:MI:SS</c>, e.g. <c>08-NOV-2020 13:18:31</c>.</summary>
    public const string AudDateTimeFormat = "dd-MMM-yyyy HH:mm:ss";

    public string FormatValue(object? value) => value switch
    {
        null => string.Empty,
        string s => s,
        DateOnly d => FormatDate(d.ToString(FieldDateFormat, CultureInfo.InvariantCulture)),
        DateTime dt => FormatDate(dt.ToString(FieldDateFormat, CultureInfo.InvariantCulture)),
        DateTimeOffset dto => FormatDate(dto.ToString(FieldDateFormat, CultureInfo.InvariantCulture)),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty
    };

    public string FormatAudDateTime(DateTime value) =>
        value.ToString(AudDateTimeFormat, CultureInfo.InvariantCulture).ToUpperInvariant();

    public string FormatEnvelopeDateTime(DateTime value) =>
        value.ToString(EnvelopeDateTimeFormat, CultureInfo.InvariantCulture);

    private static string FormatDate(string formatted) => formatted.ToUpperInvariant();
}