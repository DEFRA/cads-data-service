using Cads.Cds.Ingester.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Ingester.Infrastructure.Tests.Unit.Repositories;

internal static class TestDataSeeder
{
    public const long FirstDataSeedIngestionHistoryId = 1;
    public const long SecondDataSeedIngestionHistoryId = 2;

    public const string FirstDataSeedIngestionHistoryFileName = "0003_001__ct_param_header_seed_data.postgresql.sql";
    public const string SecondDataSeedIngestionHistoryFileName = "0003_002__ct_param_value_seed_data.postgresql.sql";

    public const string FirstDataSeedIngestionHistoryChecksum = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    public const string SecondDataSeedIngestionHistoryChecksum = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";

    public static void Seed(DbContext context)
    {
        context.AddRange(GetDataSeedIngestionHistories());
        context.SaveChanges();
    }

    private static DataSeedIngestionHistory[] GetDataSeedIngestionHistories() =>
    [
        new DataSeedIngestionHistory
        {
            Id = FirstDataSeedIngestionHistoryId,
            FileName = FirstDataSeedIngestionHistoryFileName,
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = FirstDataSeedIngestionHistoryChecksum
        },
        new DataSeedIngestionHistory
        {
            Id = SecondDataSeedIngestionHistoryId,
            FileName = SecondDataSeedIngestionHistoryFileName,
            AppliedAt = new DateTimeOffset(2026, 1, 2, 0, 0, 0, TimeSpan.Zero),
            Checksum = SecondDataSeedIngestionHistoryChecksum
        }
    ];
}