using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;

namespace Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;

public interface IScenarioGenerationDispatcher
{
    Dictionary<string, IGenerationScenario> Scenarios { get; }

    Task<Dictionary<string, CreateGenerationResponseDto>> DispatchBatchAsync(IEnumerable<CreateGenerationRequestDto> requests, CancellationToken ct);

    Task<CreateGenerationResponseDto> DispatchAsync(CreateGenerationRequestDto request, CancellationToken ct);
}
