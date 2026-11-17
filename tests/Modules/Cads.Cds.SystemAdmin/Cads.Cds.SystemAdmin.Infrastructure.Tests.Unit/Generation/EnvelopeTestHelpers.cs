using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation;

internal static class EnvelopeTestHelpers
{
    public static CtsTransactionsGQDbContext CreateDbContext(string name)
    {
        var options = new DbContextOptionsBuilder<CtsTransactionsGQDbContext>()
            .UseInMemoryDatabase(name)
            .Options;

        return new CtsTransactionsGQDbContext(options);
    }

    public static ITablePrefixResolver CreateTablePrefixResolver() =>
        new TablePrefixResolver(new EntityTypeStore());

    public static IBusinessColumnResolver CreateBusinessColumnResolver() =>
        new BusinessColumnResolver(CreateTablePrefixResolver(), new PipelineColumns(), new ImportDataTypeLookup(), new EntityTypeStore());

    public static IEnvelopeTableResolver CreateEnvelopeTableResolver() =>
        new EnvelopeTableResolver(CreateTablePrefixResolver(), CreateBusinessColumnResolver(), new EntityTypeStore());

    public static IContentGeneratorFactory CreateContentGeneratorFactory() =>
        new ContentGeneratorFactory(CreateBusinessColumnResolver(), new BusinessRowMapper());

    public static FileAssembler CreateFileAssembler() =>
        new(new EnvelopeValueFormatter(), new PipelineColumns(), new EnvelopeWriter());

    /// <summary>Splits on CRLF; the file ends with CRLF, so the trailing empty entry is dropped.</summary>
    public static string[] Lines(string content)
    {
        Assert.EndsWith("\r\n", content);
        return content[..^2].Split("\r\n");
    }

    public static string[] Fields(string line) => line.Split('|');

    /// <summary>
    /// The Bridge's split: line 1 (H) is skipped, line 2 (C) has its first field dropped and is lower-cased to become
    /// the COPY column list, and T lines are dropped. Mirrors <c>CsvDataFileSplitterStrategyByLines</c>.
    /// </summary>
    public static (string[] Columns, string[] DataLines) BridgeSplit(string content)
    {
        var lines = Lines(content);
        var columns = Fields(lines[1]).Skip(1).Select(c => c.ToLowerInvariant()).ToArray();
        var data = lines.Skip(2).Where(l => !l.StartsWith('T')).ToArray();

        return (columns, data);
    }

    /// <summary>
    /// The Bridge's trailer check against the last non-empty line. Mirrors <c>S3FileMetaDataService.ParseTrailerLine</c>
    /// plus its 4-field expectation.
    /// </summary>
    public static long ParseTrailer(string content, string s3Key)
    {
        var lastLine = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[^1];
        var parts = Fields(lastLine);

        Assert.Equal(4, parts.Length);
        Assert.Equal("T", parts[0].Trim());
        Assert.Equal(Path.GetFileName(s3Key), parts[1].Trim(), ignoreCase: true);
        Assert.True(long.TryParse(parts[3].Trim(), out var count) && count >= 0);

        return count;
    }
}