using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Scenarios;

public interface IGenerationScenario
{
    string Name { get; }

    string TableName { get; }

    ImportActionType ImportActionType { get; }

    Task<CreateGenerationResponseDto> ExecuteAsync(ICreateGenerationRequest request, CancellationToken ct = default);
}