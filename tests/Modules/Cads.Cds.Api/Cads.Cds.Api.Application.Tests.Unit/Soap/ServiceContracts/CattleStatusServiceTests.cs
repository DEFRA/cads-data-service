using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class CattleStatusServiceTests
{
    [Fact]
    public async Task CattleStatusServiceTests_Should_Return_Fault_If_HoldingId_IsNull()
    {
        var logger = Mock.Of<ILogger<CattleStatusService>>();
        var sut = new CattleStatusService(logger);

        var act = () => sut.GetCattleStatusRequest(null);

        await act.Should().ThrowAsync<FaultException>().WithMessage("Holding id cannot be null or whitespace");
    }

    [Fact]
    public async Task CattleStatusServiceTests_Should_Return_GetAnimalCohortResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<CattleStatusService>>();
        var sut = new CattleStatusService(logger);
        var holdingId = "123";

        var result = await sut.GetCattleStatusRequest(holdingId);

        result.Should().NotBeNull();
        result.HoldingId.Should().Be(holdingId);
        result.CattleStatusCSV.Should().NotBeNullOrWhiteSpace();
    }
}