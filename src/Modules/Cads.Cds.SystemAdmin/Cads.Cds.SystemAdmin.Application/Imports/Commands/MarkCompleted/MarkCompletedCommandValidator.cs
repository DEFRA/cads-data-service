using Cads.Cds.BuildingBlocks.Application.Commands.Validators;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkCompleted;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkImportCompleted;

public sealed class MarkCompletedCommandValidator
    : IdOnlyCommandValidator<MarkCompletedCommand>
{
}