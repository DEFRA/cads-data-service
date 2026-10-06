using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;

namespace Cads.Cds.Api.Tests.Integration.Endpoints;

[Collection("ApiIntegration"), Trait("Dependence", "testcontainers")]
public class BovineEndpointTests(ApiContainerFixture apiContainerFixture)
{
    // GetAnimalDetailsByIdentifier

    [Fact]
    public async Task GivenValidIdentifier_WhenGetAnimalDetailsByIdentifierRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifier);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        result.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
        result.AnimalDetail.Should().NotBeNull();
        result.AnimalDetail!.Identifier!.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
    }

    // GetAnimalsOnHolding

    [Fact]
    public async Task GivenEmptyCph_WhenGetAnimalsOnHoldingRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(""));

        await HttpResponseMessageUtilities.VerifyBadRequest<ValidationProblemDetailsDto>(response);
    }

    [Fact]
    public async Task GivenValidCph_WhenGetAnimalsOnHoldingRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl("12/345/6789"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.ResourceType.Should().Be("AnimalCollection");
        result.Cph!.Identifier.Should().Be("12/345/6789");
    }

    [Fact]
    public async Task GivenPopulatedQueryString_WhenGetAnimalsOnHoldingRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl("12/345/6789",
            "sex=Female&breedCode=HO&page=2&pageSize=2&orderBy=BirthDate&direction=Desc"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.ResourceType.Should().Be("AnimalCollection");
        result.Cph!.Identifier.Should().Be("12/345/6789");
        result.Page.Should().Be(2);
        result.PageSize.Should().Be(2);
    }

    private static string GetAnimalsOnHoldingUrl(string cph, string? queryString = null)
    {
        var url = $"{TestEndpointConstants.ApiBovineAnimals}?CPH={Uri.EscapeDataString(cph)}";

        return queryString is null ? url : $"{url}&{queryString}";
    }

    private async Task<HttpResponseMessage> ExecuteTest(string? endpoint)
    {
        var client = apiContainerFixture.CreateBasicClient();

        return await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
    }
}