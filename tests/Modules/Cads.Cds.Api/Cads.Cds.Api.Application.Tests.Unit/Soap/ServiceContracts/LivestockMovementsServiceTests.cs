using Cads.Cds.Api.Application.Soap.Messages;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class LivestockMovementsServiceTests
{
    [Fact]
    public async Task LivestockMovementsService_Should_Return_Fault_If_ServiceOptions_IsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsService>>();
        var sut = new LivestockMovementsService(logger);

        var act = () => sut.GetAnimalCohortRequest(null, null);

        await act.Should().ThrowAsync<FaultException>().WithMessage("ServiceOptions cannot be null");
    }

    [Fact]
    public async Task LivestockMovementsService_Should_Return_Fault_If_AnimalCohortQuery_IsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsService>>();
        var sut = new LivestockMovementsService(logger);
        var serviceOptions = new ServiceOptions();

        var act = () => sut.GetAnimalCohortRequest(serviceOptions, null);

        await act.Should().ThrowAsync<FaultException>().WithMessage("AnimalCohortQuery cannot be null");
    }

    [Fact]
    public async Task LivestockMovementsService_Should_Return_GetAnimalCohortResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsService>>();
        var sut = new LivestockMovementsService(logger);
        var serviceOptions = new ServiceOptions();
        var animalCohortQuery = new AnimalCohortQuery
        {
            TraceIdentifier = new TraceIdentifier
            {
                TraceIdentifierValue = "TestTraceIdentifier",
                TraceSpecificationIdentifier = "TestTraceSpecificationIdentifier"
            },
            BirthLocationIdentifier = "TestBirthLocationIdentifier",
            DateOfBirth = "01-01-1901",
            Gender = new ReferenceDataType
            {
                Code = "M",
                RefDataSetName = "Gender"
            },
            BirthLocationIdentifierType = new ReferenceDataType
            {
                Code = "TestBirthLocationIdentifierType",
                RefDataSetName = "TestBirthLocationIdentifierType"
            },
            Locations = new Locations
            {
                Location = new List<Location>
                {
                    new Location
                    {
                        WindowStartDate = "01-01-2022",
                        WindowEndDate = "01-01-2023",
                        TargetLocationIdentifier = "TestLocationIdentifier",
                        TargetLocationIdentifierType = new ReferenceDataType
                        {
                            Code = "TestLocationIdentifierType",
                            RefDataSetName = "TestLocationIdentifierType"
                        }
                    }
                }
            },
            SpeciesCodesAndAnimals = new SpeciesCodesAndAnimals
            {
                SpeciesCodeAndAnimalsList = new List<SpeciesCodeAndAnimals>
                {
                    new SpeciesCodeAndAnimals
                    {
                        AnimalIdentifiers = new AnimalIdentifiers
                        {
                            AnimalIdentifier = new List<AnimalIdentifier>
                            {
                                new AnimalIdentifier
                                {
                                    AnimalIdentifierValue = "TestAnimalIdentifierValue",
                                    AnimalIdentifierType = new ReferenceDataType
                                    {
                                        Code = "TestAnimalIdentifierType",
                                        RefDataSetName = "TestAnimalIdentifierType"
                                    }   
                                }
                            }
                        }
                    }
                }
            }
        };

        var result = await sut.GetAnimalCohortRequest(serviceOptions, animalCohortQuery);

        result.Should().NotBeNull();
        result.TraceIdentifier.Should().Be(animalCohortQuery.TraceIdentifier);
        result.CohortAnimals.Should().NotBeNull();
    }
}