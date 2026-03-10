using Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class AnimalPassportAndDetailsServiceContractTests
{
    [Fact]
    public async Task AnimalPassportAndDetailsServiceContract_Should_Return_Fault_If_Eartags_IsNull_Or_Empty()
    {
        var logger = Mock.Of<ILogger<AnimalPassportAndDetailsServiceContract>>();
        var sut = new AnimalPassportAndDetailsServiceContract(logger);

        var act = () => sut.GetAnimalPassportAndDetails(new GetAnimalPassportAndDetailsRequest());

        await act.Should().ThrowAsync<FaultException>().WithMessage("Eartags cannot be null and must contain at least one value");
    }

    [Fact]
    public async Task AnimalPassportAndDetailsServiceContract_Should_Return_GetAnimalPassportAndDetailsResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<AnimalPassportAndDetailsServiceContract>>();
        var sut = new AnimalPassportAndDetailsServiceContract(logger);
        var eartagId = "123";

        var result = await sut.GetAnimalPassportAndDetails(new GetAnimalPassportAndDetailsRequest
        {
            AnimalsIds = new AnimalsIds { Eartag = new() { eartagId } }
        });

        result.Should().NotBeNull();
        result.SearchResults!.EartagResults.Count.Should().Be(1);
    }
}