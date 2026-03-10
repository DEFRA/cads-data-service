using Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class LivestockMovementServiceContractTests
{
    [Fact]
    public async Task LivestockMovementsServiceContract_Should_Return_Fault_If_ServiceOptions_IsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsServiceContract>>();
        var sut = new LivestockMovementsServiceContract(logger);

        var act = () => sut.GetLivestockMovements(new GetLivestockMovementsRequest());

        await act.Should().ThrowAsync<FaultException>().WithMessage("ServiceOptions cannot be null");
    }

    [Fact]
    public async Task LivestockMovementsServiceContract_Should_Return_Fault_If_MovementQuery_IsNull()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsServiceContract>>();
        var sut = new LivestockMovementsServiceContract(logger);

        var act = () => sut.GetLivestockMovements(new GetLivestockMovementsRequest { ServiceOptions = new ServiceOptions() });
        await act.Should().ThrowAsync<FaultException>().WithMessage("MovementQuery cannot be null");
    }

    [Fact]
    public async Task LivestockMovementsServiceContract_Should_Return_GetAnimalDetailsResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<LivestockMovementsServiceContract>>();
        var sut = new LivestockMovementsServiceContract(logger);

        var result = await sut.GetLivestockMovements(new GetLivestockMovementsRequest
        {
            ServiceOptions = new ServiceOptions(),
            MovementQuery = new MovementQuery()
        });

        result.Should().NotBeNull();
    }
}