using System;
using System.Collections.Generic;
using System.Text;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

/// <summary>
/// Writes pipe-delimited, CRLF-terminated vendor extract records. Shared by the bulk and delta layouts.
/// </summary>
/// <remarks>
/// The staging import reads the D records with <c>COPY ... (FORMAT csv, DELIMITER '|')</c>, and the vendor format
/// has no quoting. A value containing <c>|</c>, <c>"</c>, CR or LF can't be represented, so it is rejected.
/// </remarks>
public sealed class EnvelopeWriter
{
    public const char Separator = '|';

    public const string LineEnding = "\r\n";

    private readonly StringBuilder _sb = new();

    /// <summary>Writes one record. Each field must already be formatted; it is checked before writing.</summary>
    public EnvelopeWriter WriteRecord(IEnumerable<string> fields)
    {
        var first = true;

        foreach (var field in fields)
        {
            EnsureRepresentable(field);

            if (!first)
                _sb.Append(Separator);

            _sb.Append(field);
            first = false;
        }

        _sb.Append(LineEnding);

        return this;
    }

    public override string ToString() => _sb.ToString();

    private static void EnsureRepresentable(string field)
    {
        if (field.AsSpan().IndexOfAny(['|', '"', '\r', '\n']) >= 0)
        {
            throw new ArgumentException(
                $"Value '{field.ReplaceLineEndings("\\n")}' contains '|', '\"', CR or LF, which the vendor extract format can't represent.");
        }
    }
}
