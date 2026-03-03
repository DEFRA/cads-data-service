using Cads.Cds.Api.Application.Soap.Messages;
using CoreWCF;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Implementation of ILivestockMovementsService
/// </summary>
public class LivestockMovementsService : ILivestockMovementsService
{
    private readonly ILogger<LivestockMovementsService> _logger;

    public LivestockMovementsService(ILogger<LivestockMovementsService> logger)
    {
        _logger = logger;
    }

    // ReSharper disable once InconsistentNaming
    public async Task<GetAnimalCohortResponse> GetAnimalCohortRequest(ServiceOptions? ServiceOptions, AnimalCohortQuery? AnimalCohortQuery)
    {
        if (ServiceOptions == null)
        {
            _logger.LogWarning("ServiceOptions is null - CoreWCF deserialization failed.");
            throw new FaultException("ServiceOptions cannot be null");
        }
        if (AnimalCohortQuery == null)
        {
            _logger.LogWarning("AnimalCohortQuery is null - CoreWCF deserialization failed.");
            throw new FaultException("AnimalCohortQuery cannot be null");
        }

        try
        {
            var response = new GetAnimalCohortResponse
            {
                CohortAnimals = await GetMockCohortData(),
                TraceIdentifier = AnimalCohortQuery.TraceIdentifier
            };

            _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing GetAnimalCohortRequest");
            throw new FaultException($"Error processing request: {ex.Message}");
        }
    }

    private async Task<CohortAnimals> GetMockCohortData()
    {
        // TODO: Implement actual data retrieval logic
        await Task.CompletedTask;

        var cohortAnimals = new CohortAnimals();
        cohortAnimals.CohortAnimal.Add(new CohortAnimal
        {
            CohortType = "MOCK_COHORT_TYPE_1",
            DateOfBirth = "MOCK_DATE_OF_BIRTH_1"
        });
        cohortAnimals.CohortAnimal.Add(new CohortAnimal
        {
            CohortType = "MOCK_COHORT_TYPE_2",
            DateOfBirth = "MOCK_DATE_OF_BIRTH_2"
        });
        // Mock cohort data - replace with actual implementation
        return cohortAnimals;
    }
}