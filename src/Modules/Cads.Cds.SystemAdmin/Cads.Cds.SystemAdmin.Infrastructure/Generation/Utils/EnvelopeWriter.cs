using Cads.Cds.SystemAdmin.Application.Generation.Utils;
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
public sealed class EnvelopeWriter : IEnvelopeWriter
{
    public const char Separator = '|';

    public const string LineEnding = "\r\n";

    /// <summary>Writes one record. Each field must already be formatted; it is checked before writing.</summary>
    public void WriteRecord(StringBuilder sb, IEnumerable<string> fields)
    {
        ArgumentNullException.ThrowIfNull(sb);

        var first = true;

        foreach (var field in fields)
        {
            EnsureRepresentable(field);

            if (!first)
                sb.Append(Separator);

            sb.Append(field);
            first = false;
        }

        sb.Append(LineEnding);
    }

    private static void EnsureRepresentable(string field)
    {
        if (field.AsSpan().IndexOfAny(['|', '"', '\r', '\n']) >= 0)
        {
            throw new ArgumentException(
                $"Value '{field.ReplaceLineEndings("\\n")}' contains '|', '\"', CR or LF, which the vendor extract format can't represent.");
        }
    }
}