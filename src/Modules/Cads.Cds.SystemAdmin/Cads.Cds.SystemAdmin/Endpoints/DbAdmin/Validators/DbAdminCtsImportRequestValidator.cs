using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using FluentValidation;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Validators;

public class DbAdminCtsImportRequestValidator : DbAdminRequestValidatorBase<DbAdminCtsImportRequest>
{
    private static readonly string[] AllowedCommands =
    {
        "deferred_errors",
        "plan",
        "summary"
    };

    public DbAdminCtsImportRequestValidator() : base(AllowedCommands)
    {
        RuleFor(x => x.Args)
            .Must(args => args.HasValue
                          && args.Value.TryGetProperty("run_id", out var runId)
                          && runId.ValueKind == JsonValueKind.Number
                          && runId.TryGetInt64(out _))
            .WithMessage("Args must contain a numeric 'run_id' property.");
    }
}