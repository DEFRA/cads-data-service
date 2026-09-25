using Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Dispatchers;

public sealed class ScenarioGenerationDispatcher(IEnumerable<IGenerationScenario> scenarios) : IScenarioGenerationDispatcher
{
    private readonly Dictionary<string, IGenerationScenario> _scenarios
        = scenarios.ToDictionary(s => s.Name, StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, IGenerationScenario> Scenarios => _scenarios;

    public async Task<Dictionary<string, CreateGenerationResponseDto>> DispatchBatchAsync(IEnumerable<CreateGenerationRequestDto> requests, CancellationToken ct)
    {
        var results = new Dictionary<string, CreateGenerationResponseDto>(StringComparer.OrdinalIgnoreCase);

        foreach (var request in requests)
        {
            ct.ThrowIfCancellationRequested();

            if (results.ContainsKey(request.Scenario))
                throw new GenerationValidationException($"Duplicate scenario '{request.Scenario}' in batch request.");

            var result = await DispatchAsync(request, ct);

            results[request.Scenario] = result;
        }

        return results;
    }

    public async Task<CreateGenerationResponseDto> DispatchAsync(CreateGenerationRequestDto request, CancellationToken ct)
    {
        if (!_scenarios.TryGetValue(request.Scenario, out var scenario))
            throw new GenerationValidationException($"Unknown scenario '{request.Scenario}'.");

        return await scenario.ExecuteAsync(request, ct);
    }
}