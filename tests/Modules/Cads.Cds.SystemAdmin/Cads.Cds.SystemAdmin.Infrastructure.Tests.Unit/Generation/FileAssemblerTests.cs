using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Entities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using System.Globalization;
using static Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation.EnvelopeTestHelpers;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation;

public class FileAssemblerTests
{
    private const string BulkFileName = "CTSM_CADS_PREP_BULK_00001_CT_LOCATIONS_2026-11-14-101500.CSV";
    private const string DeltaFileName = "CTSM_CADS_PREP_DELTA_00002_CT_LOCATIONS_2026-11-14-101500.CSV";

    private static readonly DateTime Created = new(2026, 11, 14, 10, 15, 0, DateTimeKind.Utc);

    private static readonly IEnvelopeTableResolver TableResolver = CreateEnvelopeTableResolver();
    private static readonly IBusinessColumnResolver ColumnResolver = CreateBusinessColumnResolver();
    private static readonly BusinessRowMapper RowMapper = new();

    private readonly FileAssembler _assembler = CreateFileAssembler();
    private readonly EnvelopeValueFormatter _formatter = new();
    private readonly PipelineColumns _pipelineColumns = new();

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static (EnvelopeTable Table, IReadOnlyList<IReadOnlyDictionary<string, object?>> Rows) Rows<T>(params T[] entities)
        where T : class, new()
    {
        using var db = CreateDbContext($"{typeof(T).Name}-{Guid.NewGuid()}");
        var table = TableResolver.Resolve(db.Model, typeof(T));
        var columns = ColumnResolver.Resolve(db.Model.FindEntityType(typeof(T))!);

        return (table, [.. entities.Select(e => RowMapper.Map(e, columns))]);
    }

    private static CtLocation Location(decimal id, string? lastColumnValue = "1") => new()
    {
        // Pipeline columns are set to prove they never reach the file.
        TransId = 99,
        TransType = "X",
        RowNumber = 7,
        FakeData = 1,
        RecordType = "Z",
        RecordCount = 42,
        LocAudId = 123,
        LocAudType = "Q",
        LocAudDatetime = new DateTime(2001, 1, 1),
        ImportedDate = new DateTime(2001, 1, 1),
        CtsFileImportId = 5,

        LocId = id,
        LocReceivePpafFlag = "Y",
        LocSltId = 1m,
        LocEffectiveFrom = new DateOnly(2018, 11, 8),
        LocEffectiveTo = null,
        LocCurrentModifiedDate = new DateOnly(2020, 3, 1),
        LocPremisesType = "FARM",
        LocVersion = lastColumnValue is null ? null : decimal.Parse(lastColumnValue, CultureInfo.InvariantCulture)
    };

    private string Bulk(params CtLocation[] locations)
    {
        var (table, rows) = Rows(locations);
        return _assembler.Create(BulkFileName, Created, table, rows, FileAssemblyOptions.Bulk());
    }

    private string Delta(string audType, params CtLocation[] locations)
    {
        var (table, rows) = Rows(locations);
        var audIds = Enumerable.Range(0, locations.Length).Select(i => 5000L + i).ToList();
        return _assembler.Create(DeltaFileName, Created, table, rows,
            FileAssemblyOptions.Delta(audType, audIds, new DateTime(2020, 11, 8, 13, 18, 31)));
    }

    // ── Vendor fidelity ──────────────────────────────────────────────────────

    // The CT_ADDRESSES records from the CTSM extract specification.
    private const string VendorBulkColumns =
        "C|RECORD_TYPE|RECORD_COUNT|ADR_ID|ADR_LOC_ID|ADR_PAR_ID|ADR_NAME|ADR_ADDRESS_2|ADR_ADDRESS_3|ADR_ADDRESS_4|ADR_ADDRESS_5|ADR_POST_CODE|ADR_CURRENT_MODIFIED_DATE|ADR_CURRENT_STATUS|ADR_CURRENT_USER|ADR_CURRENT_PID|ADR_VERSION";

