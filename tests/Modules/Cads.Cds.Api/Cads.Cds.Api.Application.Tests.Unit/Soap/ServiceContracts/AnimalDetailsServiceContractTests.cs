using Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using CoreWCF;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Soap.ServiceContracts;

public class AnimalDetailsServiceContractTests
{
    [Fact]
    public async Task AnimalDetailsServiceContract_Should_Return_Fault_If_HoldingId_IsNull()
    {
        var logger = Mock.Of<ILogger<AnimalDetailsServiceContract>>();
        var sut = new AnimalDetailsServiceContract(logger);

        var act = () => sut.GetAnimalDetails(new GetAnimalDetailsRequest());

        await act.Should().ThrowAsync<FaultException>().WithMessage("Eartags cannot be null and must contain at least one value");
    }

    [Fact]
    public async Task AnimalDetailsServiceContract_Should_Return_GetAnimalDetailsResponse_On_Success()
    {
        var logger = Mock.Of<ILogger<AnimalDetailsServiceContract>>();
        var sut = new AnimalDetailsServiceContract(logger);
        var eartagId = "123";

        var result = await sut.GetAnimalDetails(new GetAnimalDetailsRequest
        {
            Body = new GetAnimalDetailsRequestBody { AnimalsIds = new AnimalsIds { Eartag = new() { eartagId } } }
        });

        result.Should().NotBeNull();
        result.SearchResults.EartagResults.First().Eartag.Should().Be(eartagId);
    }
}