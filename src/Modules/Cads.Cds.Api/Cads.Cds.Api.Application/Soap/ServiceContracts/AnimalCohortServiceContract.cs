using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using CoreWCF;
using Microsoft.Extensions.Logging;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Implementation of IAnimalCohortServiceContract
/// </summary>
public class AnimalCohortServiceContract : IAnimalCohortServiceContract
{
    private readonly ILogger<AnimalCohortServiceContract> _logger;

    public AnimalCohortServiceContract(ILogger<AnimalCohortServiceContract> logger)
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

        var response = new GetAnimalCohortResponse
        {
            CohortAnimals = await GetMockCohortData(),
            TraceIdentifier = AnimalCohortQuery.TraceIdentifier
        };

        _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

        return response;
    }

    private async Task<CohortAnimals> GetMockCohortData()
    {
        var cohortAnimals = new CohortAnimals();
        cohortAnimals.CohortAnimal.Add(new CohortAnimal
        {
            CohortType = "MOCK_COHORT_TYPE_1",
            DateOfBirth = "MOCK_DATE_OF_BIRTH_1",
            BreedCode = new ReferenceDataType { Code = "MOCK_BREED_CODE" },
            Gender = new ReferenceDataType { Code = "MOCK_GENDER" },
            SpeciesCodeAndAnimalIdentifiers = new SpeciesCodeAndAnimalIdentifiers
            {
                AnimalIdentifiers = new AnimalIdentifiers
                {
                    AnimalIdentifier = new List<AnimalIdentifier>
                    {
                        new() { AnimalIdentifierValue = "MOCK_ANIMAL_IDENTIFIER", AnimalIdentifierType = new ReferenceDataType { Code = "MOCK_ANIMAL_IDENTIFIER_TYPE"} }
                    }
                },
                AnimalSpecies = new ReferenceDataType { Code = "MOCK_SPECIES_CODE" }
            },
            TargetLocation = new TargetLocation
            {
                MovementOnDate = "MOCK_MOVEMENT_ON_DATE",
                Location = new PrimaryLocationIdentifiersAndTypes
                {
                    LocationIdentifierAndType = new LocationIdentifierAndType { LocationIdentifier = "MOCK_LOCATION_IDENTIFIER", LocationIdentifierType = new ReferenceDataType { Code = "MOCK_LOCATION_TYPE" } },
                    LocationType = new ReferenceDataType { Code = "MOCK_LOCATION_TYPE" }
                }
            },
            LastKnownLocation = new TargetLocation
            {
                MovementOnDate = "MOCK_LAST_KNOWN_MOVEMENT_ON_DATE",
                Location = new PrimaryLocationIdentifiersAndTypes
                {
                    LocationIdentifierAndType = new LocationIdentifierAndType { LocationIdentifier = "MOCK_LAST_KNOWN_LOCATION_IDENTIFIER", LocationIdentifierType = new ReferenceDataType { Code = "MOCK_LAST_KNOWN_LOCATION_TYPE" } },
                    LocationType = new ReferenceDataType { Code = "MOCK_LAST_KNOWN_LOCATION_TYPE" }
                }
            }
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