using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;
using Microsoft.Extensions.Logging;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Implementation of IAnimalCohortServiceContract
/// </summary>
[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class AnimalCohortServiceContract : IAnimalCohortServiceContract
{
    private readonly ILogger<AnimalCohortServiceContract> _logger;

    public AnimalCohortServiceContract(ILogger<AnimalCohortServiceContract> logger)
    {
        _logger = logger;
    }

    // ReSharper disable once InconsistentNaming
    public GetAnimalCohortResponse GetAnimalCohort(GetAnimalCohortRequest request)
    {
        if (request?.ServiceOptions == null)
        {
            _logger.LogWarning("ServiceOptions is null - CoreWCF deserialization failed.");
            throw new FaultException("ServiceOptions cannot be null");
        }
        if (request?.AnimalCohortQuery == null)
        {
            _logger.LogWarning("AnimalCohortQuery is null - CoreWCF deserialization failed.");
            throw new FaultException("AnimalCohortQuery cannot be null");
        }

        var response = new GetAnimalCohortResponse
        {
            CohortAnimals = GetMockCohortData(),
            TraceIdentifier = request.AnimalCohortQuery.TraceIdentifier
        };

        _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

        return response;
    }

    private CohortAnimals GetMockCohortData()
    {
        var cohortAnimals = new CohortAnimals();
        cohortAnimals.CohortAnimal.Add(new CohortAnimal
        {
            CohortType = "MOCK_COHORT_TYPE_1",
            DateOfBirth = "MOCK_DATE_OF_BIRTH_1",
            BreedCode = new RefDataSetCode { Code = "MOCK_BREED_CODE" },
            Gender = new RefDataSetCode { Code = "MOCK_GENDER" },
            SpeciesCodeAndAnimalIdentifiers = new SpeciesCodeAndAnimalIdentifiers
            {
                AnimalIdentifiers = new AnimalIdentifiers
                {
                    AnimalIdentifier = new List<AnimalIdentifier>
                    {
                        new() { AnimalIdentifierValue = "MOCK_ANIMAL_IDENTIFIER", AnimalIdentifierSetCode = new RefDataSetCode { Code = "MOCK_ANIMAL_IDENTIFIER_TYPE"} }
                    }
                },
                AnimalSpecies = new RefDataSetCode { Code = "MOCK_SPECIES_CODE" }
            },
            TargetLocation = new CohortLocation
            {
                MovementOnDate = "MOCK_MOVEMENT_ON_DATE",
                Location = new LocationDetail
                {
                    PrimaryLocationIdentifiersAndTypes = new PrimaryLocationIdentifiersAndTypes { LocationIdentifierAndType = new List<LocationIdentifierAndType> { new LocationIdentifierAndType { LocationIdentifier = "MOCK_LOCATION_IDENTIFIER", LocationIdentifierSetCode = new RefDataSetCode { Code = "MOCK_LOCATION_TYPE" } } } },
                    LocationSetCode = new RefDataSetCode { Code = "MOCK_LOCATION_TYPE" }
                }
            },
            LastKnownLocation = new CohortLocation
            {
                MovementOnDate = "MOCK_LAST_KNOWN_MOVEMENT_ON_DATE",
                Location = new LocationDetail
                {
                    PrimaryLocationIdentifiersAndTypes = new PrimaryLocationIdentifiersAndTypes { LocationIdentifierAndType = new List<LocationIdentifierAndType> { new LocationIdentifierAndType { LocationIdentifier = "MOCK_LAST_KNOWN_LOCATION_IDENTIFIER", LocationIdentifierSetCode = new RefDataSetCode { Code = "MOCK_LAST_KNOWN_LOCATION_TYPE" } } } },
                    LocationSetCode = new RefDataSetCode { Code = "MOCK_LAST_KNOWN_LOCATION_TYPE" }
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