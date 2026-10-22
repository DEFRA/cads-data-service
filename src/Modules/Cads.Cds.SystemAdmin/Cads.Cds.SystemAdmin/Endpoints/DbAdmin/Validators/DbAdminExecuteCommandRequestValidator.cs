using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using FluentValidation;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Validators;

public sealed class DbAdminExecuteCommandRequestValidator : DbAdminRequestValidatorBase<DbAdminExecuteCommandRequest>
{
    private static readonly string[] s_allowedCommands = [
        "sessions_by_state",
        "active_queries",
        "cancel_query",
        "terminate_query",
        "transactions",
        "rows_activity",
        "buffer_cache_hit_ratio",
        "locks_and_blocking",
        "idle_in_transaction",
        "prepared_transactions",
        "table_activity",
        "index_usage",
        "largest_tables",
        "slow_queries",
        "shared_block_reads",
        "database_size",
        "reset_pg_stat_statements"];

    private static readonly string[] PidCommands = {
        "cancel_query",
        "terminate_query"
    };

    public DbAdminExecuteCommandRequestValidator() : base(s_allowedCommands)
    {
        RuleFor(x => x.Args)
            .Must(HaveValidPid)
            .WithMessage("Args must contain an integer 'pid' property between 1 and 2147483647.")
            .When(x => PidCommands.Contains(x.Command));
    }

    private static bool HaveValidPid(JsonElement? args) =>
        args is { ValueKind: JsonValueKind.Object } value
        && value.TryGetProperty("pid", out var pid)
        && pid.ValueKind == JsonValueKind.Number
        && pid.TryGetInt32(out var pidValue)
        && pidValue > 0;
}