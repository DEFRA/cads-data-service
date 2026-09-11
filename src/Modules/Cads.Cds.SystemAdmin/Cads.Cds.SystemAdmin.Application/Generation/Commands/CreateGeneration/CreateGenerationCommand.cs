using Cads.Cds.SystemAdmin.Application.Imports.Commands;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Commands.CreateGeneration;

public sealed record CreateGenerationCommand(
    string Table,
    string Scenario,
    int RowCount,
    long? BusinessKey
) : ISystemAdminCommand<CreateGenerationResponseDto>;