    private const string VendorBulkData =
        "D|294357|12933985|25805728||DUMMY NAME|DUMMY ADR LINE 1|DUMMY ADR LINE 2|DUMMY ADR LINE 3|DUMMY ADR LINE 4|DUMMY POSTCODE|08-NOV-18|1|x988886|29|1";

    private const string VendorDeltaColumns =
        "C|RECORD_TYPE|RECORD_COUNT|ADR_AUD_ID|ADR_AUD_TYPE|ADR_AUD_DATETIME|ADR_ID|ADR_LOC_ID|ADR_PAR_ID|ADR_NAME|ADR_ADDRESS_2|ADR_ADDRESS_3|ADR_ADDRESS_4|ADR_ADDRESS_5|ADR_POST_CODE|ADR_CURRENT_MODIFIED_DATE|ADR_CURRENT_STATUS|ADR_CURRENT_USER|ADR_CURRENT_PID|ADR_VERSION";

    private const string VendorDeltaData =
        "D|5|966936|U|08-NOV-2020 13:18:31|12933985|25805728||DUMMY NAME|DUMMY ADR LINE 1|DUMMY ADR LINE 2|DUMMY ADR LINE 3|DUMMY ADR LINE 4|DUMMY POSTCODE|08-NOV-18|1|x988886|29|1";

    private static CtAddress VendorAddress() => new()
    {
        TransType = "B",
        AdrId = 12933985,
        AdrLocId = 25805728,
        AdrParId = null,
        AdrName = "DUMMY NAME",
        AdrAddress2 = "DUMMY ADR LINE 1",
        AdrAddress3 = "DUMMY ADR LINE 2",
        AdrAddress4 = "DUMMY ADR LINE 3",
        AdrAddress5 = "DUMMY ADR LINE 4",
        AdrPostCode = "DUMMY POSTCODE",
        AdrCurrentModifiedDate = new DateOnly(2018, 11, 8),
        AdrCurrentStatus = "1",
        AdrCurrentUser = "x988886",
        AdrCurrentPid = 29,
        AdrVersion = 1
    };

    // RECORD_COUNT is the sequence within the file, so compare everything else.
    private static string WithoutSequence(string dataLine)
    {
        var f = Fields(dataLine);
        return string.Join('|', f.Take(1).Concat(f.Skip(2)));
    }

    [Fact]
    public void Bulk_MatchesTheVendorSample()
    {
        var (table, rows) = Rows(VendorAddress());
        var lines = Lines(_assembler.Create("CTSM_CLA_PROD_BULK_00001_CT_ADDRESSES_2020-11-07-130102.csv", Created, table, rows, FileAssemblyOptions.Bulk()));

        Assert.Equal(VendorBulkColumns, lines[1]);
        Assert.Equal(WithoutSequence(VendorBulkData), WithoutSequence(lines[2]));
    }

    [Fact]
    public void Delta_MatchesTheVendorSample()
    {
        var (table, rows) = Rows(VendorAddress());
        var lines = Lines(_assembler.Create("CTSM_CLA_PROD_DELTA_00002_CT_ADDRESSES_2020-11-08-131831.csv", Created, table, rows,
            FileAssemblyOptions.Delta("U", [966936], new DateTime(2020, 11, 8, 13, 18, 31))));

        Assert.Equal(VendorDeltaColumns, lines[1]);
        Assert.Equal(WithoutSequence(VendorDeltaData), WithoutSequence(lines[2]));
    }

    // ── General structure (ACs 1-9) ──────────────────────────────────────────

    public static TheoryData<string> Layouts => ["bulk", "delta"];

    private string Assemble(string layout, params CtLocation[] locations) =>
        layout == "bulk" ? Bulk(locations) : Delta("U", locations);

    private static string FileNameFor(string layout) => layout == "bulk" ? BulkFileName : DeltaFileName;

    [Theory, MemberData(nameof(Layouts))]
    public void File_IsHeaderColumnsDataTrailer_InOrder(string layout)
    {
        var lines = Lines(Assemble(layout, Location(1), Location(2), Location(3)));

        Assert.Equal(6, lines.Length);
        Assert.Equal(["H", "C", "D", "D", "D", "T"], lines.Select(l => Fields(l)[0]));
    }

