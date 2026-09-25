using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Scenarios;

public interface IGenerationScenario
{
    string Name { get; }

    string TableName { get; }

    Task<CreateGenerationResponseDto> ExecuteAsync(CreateGenerationRequestDto request, CancellationToken ct);
}