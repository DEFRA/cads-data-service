using Npgsql;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Extensions;

public static class ExceptionExtension
{
    extension(Exception ex)
    {
        /// <summary>
        /// Whether an exception represents a transient failure worth retrying, versus a permanent
        /// one (bad data, malformed SQL, constraint violation) that will fail identically on every
        /// attempt and should fail fast instead of wasting backoff time.
        /// </summary>
        public bool IsTransientPostgresException()
        {
            return ex switch
            {
                PostgresException pgEx => IsTransientPostgresSqlState(pgEx.SqlState),
                NpgsqlException => true,   // client/connection-level Npgsql errors not wrapping a specific Postgres error
                DbException => true,
                IOException => true,
                TimeoutException => true,
                _ => false
            };
        }

        private static bool IsTransientPostgresSqlState(string? sqlState) =>
            !string.IsNullOrEmpty(sqlState)
            && sqlState.Length >= 2
            && s_transientPostgresSqlStateClasses.Contains(sqlState[..2]);
    }

    // Postgres SQLSTATE classes worth retrying — connection drops, deadlocks/serialization
    // conflicts, resource exhaustion, and "try again shortly" conditions. Anything else
    // (bad data, constraint violations, bad SQL, permissions) will fail identically on
    // every attempt, so it defaults to non-transient rather than being retried blindly.
    private static readonly HashSet<string> s_transientPostgresSqlStateClasses = new(StringComparer.Ordinal)
    {
        "08", // Connection Exception
        "40", // Transaction Rollback — serialization_failure, deadlock_detected
        "53", // Insufficient Resources — too_many_connections, disk_full, out_of_memory
        "57", // Operator Intervention — e.g. 57P03 cannot_connect_now (server still starting up)
        "58", // System Error — I/O failures
    };
}