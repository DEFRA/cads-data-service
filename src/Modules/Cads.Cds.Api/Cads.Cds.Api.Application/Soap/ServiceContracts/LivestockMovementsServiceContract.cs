using Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;
using CoreWCF;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Implementation of ILivestockMovementsServiceContract
/// </summary>
public class LivestockMovementsServiceContract : ILivestockMovementsServiceContract
{
    private readonly ILogger<LivestockMovementsServiceContract> _logger;

    public LivestockMovementsServiceContract(ILogger<LivestockMovementsServiceContract> logger)
    {
        _logger = logger;
    }

    // ReSharper disable once InconsistentNaming
    public GetLivestockMovementsResponse GetLivestockMovements(GetLivestockMovementsRequest request)
    {
        if (request.ServiceOptions == null)
        {
            _logger.LogWarning("ServiceOptions is null - CoreWCF deserialization failed.");
            throw new FaultException("ServiceOptions cannot be null");
        }
        if (request.MovementQuery == null)
        {
            _logger.LogWarning("MovementQuery is null - CoreWCF deserialization failed.");
            throw new FaultException("MovementQuery cannot be null");
        }
        var response = new GetLivestockMovementsResponse
        {
            TraceIdentifier = request.MovementQuery.TraceIdentifier,
            TraceParameter = new TraceParameter
            {
                TraceType = request.MovementQuery.TraceType,
                WindowStartDate = request.MovementQuery.WindowStartDate,
                WindowEndDate = request.MovementQuery.WindowEndDate
            },
            SpeciesList = GetMockSpeciesList()
        };

        _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

        return response;
    }

    private static SpeciesList GetMockSpeciesList()
    {
        return new SpeciesList
        {
            Species = new List<Species>
            {
                new Species
                {
                    SpeciesCode = "MOCK_SPECIES_CODE_1",
                    Movements = new LivestockMovements
                    {
                        Movement = new List<LivestockMovement>
                        {
                            new LivestockMovement
                            {
                                AnimalIdentifier = "MOCK_ANIMAL_IDENTIFIER_1",
                                AnimalIdentifierType = "MOCK_ANIMAL_IDENTIFIER_TYPE_1",
                                DeathIndicator = "MOCK_DEATH_INDICATOR_1",
                                DateOfBirth = "MOCK_DATE_OF_BIRTH_1",
                                Gender = "MOCK_GENDER_1",
                                BreedCode = "MOCK_BREED_CODE_1",
                                OffLocationIdentifier = "MOCK_OFF_LOCATION_IDENTIFIER_1",
                                OffLocationIdentifierType =
                                    "MOCK_OFF_LOCATION_IDENTIFIER_TYPE_1",
                                OnLocationIdentifier = "MOCK_ON_LOCATION_IDENTIFIER_1",
                                OnLocationIdentifierType = "MOCK_ON_LOCATION_IDENTIFIER_TYPE_1",
                                MovementDateOff = "MOCK_MOVEMENT_DATE_OFF_1",
                                MovementDateOn = "MOCK_MOVEMENT_DATE_ON_1",
                                SystemProvidingData = "MOCK_SYSTEM_PROVIDING_DATA_1",
                                OffMovementRecordType = "MOCK_OFF_MOVEMENT_RECORD_TYPE_1",
                                OnMovementRecordType = "MOCK_ON_MOVEMENT_RECORD_TYPE_1"
                            }
                        }
                    }
                }
            }
        };
    }
}