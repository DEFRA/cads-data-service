using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.Fakes.Factories;
using FluentAssertions;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Factories;

public class S3ImportCommandFactoryTests
{
    [Fact]
    public void CreateTempTableCommand_ShouldGenerateCorrectSql()
    {
        var sql = GetFactory().SqlForTempTable(ImportDataType.CtLocations, SchemaName.Public);

        sql.Should().Be(
            "CREATE TEMP TABLE \"temp_ct_locations\" (LIKE \"cts\".\"ct_locations\" INCLUDING ALL) ON COMMIT DROP;");
    }

    [Fact]
    public void GivenInvalidBulkLoadDataType_WhenGetTableNameRequested_ShouldThrow()
    {
        Action act = () => GetFactory().GetTableName(ImportDataType.None, SchemaName.Public);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task CreateInsertCommandAsync_ShouldGenerateCorrectSql()
    {
        var sql = await GetFactory().SqlForInsert(ImportDataType.CtLocations, SchemaName.Cts, TestContext.Current.CancellationToken);

        sql.Should().Be(
            "INSERT INTO \"cts\".\"ct_locations\" (record_type,record_count,loc_id) SELECT record_type,record_count,loc_id FROM \"temp_ct_locations\"");
    }

    [Fact]
    public async Task CreateUpdateCommandAsync_ShouldGenerateCorrectSql()
    {
        var sql = await GetFactory().SqlForUpdate(ImportDataType.CtLocations, SchemaName.Cts, TestContext.Current.CancellationToken);

        sql.Should().Be(
            "UPDATE \"cts\".\"ct_locations\" AS m SET m.record_type = t.record_type, m.record_count = t.record_count, m.loc_id = t.loc_id " +
            "FROM \"temp_ct_locations\" AS t WHERE m.loc_id = t.loc_id");
    }

    [Fact]
    public async Task CreateUpsertCommandAsync_ShouldGenerateCorrectSql()
    {
        var sql = await GetFactory().SqlForUpsert(ImportDataType.CtLocations, SchemaName.Cts, TestContext.Current.CancellationToken);

        sql.Should().Be(
            "INSERT INTO \"cts\".\"ct_locations\" (record_type,record_count,loc_id) SELECT record_type,record_count,loc_id FROM \"temp_ct_locations\" " +
            "ON CONFLICT (loc_id) DO UPDATE SET record_type = EXCLUDED.record_type, record_count = EXCLUDED.record_count, loc_id = EXCLUDED.loc_id");
    }

    [Fact]
    public async Task CreateDeleteCommandAsync_ShouldGenerateCorrectSql()
    {
        var sql = await GetFactory().SqlForDelete(ImportDataType.CtLocations, SchemaName.Cts, TestContext.Current.CancellationToken);

        sql.Should().Be(
            "DELETE FROM \"cts\".\"ct_locations\" WHERE loc_id IN (SELECT loc_id FROM \"temp_ct_locations\")");
    }

    [Fact]
    public async Task FilterColumnsToTableAsync_ShouldReturnMatchingColumns()
    {
        var result = await GetFactory().FilterColumnsToTableAsync(
            ImportDataType.CtLocations,
            SchemaName.Cts,
            ["record_type", "record_count", "LOC_ID", "dummy_id"],
            TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(["record_type", "record_count", "LOC_ID"]);
    }

    [Theory]
    [InlineData(false, "\"cts\".\"ct_locations\"")]
    [InlineData(true, @"""temp_ct_locations""")]
    public void GivenValidBulkLoadDataType_WhenGetTableNameRequested_ShouldSucceed(bool isTemp, string expectedValue)
    {
        var result = GetFactory().GetTableName(ImportDataType.CtLocations, SchemaName.Cts, isTemp);

        result.Should().Be(expectedValue);
    }

    [Fact]
    public async Task CreateTempTableQueryCommandAsync_ShouldGenerateCorrectSql()
    {
        var sql = await GetFactory().SqlForQuery(ImportDataType.CtLocations, SchemaName.Cts, TestContext.Current.CancellationToken);

        sql.Should().Be(
            "SELECT record_type,record_count,loc_id FROM \"temp_ct_locations\"");
    }

    private static TestableS3BulkLoadCommandFactory GetFactory() =>
        new(new NpgsqlConnection("Host=cads-postgres;Port=5432;Database=cads_data_service;Username=postgres;Password=postgres"), LocationsHeader.Split('|'));

    private static string LocationsHeader =>
        "record_type|record_count|loc_id";
}