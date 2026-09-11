using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Tests.Component.TestFixtures;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.Api.Tests.Component.Endpoints;

public class BovineAnimalsOnCphEndpointTests(ApiTestFixture testFixture) : IClassFixture<ApiTestFixture>
{
    [Fact]
    public async Task GivenKnownCph_WhenAnimalsRequested_ShouldReturnTheAgreedCollectionStructure()
    {
        var collection = await GetCollection(Url());

        collection.ResourceType.Should().Be("AnimalCollection");
        collection.TotalRecords.Should().Be(TestBovineConstants.KnownCphAnimalCount);
        collection.Animals.Should().HaveCount(TestBovineConstants.KnownCphAnimalCount);

        var first = collection.Animals.First();
        first.Identifier!.Schema.Should().Be("uk.gov.defra.ear-tag.conventional");
        first.Identifier.Identifier.Should().Be("UK324537113234");
        first.BirthDate.Should().Be(new DateOnly(2023, 3, 10));
        first.DateOnCph.Should().Be(new DateOnly(2023, 3, 14));
        first.DateOffCph.Should().BeNull();
        first.Species.Should().Be("Cattle");
        first.Sex.Should().Be("Female");
        first.BreedCode!.Schema.Should().Be("cts.breed");
        first.BreedCode.BreedName.Should().Be("Holstein Friesian");
        first.BreedCode.Identifier.Should().Be("HO");
        first.Status.Should().Be("Alive");
    }

    [Fact]
    public async Task GivenNoPaginationParameters_WhenAnimalsRequested_ShouldReturnPageOneOfOneAtTheDefaultPageSize()
    {
        var collection = await GetCollection(Url());

        collection.Page.Should().Be(1);
        collection.PageSize.Should().Be(25);
        collection.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task GivenPaginationParameters_WhenAnimalsRequested_ShouldReturnThatPageAndTheHoldingTotals()
    {
        var collection = await GetCollection(Url("page=2&page-size=3"));

        collection.Page.Should().Be(2);
        collection.PageSize.Should().Be(3);
        collection.TotalPages.Should().Be(3);
        collection.TotalRecords.Should().Be(TestBovineConstants.KnownCphAnimalCount);
        collection.Animals.Should().HaveCount(3);
        EarTags(collection).Should().ContainInOrder("UK324537113237", "UK324537113238", "UK324537113239");
    }

    [Fact]
    public async Task GivenNoOrderBy_WhenAnimalsRequested_ShouldSortByEarTagNumberAscending()
    {
        var collection = await GetCollection(Url());

        EarTags(collection).Should().BeInAscendingOrder();
    }

    [Fact]
    public async Task GivenOrderByAndDirection_WhenAnimalsRequested_ShouldSortAccordingly()
    {
        var collection = await GetCollection(Url("order-by=BirthDate&direction=Desc"));

        EarTags(collection).First().Should().Be("UK324537113241");
    }

    [Fact]
    public async Task GivenFilters_WhenAnimalsRequested_ShouldReturnOnlyTheMatchingAnimals()
    {
        var collection = await GetCollection(Url("status=Alive&sex=Female&breedCode=HOX&dateOnCPHFrom=2023-09-01"));

        EarTags(collection).Should().BeEquivalentTo(["UK324537113239", "UK324537113241"]);
        collection.TotalRecords.Should().Be(2);
    }

    [Fact]
    public async Task GivenRegisteredOnHoldingAssociation_WhenAnimalsRequested_ShouldReturnTheRegisteredAnimals()
    {
        var collection = await GetCollection(Url("holdingAssociation=RegisteredOnHolding"));

        collection.TotalRecords.Should().Be(TestBovineConstants.KnownCphAnimalCount);
        collection.Animals.First().DateOnCph.Should().Be(new DateOnly(2023, 3, 14));
    }

    [Fact]
    public async Task GivenSearchTerm_WhenAnimalsRequested_ShouldFilterAcrossTheHoldingAndReportTheMatchingTotals()
    {
        var collection = await GetCollection(Url("q=male"));

        EarTags(collection).Should().BeEquivalentTo(["UK324537113236", "UK324537113237", "UK324537113238"]);
        collection.TotalRecords.Should().Be(3);
        collection.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task GivenSearchTermMatchingNoAnimals_WhenAnimalsRequested_ShouldReturnOkWithAnEmptyCollection()
    {
        var collection = await GetCollection(Url("q=Aberdeen"));

        collection.Animals.Should().BeEmpty();
        collection.TotalRecords.Should().Be(0);
        collection.TotalPages.Should().Be(1);
        collection.Page.Should().Be(1);
    }

    [Fact]
    public async Task GivenKnownCphHoldingNoAnimals_WhenAnimalsRequested_ShouldReturnOkWithAnEmptyCollection()
    {
        var response = await testFixture.HttpClient.GetAsync(
            $"{TestEndpointConstants.ApiBovineAnimals}?CPH={Uri.EscapeDataString(TestBovineConstants.KnownCphWithNoAnimals)}",
            TestContext.Current.CancellationToken);

        var collection = await HttpResponseMessageUtilities.VerifyOk<AnimalCollectionDto>(response);

        collection.Animals.Should().BeEmpty();
        collection.TotalRecords.Should().Be(0);
        collection.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task GivenUnknownCph_WhenAnimalsRequested_ShouldReturnNotFound()
    {
        var response = await testFixture.HttpClient.GetAsync(
            $"{TestEndpointConstants.ApiBovineAnimals}?CPH={Uri.EscapeDataString(TestBovineConstants.UnknownCph)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenNoCph_WhenAnimalsRequested_ShouldReturnBadRequest()
    {
        var response = await testFixture.HttpClient.GetAsync(
            TestEndpointConstants.ApiBovineAnimals,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task<AnimalCollectionDto> GetCollection(string url)
    {
        var response = await testFixture.HttpClient.GetAsync(url, TestContext.Current.CancellationToken);

        return await HttpResponseMessageUtilities.VerifyOk<AnimalCollectionDto>(response);
    }

    private static string Url(string? queryString = null)
    {
        var url = $"{TestEndpointConstants.ApiBovineAnimals}?CPH={Uri.EscapeDataString(TestBovineConstants.KnownCph)}";

        return queryString is null ? url : $"{url}&{queryString}";
    }

    private static IEnumerable<string?> EarTags(AnimalCollectionDto collection)
        => collection.Animals.Select(a => a.Identifier?.Identifier);
}