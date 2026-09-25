using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using FluentValidation;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Validators;

public sealed class DbAdminExecuteCommandRequestValidator : AbstractValidator<DbAdminExecuteCommandRequest>
{
    public readonly string[] AllowedCommands = {
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
        "shared_block_reads"
    };

    public DbAdminExecuteCommandRequestValidator()
    {
        RuleFor(x => x.Command)
            .NotEmpty()
            .WithMessage("Command is required.");

        RuleFor(x => x.Command)
            .Must(command => AllowedCommands.Contains(command))
            .WithMessage("Command is not recognised.");
    }
}