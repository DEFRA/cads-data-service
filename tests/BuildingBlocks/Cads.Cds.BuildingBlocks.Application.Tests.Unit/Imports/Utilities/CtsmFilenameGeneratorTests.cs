using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Application.Schema;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Application.Tests.Unit.Imports.Utilities;

public class CtsmFilenameGeneratorTests
{
    private static readonly DateTimeOffset FixedNow = new(2026, 09, 21, 14, 32, 05, TimeSpan.Zero);

    public static TheoryData<ImportDataType> AllDataTypes()
    {
        var data = new TheoryData<ImportDataType>();

        foreach (var dataType in Enum.GetValues<ImportDataType>().Where(t => t != ImportDataType.None))
        {
            data.Add(dataType);
        }

        return data;
    }

    [Theory]
    [InlineData(ImportActionType.Bulk, ImportDataType.CtLocations, "BULK", "CT_LOCATIONS")]
    [InlineData(ImportActionType.Delta, ImportDataType.CtAddresses, "DELTA", "CT_ADDRESSES")]
    [InlineData(ImportActionType.Bulk, ImportDataType.CtRegisteredAnimals, "BULK", "CT_REGISTERED_ANIMALS")]
    public void Generate_Should_Produce_Filename_That_RoundTrips_Through_TryParse(
        ImportActionType actionType,
        ImportDataType dataType,
        string expectedType,
        string expectedTableName)
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        var filename = generator.Generate(new CtsmFilenameRequest(actionType, dataType) { BatchId = 7, PartNo = 42 });

        filename.Should().Be($"CTSM_CADS_ETE_{expectedType}_00007_042_{expectedTableName}_2026-09-21-143205.csv");

        CtsmFilenameParser.TryParse(filename, out var parsed).Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.App.Should().Be("CADS");
        parsed.Env.Should().Be("ETE");
        parsed.Type.Should().Be(expectedType);
        parsed.BatchId.Should().Be("00007");
        parsed.PartNo.Should().Be("042");
        parsed.TableName.Should().Be(expectedTableName);
        parsed.Timestamp.Should().Be("2026-09-21-143205");
    }

    [Theory]
    [MemberData(nameof(AllDataTypes))]
    public void Generate_Should_RoundTrip_For_Every_ImportDataType(ImportDataType dataType)
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        var tableName = dataType.GetTableName(SchemaName.CtsTransactions);

        var filename = generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, dataType));

        CtsmFilenameParser.TryParse(filename, out var parsed).Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.TableName.Should().Be(tableName!.ToUpperInvariant());
        parsed.GetDestinationTableName().Should().Be($"cts_transactions.{tableName}");
    }

    [Fact]
    public void Generate_Should_Pad_BatchId_And_PartNo_To_The_Supported_Ranges()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        var lowest = generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties) { BatchId = 1, PartNo = 1 });
        var highest = generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties) { BatchId = 99999, PartNo = 999 });

        lowest.Should().Be("CTSM_CADS_ETE_BULK_00001_001_CT_PARTIES_2026-09-21-143205.csv");
        highest.Should().Be("CTSM_CADS_ETE_BULK_99999_999_CT_PARTIES_2026-09-21-143205.csv");
    }

    [Fact]
    public void Generate_Should_Return_Unique_Filenames_For_Rapid_Successive_Calls()
    {
        // A fixed clock is the worst case for uniqueness: every call shares a timestamp.
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        var request = new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties);

        var filenames = Enumerable.Range(0, 5000).Select(_ => generator.Generate(request)).ToList();

        filenames.Should().OnlyHaveUniqueItems();
        filenames.Should().AllSatisfy(f => CtsmFilenameParser.TryParse(f, out _).Should().BeTrue());
    }

    [Fact]
    public void Generate_Should_Return_Unique_Filenames_When_Called_Concurrently()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        var request = new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties);
        var filenames = new string[2000];

        Parallel.For(0, filenames.Length, i => filenames[i] = generator.Generate(request));

        filenames.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void Generate_Should_Allocate_Sequential_BatchIds_When_None_Supplied()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        var request = new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties);

        var batchIds = Enumerable.Range(0, 3)
            .Select(_ => CtsmFilenameParser.Parse(generator.Generate(request))!.BatchId)
            .ToList();

        batchIds.Should().Equal("00001", "00002", "00003");
    }

    [Fact]
    public void Generate_Should_Reuse_Previous_Filename_When_Flag_Is_Set()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        var previous = generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties));

        var reissued = generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties)
        {
            ReusePreviousFilename = true,
            PreviousFilename = previous
        });

        reissued.Should().Be(previous);
    }

    [Fact]
    public void Generate_Should_Reuse_Previous_Filename_Regardless_Of_The_Other_Request_Values()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));
        const string previous = "CTSM_CADS_PREP_BULK_00001_001_CT_LOCATIONS_2026-07-28-094638.csv";

        var reissued = generator.Generate(new CtsmFilenameRequest(ImportActionType.Delta, ImportDataType.CtBreeds)
        {
            BatchId = 55,
            PartNo = 9,
            ReusePreviousFilename = true,
            PreviousFilename = previous
        });

        reissued.Should().Be(previous);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Generate_Should_Throw_When_Reuse_Requested_Without_A_Previous_Filename(string? previousFilename)
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        Action act = () => generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties)
        {
            ReusePreviousFilename = true,
            PreviousFilename = previousFilename
        });

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Generate_Should_Throw_When_The_Previous_Filename_Is_Not_A_Ctsm_Filename()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        Action act = () => generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties)
        {
            ReusePreviousFilename = true,
            PreviousFilename = "not-a-ctsm-filename.csv"
        });

        act.Should().Throw<FormatException>()
           .WithMessage("Invalid CTSM filename format*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100000)]
    public void Generate_Should_Throw_When_BatchId_Is_Out_Of_Range(int batchId)
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        Action act = () => generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties) { BatchId = batchId });

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1000)]
    public void Generate_Should_Throw_When_PartNo_Is_Out_Of_Range(int partNo)
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        Action act = () => generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.CtParties) { PartNo = partNo });

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Generate_Should_Throw_When_The_DataType_Has_No_Table()
    {
        var generator = new CtsmFilenameGenerator(new FixedTimeProvider(FixedNow));

        Action act = () => generator.Generate(new CtsmFilenameRequest(ImportActionType.Bulk, ImportDataType.None));

        act.Should().Throw<ArgumentException>()
           .WithMessage("No 'CtsTransactions' table name is defined for import data type 'None'.*");
    }

    private sealed class FixedTimeProvider(DateTimeOffset utcNow) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => utcNow;
    }
}