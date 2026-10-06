using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.Api.Tests.Integration.Endpoints;

[Collection("ApiIntegration"), Trait("Dependence", "testcontainers")]
public class BovineEndpointTests(ApiContainerFixture apiContainerFixture)
{
    // # GetAnimalDetailsByIdentifier

    [Fact]
    public async Task GivenUnknownIdentifier_WhenGetAnimalDetailsByIdentifierRequested_ShouldReturnNotFound()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.UnknownIdentifier);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenValidIdentifier_WhenGetAnimalDetailsByIdentifierRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifier);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        result.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
        result.AnimalDetail.Should().NotBeNull();
        result.AnimalDetail!.Identifier!.Identifier.Should().Be(TestBovineConstants.KnownIdentifier);
    }

    [Fact]
    public async Task GivenAnimalWithParents_WhenGetAnimalDetailsByIdentifierRequested_ShouldReturnDamAndSire()
    {
        var response = await ExecuteTest(TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifier);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        var parentIdentifiers = result.AnimalDetail!.Parentage!
            .Select(p => p.AnimalIdentifier!.Identifier)
            .ToList();

        parentIdentifiers.Should().BeEquivalentTo(
            [TestBovineConstants.KnownDamIdentifier, TestBovineConstants.KnownSireIdentifier]);
    }

    [Fact]
    public async Task GivenAnimalWithoutParents_WhenGetAnimalDetailsByIdentifierRequested_ShouldReturnNoParentage()
    {
        // UK324537113250 is the seeded dead animal (n = 34) with no relationship rows
        var response = await ExecuteTest(
            TestEndpointConstants.ApiBovineAnimalsRoot + TestBovineConstants.KnownIdentifierNoParents);

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalDetailsDto>(response);

        result.AnimalDetail!.Parentage.Should().BeNullOrEmpty();
    }

    // # GetAnimalsOnHolding

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
    }

    [Fact]
    public async Task GivenKnownCph_WhenGetAnimalsOnHoldingRequested_ShouldReturnCurrentAnimalsOnly()
    {
        // 30 current; the 3 historical and 1 dead animal are excluded by default
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph, "pageSize=100"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphAnimalCount);
        result.Animals.Should().HaveCount(TestBovineConstants.KnownCphAnimalCount);
        result.Animals.Select(a => a.Identifier).Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public async Task GivenOtherCph_WhenGetAnimalsOnHoldingRequested_ShouldOnlyReturnThatHoldingsAnimals()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.OtherCph));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.Cph!.Identifier.Should().Be(TestBovineConstants.OtherCph);
        result.TotalRecords.Should().Be(TestBovineConstants.OtherCphAnimalCount);
        result.Animals.Should().HaveCount(TestBovineConstants.OtherCphAnimalCount);
    }

    [Fact]
    public async Task GivenUnknownCph_WhenGetAnimalsOnHoldingRequested_ShouldReturnEmptyCollection()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl("01/234/5678"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(0);
        result.Animals.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenPopulatedQueryString_WhenGetAnimalsOnHoldingRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&breedCode=HO&page=2&pageSize=2&orderBy=BirthDate&direction=Desc"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.ResourceType.Should().Be("AnimalCollection");
        result.Cph!.Identifier.Should().Be(TestBovineConstants.KnownCph);
        result.Page.Should().Be(2);
        result.PageSize.Should().Be(2);
        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphFemaleHoCount);   // 5
        result.Animals.Should().HaveCount(2);
    }

    [Fact]
    public async Task GivenSexAndBreedFilter_WhenGetAnimalsOnHoldingRequested_ShouldOnlyReturnMatches()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&breedCode=HO"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphFemaleHoCount);
        result.Animals.Should().HaveCount(TestBovineConstants.KnownCphFemaleHoCount);
        result.Animals.Should().OnlyContain(a => a.Sex == "Female");
    }

    [Fact]
    public async Task GivenSexFilterOnly_WhenGetAnimalsOnHoldingRequested_ShouldReturnHalfTheHolding()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&pageSize=100"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(15);
    }

    [Fact]
    public async Task GivenLowerCaseBreedCode_WhenGetAnimalsOnHoldingRequested_ShouldMatchCaseInsensitively()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&breedCode=ho"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.TotalRecords.Should().Be(TestBovineConstants.KnownCphFemaleHoCount);
    }

    [Fact]
    public async Task GivenPaging_WhenGetAnimalsOnHoldingRequested_ShouldPageWithoutOverlapAndKeepTotal()
    {
        var page1 = await GetPage(1);
        var page2 = await GetPage(2);
        var page3 = await GetPage(3);   // 5 matches -> 2 + 2 + 1

        page1.Animals.Should().HaveCount(2);
        page2.Animals.Should().HaveCount(2);
        page3.Animals.Should().HaveCount(1);

        new[] { page1, page2, page3 }.Should().OnlyContain(p => p.TotalRecords == 5);

        page1.Animals.Concat(page2.Animals).Concat(page3.Animals)
            .Select(a => a.Identifier).Should().OnlyHaveUniqueItems();

        async Task<AnimalsOnHoldingDto> GetPage(int page)
        {
            var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
                $"sex=Female&breedCode=HO&pageSize=2&page={page}"));
            return await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);
        }
    }

    [Fact]
    public async Task GivenPageBeyondResults_WhenGetAnimalsOnHoldingRequested_ShouldReturnEmptyPage()
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            "sex=Female&breedCode=HO&pageSize=2&page=10"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        result.Animals.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Asc")]
    [InlineData("Desc")]
    public async Task GivenOrderByBirthDate_WhenGetAnimalsOnHoldingRequested_ShouldSortByBirthDate(string direction)
    {
        var response = await ExecuteTest(GetAnimalsOnHoldingUrl(TestBovineConstants.KnownCph,
            $"orderBy=BirthDate&direction={direction}&pageSize=100"));

        var result = await HttpResponseMessageUtilities.VerifyOk<AnimalsOnHoldingDto>(response);

        var dates = result.Animals.Select(a => a.BirthDate).ToList();

        if (direction == "Asc")
            dates.Should().BeInAscendingOrder();
        else
            dates.Should().BeInDescendingOrder();
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