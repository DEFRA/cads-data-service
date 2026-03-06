using Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;
using Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;
using CoreWCF;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

public class AnimalPassportAndDetailsServiceContract : IAnimalPassportAndDetailsServiceContract
{
    private readonly ILogger<AnimalPassportAndDetailsServiceContract> _logger;

    public AnimalPassportAndDetailsServiceContract(ILogger<AnimalPassportAndDetailsServiceContract> logger)
    {
        _logger = logger;
    }

    public async Task<GetAnimalPassportAndDetailsResponse> GetAnimalPassportAndDetailsRequest(AnimalsIds? AnimalsIds)
    {
        if (AnimalsIds == null)
        {
            _logger.LogWarning("AnimalsIds is null - CoreWCF deserialization failed");
            throw new FaultException("AnimalsIds cannot be null");
        }

        if (AnimalsIds.Eartag.Count == 0)
        {
            _logger.LogWarning("Eartags are null or has no values");
            throw new FaultException("Eartags cannot be null and must contain at least one value");
        }

        var response = new GetAnimalPassportAndDetailsResponse { SearchResults = await GetMockSearchResults() };

        _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

        return response;
    }

    private async Task<SearchResults?> GetMockSearchResults()
    {
        return new SearchResults
        {
            EartagResults = new List<EartagResult>
            {
                new EartagResult
                {
                    DetailsFound = new DetailsFound
                    {
                        AnimalRecord =
                            new AnimalRecord
                            {
                                AnimalPk = 123456,
                                AnimalDetails =
                                    new AnimalDetails
                                    {
                                        AnimalSpecies = new CodeType { Code = "MOCK_SPECIES_CODE" },
                                        Breed = new CodeType { Code = "MOCK_BREED_CODE" },
                                        AnimalType = new CodeType { Code = "MOCK_ANIMAL_TYPE" },
                                        Gender = "MOCK_GENDER"
                                    },
                                LivestockType = new CodeType { Code = "MOCK_LIVESTOCK_TYPE" },
                                IndividualAnimalReference = "MOCK_INDIVIDUAL_ANIMAL_REFERENCE",
                                DateOfBirth = "01/01/2020",
                                IndvdlyRegstAnimalStatus = "MOCK_STATUS",
                                PassportVersion = 1,
                                Sire = "MOCK_SIRE",
                                Dam = "MOCK_DAM",
                                RefusedPassport = false,
                                Holdings = new Holdings
                                {
                                    AnimalOnFarm = new List<AnimalOnFarm>
                                    {
                                        new AnimalOnFarm
                                        {
                                            HoldingId = "MOCK_HOLDING_ID",
                                            CurrentlyOnLocation = true
                                        }
                                    }
                                }
                            },
                        AnimalPassport = new AnimalPassport
                        {
                            BirthDam = "MOCK_BIRTH_DAM",
                            PassportStatusCode = 12,
                            DateOfRegistration = "MOCK_DATE_OF_REGISTRATION",
                            PassportVersion = 1,
                            RefusedPassport = false,
                            DamIsFemale = true,
                            DamAliveAtBirth = true,
                            DamMinimumAgeAtBirth = true,
                            DamOnLocationAtBirth = true
                        },
                        Movements = new Movements
                        {
                            Movement = new List<Movement>
                            {
                                new Movement
                                {
                                    LivestockMovementStatus = "MOCK_LIVESTOCK_MOVEMENT_STATUS",
                                    MovementDateOn = "MOCK_MOVEMENT_DATE_ON",
                                    ReportRcvdDateTimeOn = "MOCK_REPORT_RCVD_DATE_TIME_ON",
                                    OnFeature = new OnFeature
                                    {
                                        FeaturePK = 123456,
                                        FeatureDetails =
                                            new FeatureDetails
                                            {
                                                FeatureName = "MOCK_FEATURE_NAME",
                                                FeatureType =
                                                    new CodeType { Code = "MOCK_FEATURE_TYPE" }
                                            },
                                        AltFeatureIdentities = new AltFeatureIdentities
                                        {
                                            AltFeatureIdentity = new List<AltFeatureIdentity>
                                            {
                                                new AltFeatureIdentity
                                                {
                                                    AltFeatureIdentityPK = 123456,
                                                    AltFeatureIdentityType =
                                                        new CodeType { Code = "MOCK_ALT_FEATURE_TYPE" },
                                                    AltFeatureIdentityValue = "MOCK_ALT_FEATURE_VALUE",
                                                    AltFeatureIdFromDate = "MOCK_ALT_FEATURE_ID_FROM_DATE"
                                                }
                                            }
                                        }
                                    },
                                    CTSMovementType = 123,
                                    CTSMovementTypeDesc = "CTSMovementTypeDesc",
                                    MovementDirection = "MOCK_MOVEMENT_DIRECTION",
                                    LocationType = "MOCK_LOCATION_TYPE"
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}