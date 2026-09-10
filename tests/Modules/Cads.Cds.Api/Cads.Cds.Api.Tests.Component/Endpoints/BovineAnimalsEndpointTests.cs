using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Tests.Component.TestFixtures;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.Api.Tests.Component.Endpoints;

public class BovineAnimalsEndpointTests(ApiTestFixture testFixture) : IClassFixture<ApiTestFixture>
{
    [Fact]
    public async Task GivenKnownIdentifier_WhenGetAnimalDetailsRequested_ShouldReturnMatchingAnimal()
    {
        var response = await testFixture.HttpClient.GetAsync(
            TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifier,
            TestContext.Current.CancellationToken);

        var animal = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailDto>(response);

        animal.ResourceType.Should().Be("AnimalDetail");
        animal.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
        animal.AnimalDetail.Should().NotBeNull();
        animal.AnimalDetail!.Identifier!.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
        animal.AnimalDetail.Parentage.Should().HaveCount(2);
    }

    [Fact]
    public async Task GivenUnknownIdentifier_WhenGetAnimalDetailsRequested_ShouldReturnNotFound()
    {
        var response = await testFixture.HttpClient.GetAsync(
            TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.UnknownIdentifier,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}