    [Theory, MemberData(nameof(Layouts))]
    public void EveryLine_EndsWithCrLf_IncludingTheLast_AndThereIsNoBareLf(string layout)
    {
        var content = Assemble(layout, Location(1), Location(2));

        Assert.EndsWith("\r\n", content);
        Assert.Equal(content.Count(c => c == '\n'), content.Split("\r\n").Length - 1);
        Assert.Equal(content.Count(c => c == '\r'), content.Count(c => c == '\n'));
    }

    [Theory, MemberData(nameof(Layouts))]
    public void RecordCount_IsOneBasedAndIncrementing(string layout)
    {
        var lines = Lines(Assemble(layout, Location(10), Location(20), Location(30)));

        Assert.Equal(["1", "2", "3"], lines.Where(l => l.StartsWith('D')).Select(l => Fields(l)[1]));
    }

    [Theory, MemberData(nameof(Layouts))]
    public void HeaderAndTrailer_CarryTheFileName_AndTrailerCountsTheDataRows(string layout)
    {
        var content = Assemble(layout, Location(1), Location(2));
        var lines = Lines(content);
        var header = Fields(lines[0]);

        Assert.Equal(3, header.Length);
        Assert.Equal(FileNameFor(layout), header[1]);
        Assert.Equal(FileNameFor(layout), Fields(lines[^1])[1]);
        Assert.Equal(2, ParseTrailer(content, $"cads/cts/{layout}/{FileNameFor(layout)}"));
        Assert.DoesNotContain("PART", FileNameFor(layout));
    }

    [Theory, MemberData(nameof(Layouts))]
    public void EveryDataRow_HasOneFieldFewerThanTheColumnsRecord_EvenWithTrailingNulls(string layout)
    {
        var lines = Lines(Assemble(layout, Location(1), Location(2, lastColumnValue: null)));
        var columnCount = Fields(lines[1]).Length;

        Assert.All(lines.Where(l => l.StartsWith('D')), l => Assert.Equal(columnCount - 1, Fields(l).Length));
        Assert.EndsWith("|", lines[3]);
    }

    [Theory, MemberData(nameof(Layouts))]
    public void ColumnNames_AfterTheBridgeSplit_AreDistinctRealColumns_WithEnvelopeColumnsOnce(string layout)
    {
        using var db = CreateDbContext(nameof(ColumnNames_AfterTheBridgeSplit_AreDistinctRealColumns_WithEnvelopeColumnsOnce) + layout);
        var entityType = db.Model.FindEntityType(typeof(CtLocation))!;
        var store = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table("ct_locations", "cts_transactions");
        var realColumns = entityType.GetProperties()
            .Select(p => Microsoft.EntityFrameworkCore.RelationalPropertyExtensions.GetColumnName(p, store))
            .ToHashSet();

        var (columns, dataLines) = BridgeSplit(Assemble(layout, Location(1), Location(2)));

        Assert.Equal(columns.Length, columns.Distinct().Count());
        Assert.All(columns, c => Assert.Contains(c, realColumns));
        Assert.All(dataLines, l => Assert.Equal(columns.Length, Fields(l).Length));

        var envelope = layout == "bulk"
            ? new[] { "record_type", "record_count" }
            : ["record_type", "record_count", "loc_aud_id", "loc_aud_type", "loc_aud_datetime"];

        Assert.Equal(envelope, columns.Take(envelope.Length));
        Assert.DoesNotContain(columns.Skip(envelope.Length), c => _pipelineColumns.IsExcluded(c, "LOC"));
    }

    [Theory, MemberData(nameof(Layouts))]
    public void PipelineValuesOnTheEntity_NeverReachTheFile(string layout)
    {
        var dataFields = Fields(Lines(Assemble(layout, Location(1)))[2]);

        // TransType "X", RecordType "Z" and LocAudType "Q" are only on the entity's pipeline columns.
        Assert.DoesNotContain("X", dataFields);
        Assert.DoesNotContain("Z", dataFields);
        Assert.DoesNotContain("Q", dataFields);
        Assert.Equal("D", dataFields[0]);
    }

    // ── Bulk (ACs 11-14) ─────────────────────────────────────────────────────

