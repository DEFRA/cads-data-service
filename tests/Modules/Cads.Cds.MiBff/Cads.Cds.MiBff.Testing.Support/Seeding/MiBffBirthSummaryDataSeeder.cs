using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Cads.Cds.MiBff.Testing.Support.Seeding;

public static class MiBffBirthSummaryDataSeeder
{
    // Fixed IDs so assertions are predictable
    private const long BreedDairyId = 9001;
    private const long BreedNonDairyId = 9002;
    private const long ApplicationId = 9010;
    private const long LocationId = 9020;
    private const long CountyId = 9030;
    private const long CountryEnglandId = 9040;
    private const long CountryWalesId = 9041;

    // Animals inside the query range
    private const long AnimalInRangeId1 = 8001;
    private const long AnimalInRangeId2 = 8002;

    // Animal outside the query range — must NOT appear in results
    private const long AnimalOutOfRangeId = 8003;

    private static readonly DateOnly s_birthDateInRange = new(2023, 3, 15);
    private static readonly DateOnly s_birthDateInRange2 = new(2023, 6, 20);
    public static readonly DateOnly BirthDateOutOfRange = new(2021, 1, 1);

    public static readonly DateOnly QueryFrom = new(2023, 1, 1);
    public static readonly DateOnly QueryTo = new(2024, 1, 1);

    public static void Seed(PostgreSqlContainer container)
    {
        var connStr = LocalConnectionString(container);

        using var conn = new NpgsqlConnection(connStr);
        conn.Open();

        // 1. Lookup tables (insert-or-skip to tolerate re-runs)
        Execute(conn, $"""
            INSERT INTO _ct_breeds (brd_id, brd_type, brd_code, brd_short_description, brd_long_description)
            VALUES ({BreedDairyId}, 'D', 'HF', 'Holstein', 'HOLSTEIN FRIESIAN'),
                   ({BreedNonDairyId}, 'B', 'HE', 'Hereford', 'HEREFORD')
            ON CONFLICT (brd_id) DO NOTHING;
        """);

        Execute(conn, $"""
            INSERT INTO _ct_valid_applications (vap_id, vap_application_type)
            VALUES ({ApplicationId}, 'B')
            ON CONFLICT (vap_id) DO NOTHING;
        """);

        Execute(conn, $"""
            INSERT INTO _ct_countries (cry_id, cry_name)
            VALUES ({CountryEnglandId}, 'England'),
                   ({CountryWalesId},   'Wales')
            ON CONFLICT (cry_id) DO NOTHING;
        """);

        Execute(conn, $"""
            INSERT INTO _ct_counties (cty_id, cty_name)
            VALUES ({CountyId}, 'Devon')
            ON CONFLICT (cty_id) DO NOTHING;
        """);

        Execute(conn, $"""
            INSERT INTO _ct_locations (loc_id, loc_cty_id)
            VALUES ({LocationId}, {CountyId})
            ON CONFLICT (loc_id) DO NOTHING;
        """);

        // 2. Registered animals
        Execute(conn, $"""
            INSERT INTO _ct_registered_animals
                (ran_id, ran_birth_date, ran_sex, ran_brd_id, ran_vap_id, ran_loc_id_passport, ran_cry_id_chr_origin)
            VALUES
                ({AnimalInRangeId1},   '{s_birthDateInRange:yyyy-MM-dd}',  'M', {BreedDairyId},    {ApplicationId}, {LocationId}, {CountryEnglandId}),
                ({AnimalInRangeId2},   '{s_birthDateInRange2:yyyy-MM-dd}', 'F', {BreedNonDairyId}, {ApplicationId}, {LocationId}, {CountryWalesId}),
                ({AnimalOutOfRangeId}, '{BirthDateOutOfRange:yyyy-MM-dd}', 'M', {BreedDairyId},  {ApplicationId}, {LocationId}, {CountryEnglandId})
            ON CONFLICT (ran_id) DO NOTHING;
        """);
    }

    public static string LocalConnectionString(PostgreSqlContainer container) =>
        $"Host=127.0.0.1;" +
        $"Port={container.GetMappedPublicPort(5432)};" +
        $"Database={TestDatabaseConstants.TestCadsDatabaseName};" +
        $"Username={TestDatabaseConstants.PostgresUserName};" +
        $"Password={TestDatabaseConstants.PostgresPassword}";

    private static void Execute(NpgsqlConnection conn, string sql)
    {
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }
}