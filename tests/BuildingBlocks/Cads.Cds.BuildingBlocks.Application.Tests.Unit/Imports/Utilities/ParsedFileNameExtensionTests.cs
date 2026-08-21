using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Application.Tests.Unit.Imports.Utilities;

public class ParsedFileNameExtensionTests
{
    [Theory]
    [InlineData("CTSM_CADS_PREP_BULK_00001_001_CT_LOCATIONS_2026-07-28-094638.csv", "cts_transactions.ct_locations")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_002_CT_LOCATIONS_2026-07-28-094638.csv", "cts_transactions.ct_locations")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_001_CT_REGISTERED_ANIMALS_2026-07-28-094629.csv", "cts_transactions.ct_registered_animals")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_002_CT_REGISTERED_ANIMALS_2026-07-28-094629.csv", "cts_transactions.ct_registered_animals")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_001_CT_ADDRESSES_2026-07-30-141209.csv", "cts_transactions.ct_addresses")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_002_CT_ADDRESSES_2026-07-30-141209.csv", "cts_transactions.ct_addresses")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_002_CT_BREEDS_2026-07-30-141209.csv", "cts_transactions.ct_breeds")]
    public void GetDestinationTableName_Should_Return_Expected(string filename, string? expected)
    {
        var parsedFile = CtsmFilenameParser.Parse(filename);
        parsedFile.Should().NotBeNull();

        var result = parsedFile.GetDestinationTableName();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("CTSM_CADS_PREP_UNKNOWN_00001_CT_LOCATIONS_2026-07-28-094638.csv")]
    [InlineData("CTSM_CADS_PREP_UNKNOWN_00001_001_CT_LOCATIONS_2026-07-28-094638.csv")]
    public void GetDestinationTableName_Should_Throw_WhenTypeIsInvalid(string filename)
    {
        var parsedFile = CtsmFilenameParser.Parse(filename);

        Action act = () => parsedFile!.GetDestinationTableName();

        act.Should().Throw<UnprocessableException>()
           .WithMessage($"Invalid import action type*");
    }

    [Fact]
    public void GetDestinationTableName_Should_Throw_WhenFilenameIsEmpty()
    {
        Action act = () => CtsmFilenameParser.Parse("")!.GetDestinationTableName();

        act.Should().Throw<FormatException>()
           .WithMessage("Invalid CTSM filename format*");
    }

    [Theory]
    [InlineData("CTSM_CADS_PREP_BULK_00001_001_CT_LOCATIONS_2026-07-28-094638.csv", "CTSM_CADS_PREP_BULK_00001_CT_LOCATIONS")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_002_CT_LOCATIONS_2026-07-28-094638.csv", "CTSM_CADS_PREP_BULK_00001_CT_LOCATIONS")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_001_CT_REGISTERED_ANIMALS_2026-07-28-094629.csv", "CTSM_CADS_PREP_BULK_00001_CT_REGISTERED_ANIMALS")]
    [InlineData("CTSM_CADS_PREP_BULK_00001_002_CT_REGISTERED_ANIMALS_2026-07-28-094629.csv", "CTSM_CADS_PREP_BULK_00001_CT_REGISTERED_ANIMALS")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_001_CT_ADDRESSES_2026-07-30-141209.csv", "CTSM_CADS_PREP_DELTA_00002_CT_ADDRESSES")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_002_CT_ADDRESSES_2026-07-30-141209.csv", "CTSM_CADS_PREP_DELTA_00002_CT_ADDRESSES")]
    public void GetGroupKey_Should_Return_Expected(string filename, string expected)
    {
        var parsedFile = CtsmFilenameParser.Parse(filename);
        parsedFile.Should().NotBeNull();

        var result = parsedFile.GetGroupKey();
        result.Should().Be(expected);
    }
}