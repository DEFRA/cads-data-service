using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;
using FluentAssertions;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Factories;

public class S3BulkLoadCommandFactoryTests
{
    [Fact]
    public void CreateTempTableCommand_ShouldGenerateCorrectSql()
    {
        var cmd = GetFactory().CreateTempTableCommand(BulkLoadDataTypes.Locations);

        cmd.CommandText.Should().Be(
            "CREATE TEMP TABLE \"temp__ct_locations\" (LIKE \"_ct_locations\" INCLUDING ALL) ON COMMIT DROP;");
    }

    [Fact]
    public void GivenInvalidBulkLoadDataType_WhenGetTableNameRequested_ShouldThrow()
    {
        Action act = () => GetFactory().GetTableName(BulkLoadDataTypes.None);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task CreateInsertCommandAsync_ShouldGenerateCorrectSql()
    {
        var cmd = await GetFactory().CreateInsertCommandAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        cmd.CommandText.Should().Be(
            "INSERT INTO \"_ct_locations\" (record_type,record_count,loc_id) SELECT record_type,record_count,loc_id FROM \"temp__ct_locations\"");
    }

    [Fact]
    public async Task CreateUpdateCommandAsync_ShouldGenerateCorrectSql()
    {
        var cmd = await GetFactory().CreateUpdateCommandAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        cmd.CommandText.Should().Be(
            "UPDATE \"_ct_locations\" AS m SET m.record_type = t.record_type, m.record_count = t.record_count, m.loc_id = t.loc_id " +
            "FROM \"temp__ct_locations\" AS t WHERE m.loc_id = t.loc_id");
    }

    [Fact]
    public async Task CreateUpsertCommandAsync_ShouldGenerateCorrectSql()
    {
        var cmd = await GetFactory().CreateUpsertCommandAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        cmd.CommandText.Should().Be(
            "INSERT INTO \"_ct_locations\" (record_type,record_count,loc_id) SELECT record_type,record_count,loc_id FROM \"temp__ct_locations\" " +
            "ON CONFLICT (loc_id) DO UPDATE SET record_type = EXCLUDED.record_type, record_count = EXCLUDED.record_count, loc_id = EXCLUDED.loc_id");
    }

    [Fact]
    public async Task CreateDeleteCommandAsync_ShouldGenerateCorrectSql()
    {
        var cmd = await GetFactory().CreateDeleteCommandAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        cmd.CommandText.Should().Be(
            "DELETE FROM \"_ct_locations\" WHERE loc_id IN (SELECT loc_id FROM \"temp__ct_locations\")");
    }

    [Fact]
    public async Task FilterColumnsToTableAsync_ShouldReturnMatchingColumns()
    {
        var result = await GetFactory().FilterColumnsToTableAsync(
            BulkLoadDataTypes.Locations,
            ["record_type", "record_count", "LOC_ID", "dummy_id"],
            TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(["record_type", "record_count", "LOC_ID"]);
    }

    [Theory]
    [InlineData(false, @"""_ct_locations""")]
    [InlineData(true, @"""temp__ct_locations""")]
    public void GivenValidBulkLoadDataType_WhenGetTableNameRequested_ShouldSucceed(bool isTemp, string expectedValue)
    {
        var result = GetFactory().GetTableName(BulkLoadDataTypes.Locations, isTemp);

        result.Should().Be(expectedValue);
    }

    [Fact]
    public async Task GetColumnNamesAsync_ShouldReturnConfiguredColumns()
    {
        var expected = new[] { "record_type", "record_count", "loc_id" };

        var result = await GetFactory().GetColumnNamesAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateTempTableQueryCommandAsync_ShouldGenerateCorrectSql()
    {
        var cmd = await GetFactory().CreateTempTableQueryCommandAsync(BulkLoadDataTypes.Locations, TestContext.Current.CancellationToken);

        cmd.CommandText.Should().Be(
            "SELECT record_type,record_count,loc_id FROM \"temp__ct_locations\"");
    }

    private static TestableS3BulkLoadCommandFactory GetFactory() =>
        new(new NpgsqlConnection("Host=cads-postgres;Port=5432;Database=cads_data_service;Username=postgres;Password=postgres"), LocationsHeader.Split('|'));

    private static string LocationsHeader =>
        "record_type|record_count|loc_id";
}