    [Fact]
    public void Bulk_ColumnsRecord_HasEnvelopeColumnsThenKeyAtPosition4_AndNoAudColumns()
    {
        var c = Fields(Lines(Bulk(Location(1)))[1]);

        Assert.Equal(["C", "RECORD_TYPE", "RECORD_COUNT", "LOC_ID", "LOC_RECEIVE_PPAF_FLAG"], c.Take(5));
        Assert.DoesNotContain(c, f => f.Contains("_AUD_"));
    }

    [Fact]
    public void Bulk_DataRecord_HasTheKeyValueAtPosition3()
    {
        var d = Fields(Lines(Bulk(Location(12345)))[2]);

        Assert.Equal(["D", "1", "12345", "Y"], d.Take(4));
    }

    [Fact]
    public void Bulk_DatesUseDdMonYy()
    {
        var lines = Lines(Bulk(Location(1)));
        var c = Fields(lines[1]);
        var d = Fields(lines[2]);

        Assert.Equal("08-NOV-18", d[Array.IndexOf(c, "LOC_EFFECTIVE_FROM") - 1]);
        Assert.Equal("01-MAR-20", d[Array.IndexOf(c, "LOC_CURRENT_MODIFIED_DATE") - 1]);
        Assert.Equal("", d[Array.IndexOf(c, "LOC_EFFECTIVE_TO") - 1]);
    }

    // ── Delta (ACs 15-19) ────────────────────────────────────────────────────

    [Fact]
    public void Delta_ColumnsRecord_HasAudColumnsAtPositions4To6_AndKeyAtPosition7()
    {
        var c = Fields(Lines(Delta("U", Location(1)))[1]);

        Assert.Equal(["C", "RECORD_TYPE", "RECORD_COUNT", "LOC_AUD_ID", "LOC_AUD_TYPE", "LOC_AUD_DATETIME", "LOC_ID"], c.Take(7));
        Assert.Single(c, "LOC_AUD_ID");
    }

    [Theory]
    [InlineData("U")]
    [InlineData("D")]
    [InlineData("I")]
    public void Delta_DataRecord_HasAudValuesAt3To5_AndKeyValueAtPosition6(string audType)
    {
        var lines = Lines(Delta(audType, Location(777), Location(778)));
        var d1 = Fields(lines[2]);
        var d2 = Fields(lines[3]);

        Assert.Equal(["D", "1", "5000", audType, "08-NOV-2020 13:18:31", "777"], d1.Take(6));
        Assert.Equal(["D", "2", "5001", audType, "08-NOV-2020 13:18:31", "778"], d2.Take(6));
    }

    [Fact]
    public void Delta_AudDateTimeUsesDdMonYyyy_WhileOtherDatesOnTheRowUseDdMonYy()
    {
        var lines = Lines(Delta("U", Location(1)));
        var c = Fields(lines[1]);
        var d = Fields(lines[2]);

        Assert.Equal("08-NOV-2020 13:18:31", d[Array.IndexOf(c, "LOC_AUD_DATETIME") - 1]);
        Assert.Equal("08-NOV-18", d[Array.IndexOf(c, "LOC_EFFECTIVE_FROM") - 1]);
    }

    [Fact]
    public void Delta_AudDateTime_DefaultsToTheFileCreatedTime()
    {
        var (table, rows) = Rows(Location(1));
        var content = _assembler.Create(DeltaFileName, Created, table, rows, FileAssemblyOptions.Delta("U", [1]));

        Assert.Equal("14-NOV-2026 10:15:00", Fields(Lines(content)[2])[4]);
    }

    // ── Validation ───────────────────────────────────────────────────────────

