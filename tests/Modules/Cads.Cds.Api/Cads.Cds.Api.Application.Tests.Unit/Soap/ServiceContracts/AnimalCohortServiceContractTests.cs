using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class AnimalCohortServiceContractTests
{
    [Fact]
    public async Task AnimalCohortServiceContract_Should_Return_GetAnimalCohortResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<AnimalCohortServiceContract>>();
        var sut = new AnimalCohortServiceContract(logger);
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
            Gender = new CommonRefDataSetCode
            {
                Code = "M",
                RefDataSetName = "Gender"
            },
            BirthLocationIdentifierSetCode = new CommonRefDataSetCode
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
                        TargetLocationIdentifierSetCode = new CommonRefDataSetCode
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
                                    AnimalIdentifierSetCode = new CommonRefDataSetCode
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

        var result = await sut.GetAnimalCohort(new GetAnimalCohortRequest { ServiceOptions = serviceOptions, AnimalCohortQuery = animalCohortQuery });

        result.Should().NotBeNull();
        result.TraceIdentifier.Should().Be(animalCohortQuery.TraceIdentifier);
        result.CohortAnimals.Should().NotBeNull();
    }
}