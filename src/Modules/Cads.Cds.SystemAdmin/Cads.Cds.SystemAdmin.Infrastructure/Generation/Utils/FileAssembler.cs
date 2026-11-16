using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class FileAssembler(IEnvelopeValueFormatter valueFormatter, IPipelineColumns pipelineColumns) : IFileAssembler
{
    private const string HeaderRecordType = "H";

    private const string ColumnsRecordType = "C";

    private const string DataRecordType = "D";

    private const string TrailerRecordType = "T";

    private const string RecordTypeColumn = "RECORD_TYPE";

    private const string RecordCountColumn = "RECORD_COUNT";

    public string Create(
        string fileName,
        DateTime fileCreatedDateTime,
        EnvelopeTable table,
        IReadOnlyList<IReadOnlyDictionary<string, object?>> rows,
        FileAssemblyOptions options)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(options);

        Validate(table, rows, options);

        var isDelta = options.Layout == FileLayout.Delta;
        var envelopeDateTime = valueFormatter.FormatEnvelopeDateTime(fileCreatedDateTime);
        var writer = new EnvelopeWriter();
        string[] audColumns = isDelta ? [.. pipelineColumns.AudColumns(table.Prefix).Select(c => c.ToUpperInvariant())] : [];

        writer.WriteRecord([HeaderRecordType, fileName, envelopeDateTime]);

        writer.WriteRecord([
            ColumnsRecordType,
            RecordTypeColumn,
            RecordCountColumn,
            .. audColumns,
            .. table.BusinessColumns.Select(c => c.ToUpperInvariant())
        ]);

        var audType = isDelta ? options.AudType! : null;
        var audDateTime = isDelta ? valueFormatter.FormatAudDateTime(options.AudDateTime ?? fileCreatedDateTime) : null;

        for (var i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            string[] audFields = isDelta ? [options.AudIds![i].ToString(CultureInfo.InvariantCulture), audType!, audDateTime!] : [];

            writer.WriteRecord([
                DataRecordType,
                (i + 1).ToString(CultureInfo.InvariantCulture),
                .. audFields,
                .. table.BusinessColumns.Select(c => valueFormatter.FormatValue(row[c]))
            ]);
        }

        writer.WriteRecord([TrailerRecordType, fileName, envelopeDateTime, rows.Count.ToString(CultureInfo.InvariantCulture)]);

        return writer.ToString();
    }

    private static void Validate(
        EnvelopeTable table,
        IReadOnlyList<IReadOnlyDictionary<string, object?>> rows,
        FileAssemblyOptions options)
    {
        if (rows is null || rows.Count == 0)
            throw new ArgumentException("rows must contain at least one row.", nameof(rows));

        if (table.BusinessColumns.Count == 0)
            throw new ArgumentException($"Table '{table.TableName}' has no business columns.", nameof(table));

        switch (options.Layout)
        {
            case FileLayout.Bulk:
                if (options.AudType is not null || options.AudIds is not null || options.AudDateTime is not null)
                    throw new ArgumentException("A bulk file must not carry aud options.", nameof(options));
                break;

            case FileLayout.Delta:
                if (options.AudType is not { } audType || !FileAssemblyOptions.ValidAudTypes.Contains(audType))
                    throw new ArgumentException(
                        $"A delta file needs an aud type of {string.Join(", ", FileAssemblyOptions.ValidAudTypes)}; got '{options.AudType}'.",
                        nameof(options));

                if (options.AudIds is null || options.AudIds.Count != rows.Count)
                    throw new ArgumentException(
                        $"A delta file needs one aud id per row: {rows.Count} rows, {options.AudIds?.Count ?? 0} aud ids.",
                        nameof(options));
                break;

            default:
                throw new ArgumentException($"Unsupported layout '{options.Layout}'.", nameof(options));
        }

        var expected = new HashSet<string>(table.BusinessColumns, StringComparer.Ordinal);

        for (var i = 0; i < rows.Count; i++)
        {
            if (rows[i].Count != expected.Count || !rows[i].Keys.All(expected.Contains))
            {
                var actual = rows[i].Keys.ToHashSet(StringComparer.Ordinal);
                var missing = expected.Except(actual);
                var extra = actual.Except(expected);

                throw new ArgumentException(
                    $"Row {i + 1} doesn't match the business columns of '{table.TableName}'. " +
                    $"Missing: [{string.Join(", ", missing)}]. Unexpected: [{string.Join(", ", extra)}].",
                    nameof(rows));
            }
        }
    }
}