    [Fact]
    public void Rejects_EmptyRows()
    {
        var (table, _) = Rows(Location(1));

        Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, [], FileAssemblyOptions.Bulk()));
        Assert.Throws<ArgumentException>(() => _assembler.Create(DeltaFileName, Created, table, [], FileAssemblyOptions.Delta("U", [])));
    }

    [Fact]
    public void Rejects_DeltaWithTheWrongNumberOfAudIds()
    {
        var (table, rows) = Rows(Location(1), Location(2));

        Assert.Throws<ArgumentException>(() => _assembler.Create(DeltaFileName, Created, table, rows, FileAssemblyOptions.Delta("U", [1])));
        Assert.Throws<ArgumentException>(() => _assembler.Create(DeltaFileName, Created, table, rows, new FileAssemblyOptions(FileLayout.Delta, "U")));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("X")]
    [InlineData("u")]
    public void Rejects_DeltaWithAMissingOrInvalidAudType(string? audType)
    {
        var (table, rows) = Rows(Location(1));

        Assert.Throws<ArgumentException>(() => _assembler.Create(DeltaFileName, Created, table, rows,
            new FileAssemblyOptions(FileLayout.Delta, audType, [1])));
    }

    [Fact]
    public void Rejects_BulkCarryingAudOptions()
    {
        var (table, rows) = Rows(Location(1));

        Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, rows, new FileAssemblyOptions(FileLayout.Bulk, AudType: "U")));
        Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, rows, new FileAssemblyOptions(FileLayout.Bulk, AudIds: [1])));
        Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, rows, new FileAssemblyOptions(FileLayout.Bulk, AudDateTime: Created)));
    }

    [Fact]
    public void Rejects_RowsThatDontMatchTheBusinessColumns()
    {
        var (table, rows) = Rows(Location(1));

        var extra = new Dictionary<string, object?>(rows[0]) { ["trans_type"] = "B" };
        var missing = new Dictionary<string, object?>(rows[0]);
        missing.Remove("loc_id");

        var ex = Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, [extra], FileAssemblyOptions.Bulk()));
        Assert.Contains("trans_type", ex.Message);
        Assert.Throws<ArgumentException>(() => _assembler.Create(BulkFileName, Created, table, [missing], FileAssemblyOptions.Bulk()));
    }

    // ── Formatting ───────────────────────────────────────────────────────────

    [Theory]
    [InlineData("de-DE")]
    [InlineData("en-GB")]
    [InlineData("fr-FR")]
    public void Formatting_IsInvariant_WhateverTheCurrentCulture(string culture)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(culture);

            Assert.Equal("1.5", _formatter.FormatValue(1.5m));
            Assert.Equal("1234567.25", _formatter.FormatValue(1234567.25m));
            Assert.Equal("2.5", _formatter.FormatValue(2.5d));
            Assert.Equal("08-NOV-18", _formatter.FormatValue(new DateOnly(2018, 11, 8)));
            Assert.Equal("08-NOV-18", _formatter.FormatValue(new DateTime(2018, 11, 8, 23, 59, 0)));
            Assert.Equal("08-NOV-18", _formatter.FormatValue(new DateTimeOffset(2018, 11, 8, 1, 0, 0, TimeSpan.Zero)));
            Assert.Equal("08-NOV-2020 13:18:31", _formatter.FormatAudDateTime(new DateTime(2020, 11, 8, 13, 18, 31)));
            Assert.Equal("07112020 13:01:02", _formatter.FormatEnvelopeDateTime(new DateTime(2020, 11, 7, 13, 1, 2)));
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData(1, "JAN")]
    [InlineData(5, "MAY")]
    [InlineData(9, "SEP")]
    [InlineData(12, "DEC")]
    public void Months_AreUpperCase(int month, string expected)
    {
        Assert.Equal($"01-{expected}-24", _formatter.FormatValue(new DateOnly(2024, month, 1)));
    }

    [Fact]
    public void Nulls_AreEmptyFields_NeverTheWordNull()
    {
        var content = Bulk(Location(1, lastColumnValue: null));

        Assert.Equal(string.Empty, _formatter.FormatValue(null));
        Assert.DoesNotContain("NULL", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("||", Lines(content)[2]);
    }

    [Theory]
    [InlineData("a|b")]
    [InlineData("say \"hi\"")]
    [InlineData("line\r")]
    [InlineData("line\nbreak")]
    public void Values_ThatTheFormatCantRepresent_Throw(string value)
    {
        var location = Location(1);
        location.LocComments = value;

        Assert.Throws<ArgumentException>(() => Bulk(location));
    }
}
