using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;

public delegate T RecordMapper<T>(Dictionary<string, object?> row);

public delegate T TextParser<T>(string value, char delimiter = '|');

public static class BulkLoadTestHelpers
{
    public static async Task AssertCsvRowsMatchDatabaseAsync(
        string connectionString,
        string tableName,
        IEnumerable<string> csvRows,
        string orderBy)
    {
        var expected = csvRows
            .Select(row => LocationRecordUtilities.ParseLocationCsvRow(row))
            .ToList();

        var actualRows = await PostgresWaitUtilities.WaitForRowsAsync(
            connectionString,
            tableName,
            orderBy,
            expectedCount: expected.Count,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        var actual = actualRows
            .Select(LocationRecordUtilities.MapLocationFromDb)
            .ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    public static async Task AssertRowsMatchDatabaseAsync<T>(
        string connectionString,
        string tableName,
        Dictionary<string, T?> dataRows,
        RecordMapper<T> recordMapper,
        string orderBy)
    {
        var expected = dataRows
           .Select( i=>i.Value)
           .ToList();

        var actualRows = await PostgresWaitUtilities.WaitForRowsAsync(
            connectionString,
            tableName,
            orderBy,
            expectedCount: expected.Count,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        var actual = actualRows
            .Select(row => recordMapper(row))
            .ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}