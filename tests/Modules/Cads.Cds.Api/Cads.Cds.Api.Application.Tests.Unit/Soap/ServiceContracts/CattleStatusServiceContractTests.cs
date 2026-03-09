using Cads.Cds.Api.Application.Soap.Messages.CattleStatus;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class CattleStatusServiceContractTests
{
    [Fact]
    public async Task CattleStatusServiceTests_Should_Return_Fault_If_HoldingId_IsNull()
    {
        var logger = Mock.Of<ILogger<CattleStatusServiceContract>>();
        var sut = new CattleStatusServiceContract(logger);

        var act = () => sut.GetCattleStatus(new GetCattleStatusRequest());

        act.Should().Throw<FaultException>().WithMessage("Holding id cannot be null or whitespace");
    }

    [Fact]
    public async Task CattleStatusServiceTests_Should_Return_GetAnimalCohortResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<CattleStatusServiceContract>>();
        var sut = new CattleStatusServiceContract(logger);
        var holdingId = "123";

        var result = sut.GetCattleStatus(new GetCattleStatusRequest { HoldingId = holdingId });

        result.Should().NotBeNull();
        result.HoldingId.Should().Be(holdingId);
        result.CattleStatusCSV.Should().NotBeNullOrWhiteSpace();
    }
}