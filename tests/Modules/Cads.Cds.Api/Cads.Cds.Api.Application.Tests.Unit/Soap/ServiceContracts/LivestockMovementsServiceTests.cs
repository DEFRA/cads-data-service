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
    public void LivestockMovementsService_Should_Return_Fault_If_ServiceOptionsIsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsService>>();
        var sut = new LivestockMovementsService(logger);

        var act = () => sut.GetAnimalCohortRequest(null, null).GetAwaiter().GetResult();

        act.Should().Throw<FaultException>().WithMessage("ServiceOptions cannot be null");
    }

    [Fact]
    public void LivestockMovementsService_Should_Return_Fault_If_AnimalCohortQueryIsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsService>>();
        var sut = new LivestockMovementsService(logger);
        var serviceOptions = new ServiceOptions();

        var act = () => sut.GetAnimalCohortRequest(serviceOptions, null).GetAwaiter().GetResult();

        act.Should().Throw<FaultException>().WithMessage("AnimalCohortQuery cannot be null");
    }

    [Fact]
    public void LivestockMovementsService_Should_Return_GetAnimalCohortResponse_On_Success()
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
            }
        };

        var result = sut.GetAnimalCohortRequest(serviceOptions, animalCohortQuery).GetAwaiter().GetResult();

        result.Should().NotBeNull();
        result.TraceIdentifier.Should().Be(animalCohortQuery.TraceIdentifier);
    }
}