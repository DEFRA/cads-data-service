using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Tests.Component.TestFixtures;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.Api.Tests.Component.Endpoints;

public class BovineEndpointTests(ApiTestFixture testFixture) : IClassFixture<ApiTestFixture>
{
    // GetAnimalDetailsByIdentifier
    [Fact]
    public async Task GivenKnownIdentifier_WhenGetAnimalDetailsRequested_ShouldReturnFullContractShape()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifier);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        result.ResourceType.Should().Be("AnimalDetail");
        result.EventDateTime.Should().Be(new DateTime(2026, 08, 24, 12, 0, 0, DateTimeKind.Utc));
        result.Source!.System.Should().Be("CTS");

        var animal = result.AnimalDetail!;
        animal.Sex.Should().Be("Female");
        animal.State.Should().Be("Alive");
        animal.RestrictionStatus.Should().Be("Restricted");
        animal.BreedCode!.Identifier.Should().Be("HO");
        animal.BreedCode.BreedName.Should().Be("Holstein Friesian");
        animal.Parentage.Should().BeEquivalentTo(
        [
            new { Relationship = "GeneticDam", AnimalIdentifier = new { Identifier = TestBovineConstants.KnownDamIdentifier } },
            new { Relationship = "Sire", AnimalIdentifier = new { Identifier = TestBovineConstants.KnownSireIdentifier } }
        ], o => o.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GivenAnimalWithNoParents_WhenGetAnimalDetailsRequested_ShouldReturnEmptyParentage()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifierNoParents);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        result.AnimalDetail!.Parentage.Should().BeEmpty();
        result.AnimalDetail.State.Should().Be("Dead");
    }

    [Fact]
    public async Task GivenUnknownIdentifier_WhenGetAnimalDetailsRequested_ShouldReturnNotFound()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.UnknownIdentifier);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.ResourceType.Should().Be("AnimalCollection");
        result.Cph!.Identifier.Should().Be(TestBovineConstants.KnownCph);
        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphAnimalCount);
        result.Animals.Should().HaveCount(25);
    }

    [Fact]
    public async Task GivenPopulatedQueryString_WhenGetAnimalsOnHoldingRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&breedCode=HO&page=2&pageSize=2&orderBy=BirthDate&direction=Desc"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphFemaleHoCount);
        result.Animals.Should().HaveCount(2);
        result.Animals.Should().OnlyContain(a => a.Sex == "Female" && a.BreedCode!.Identifier == "HO");
        result.Animals.Should().BeInDescendingOrder(a => a.BirthDate);
    }

    [Fact]
    public async Task GivenOtherCph_WhenGetAnimalsOnHoldingRequested_ShouldOnlyReturnThatHolding()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.OtherCph));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(TestBovineConstants.OtherCphAnimalCount);
        result.LocationName.Should().Be("Elm Farm");
    }

    private async Task<HttpResponseMessage> ExecuteTest(string url)
    {
        return await testFixture.HttpClient.GetAsync(url, TestContext.Current.CancellationToken);
    }

    private static string GetAnimalsOnHoldingUrl(string cph, string? queryString = null)
    {
        var url = $"{TestEndpointConstants.ApiBovineAnimals}?CPH={Uri.EscapeDataString(cph)}";

        return queryString is null ? url : $"{url}&{queryString}";
    }
}