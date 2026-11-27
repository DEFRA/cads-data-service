using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;

public interface IScenarioGenerationDispatcher
{
    Dictionary<string, IGenerationScenario> Scenarios { get; }

    Task<Dictionary<string, CreateGenerationResponseDto>> DispatchBatchAsync(IEnumerable<ICreateGenerationRequest> requests, CancellationToken ct);

    Task<CreateGenerationResponseDto> DispatchAsync(ICreateGenerationRequest request, CancellationToken ct);
}