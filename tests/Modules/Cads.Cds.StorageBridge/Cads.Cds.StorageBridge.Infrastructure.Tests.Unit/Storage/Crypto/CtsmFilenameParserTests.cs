using Cads.Cds.StorageBridge.Infrastructure.Storage.Crypto;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Storage.Crypto;

public class CtsmFilenameParserTests
{
    [Fact]
    public void TryParse_returns_the_expected_fields_for_the_new_format()
    {
        var result = CtsmFilenameParser.TryParse("CTSM_UKV_PROD_BULK_123456_1_CT_REGISTERED_MOVEMENTS_2026-02-22-074603.csv", out var ctsmFilename);

        result.Should().BeTrue();
        ctsmFilename.Should().NotBeNull();
        ctsmFilename!.App.Should().Be("UKV");
        ctsmFilename.Env.Should().Be("PROD");
        ctsmFilename.Type.Should().Be("BULK");
        ctsmFilename.BatchId.Should().Be("123456");
        ctsmFilename.PartNo.Should().Be("1");
        ctsmFilename.TableName.Should().Be("CT_REGISTERED_MOVEMENTS");
        ctsmFilename.Timestamp.Should().Be("2026-02-22-074603");
    }

    [Theory]
    [InlineData("report-2026-07-30.csv")]
    [InlineData("CTSM_incomplete.csv")]
    [InlineData("")]
    public void TryParse_rejects_names_that_are_not_ctsm_exports(string filename)
    {
        CtsmFilenameParser.TryParse(filename, out var ctsmFilename).Should().BeFalse();
        ctsmFilename.Should().BeNull();
    }

    // Password vectors from cads-bridge's CtsmFilenameExtensionsTests.
    [Theory]
    [InlineData("CTSM_UKV_PROD_BULK_######_CT_REGISTERED_ANIMALS_2026-02-22-074603.csv", "2026-02-22_ANIMALS_REGISTERED_CT_######_BULK_PROD_UKV_CTSM")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_CT_ANIMAL_STATUSES_2026-07-30-141209.csv", "2026-07-30_STATUSES_ANIMAL_CT_00002_DELTA_PREP_CADS_CTSM")]
    [InlineData("CTSM_CADS_PREP_DELTA_00002_001_CT_ANIMAL_STATUSES_2026-07-30-141209.csv", "2026-07-30_STATUSES_ANIMAL_CT_001_00002_DELTA_PREP_CADS_CTSM")]
    public void DerivePassword_matches_the_cads_bridge_vectors(string filename, string expectedPassword)
    {
        CtsmFilenameParser.TryParse(filename, out var ctsmFilename).Should().BeTrue();

        ctsmFilename!.DerivePassword().Should().Be(expectedPassword);
    }
}