using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;
using Cads.Cds.Api.Application.Tests.Unit.Fakes.Repositories;
using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.DTOs.Bovine;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class AnimalsOnCphQueryAdapterTests
{
    private const string Cph = "08/065/0077";

    [Fact]
    public async Task GetAsync_WhenCphIsNotRecognised_ShouldReturnNull()
    {
        var sut = CreateSut(holding: null);

        var result = await sut.GetAsync(Query(), TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_WhenCphIsRecognisedButHoldsNoAnimals_ShouldReturnEmptyCollectionAsPageOneOfOne()
    {
        var sut = CreateSut(new AnimalHoldingDto { Cph = Cph, Animals = [] });

        var result = await sut.GetAsync(Query(), TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.ResourceType.Should().Be("AnimalCollection");
        result.Animals.Should().BeEmpty();
        result.Page.Should().Be(1);
        result.TotalPages.Should().Be(1);
        result.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task GetAsync_WithoutPaginationParameters_ShouldReturnPageOneOfOneAtTheDefaultPageSize()
    {
        var sut = CreateSut(HoldingWith(Animal("UK0003"), Animal("UK0001"), Animal("UK0002")));

        var result = await sut.GetAsync(Query(), TestContext.Current.CancellationToken);

        result!.Page.Should().Be(1);
        result.PageSize.Should().Be(25);
        result.TotalPages.Should().Be(1);
        result.TotalRecords.Should().Be(3);
        result.Animals.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetAsync_WithoutOrderBy_ShouldSortByEarTagNumberAscending()
    {
        var sut = CreateSut(HoldingWith(Animal("UK0003"), Animal("UK0001"), Animal("UK0002")));

        var result = await sut.GetAsync(Query(), TestContext.Current.CancellationToken);

        EarTags(result!).Should().ContainInOrder("UK0001", "UK0002", "UK0003");
    }

    [Fact]
    public async Task GetAsync_WithPaginationParameters_ShouldReturnThatPageAndTheTotalsForTheHolding()
    {
        var animals = Enumerable.Range(1, 30).Select(i => Animal(EarTag("UK", i))).ToArray();
        var sut = CreateSut(HoldingWith(animals));

        var query = Query();
        query.Page = 2;
        query.PageSize = 25;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        result!.Page.Should().Be(2);
        result.PageSize.Should().Be(25);
        result.TotalPages.Should().Be(2);
        result.TotalRecords.Should().Be(30);
        result.Animals.Should().HaveCount(5);
        EarTags(result).Should().ContainInOrder("UK0026", "UK0027", "UK0028", "UK0029", "UK0030");
    }

    [Fact]
    public async Task GetAsync_ShouldSortAcrossTheWholeHoldingBeforePaging()
    {
        var animals = Enumerable.Range(1, 30).Select(i => Animal(EarTag("UK", i))).ToArray();
        var sut = CreateSut(HoldingWith(animals));

        var query = Query();
        query.Page = 1;
        query.PageSize = 2;
        query.Direction = SortDirection.Desc;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().ContainInOrder("UK0030", "UK0029");
    }

    [Theory]
    [InlineData(AnimalOrderBy.Identifier, SortDirection.Asc, "UK0001")]
    [InlineData(AnimalOrderBy.Identifier, SortDirection.Desc, "UK0003")]
    [InlineData(AnimalOrderBy.BirthDate, SortDirection.Asc, "UK0002")]
    [InlineData(AnimalOrderBy.BirthDate, SortDirection.Desc, "UK0003")]
    [InlineData(AnimalOrderBy.DateOnCPH, SortDirection.Asc, "UK0003")]
    [InlineData(AnimalOrderBy.DateOnCPH, SortDirection.Desc, "UK0001")]
    [InlineData(AnimalOrderBy.Sex, SortDirection.Asc, "UK0002")]
    [InlineData(AnimalOrderBy.Sex, SortDirection.Desc, "UK0001")]
    [InlineData(AnimalOrderBy.BreedCode, SortDirection.Asc, "UK0003")]
    [InlineData(AnimalOrderBy.BreedCode, SortDirection.Desc, "UK0002")]
    public async Task GetAsync_ShouldSortOnEachSupportedColumnInBothDirections(
        AnimalOrderBy orderBy,
        SortDirection direction,
        string expectedFirstEarTag)
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", birthDate: "2023-03-10", dateOnCph: "2023-06-01", sex: "Male", breedCode: "HO"),
            Animal("UK0002", birthDate: "2023-01-05", dateOnCph: "2023-05-01", sex: "Female", breedCode: "SH"),
            Animal("UK0003", birthDate: "2023-09-20", dateOnCph: "2023-04-01", sex: "Male", breedCode: "AA")));

        var query = Query();
        query.OrderBy = orderBy;
        query.Direction = direction;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).First().Should().Be(expectedFirstEarTag);
    }

    [Fact]
    public async Task GetAsync_WhenDirectionIsNotSupplied_ShouldSortAscending()
    {
        var sut = CreateSut(HoldingWith(Animal("UK0003"), Animal("UK0001")));

        var query = Query();
        query.OrderBy = AnimalOrderBy.Identifier;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().ContainInOrder("UK0001", "UK0003");
    }

    [Fact]
    public async Task GetAsync_WhenHoldingAssociationIsMovedOnHolding_ShouldTakeDateOnCphFromTheMoveOnDate()
    {
        var sut = CreateSut(HoldingWith(new AnimalOnCphDto
        {
            Identifier = new AnimalIdentifierDto { Identifier = "UK0001" },
            MovedOnCph = new DateOnly(2023, 3, 14),
            RegisteredOnCph = new DateOnly(2023, 3, 10)
        }));

        var result = await sut.GetAsync(Query(), TestContext.Current.CancellationToken);

        result!.Animals.Single().DateOnCph.Should().Be(new DateOnly(2023, 3, 14));
    }

    [Fact]
    public async Task GetAsync_WhenHoldingAssociationIsRegisteredOnHolding_ShouldTakeDateOnCphFromTheRegistrationDate()
    {
        var sut = CreateSut(HoldingWith(new AnimalOnCphDto
        {
            Identifier = new AnimalIdentifierDto { Identifier = "UK0001" },
            MovedOnCph = new DateOnly(2023, 3, 14),
            RegisteredOnCph = new DateOnly(2023, 3, 10)
        }));

        var query = Query();
        query.HoldingAssociation = HoldingAssociation.RegisteredOnHolding;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        result!.Animals.Single().DateOnCph.Should().Be(new DateOnly(2023, 3, 10));
    }

    [Fact]
    public async Task GetAsync_ShouldExcludeAnimalsWithNoDateForTheRequestedAssociation()
    {
        var sut = CreateSut(HoldingWith(new AnimalOnCphDto
        {
            Identifier = new AnimalIdentifierDto { Identifier = "UK0001" },
            MovedOnCph = new DateOnly(2023, 3, 14),
            RegisteredOnCph = null
        }));

        var query = Query();
        query.HoldingAssociation = HoldingAssociation.RegisteredOnHolding;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        result!.Animals.Should().BeEmpty();
        result.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task GetAsync_ShouldFilterByStatus()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", status: "Alive"),
            Animal("UK0002", status: "OffFarm"),
            Animal("UK0003", status: "Dead")));

        var query = Query();
        query.Status = [AnimalStatus.Alive, AnimalStatus.OffFarm];

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0001", "UK0002"]);
    }

    [Fact]
    public async Task GetAsync_ShouldFilterBySex()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", sex: "Female"),
            Animal("UK0002", sex: "Male")));

        var query = Query();
        query.Sex = "Female";

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0001"]);
    }

    [Fact]
    public async Task GetAsync_ShouldFilterByBreedCode()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", breedCode: "HO"),
            Animal("UK0002", breedCode: "HOX"),
            Animal("UK0003", breedCode: "AA")));

        var query = Query();
        query.BreedCode = ["HO", "HOX"];

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0001", "UK0002"]);
    }

    [Fact]
    public async Task GetAsync_ShouldFilterByDateOnCphFrom()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", dateOnCph: "2025-12-31"),
            Animal("UK0002", dateOnCph: "2026-01-01"),
            Animal("UK0003", dateOnCph: "2026-02-01")));

        var query = Query();
        query.DateOnCphFrom = new DateOnly(2026, 1, 1);

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0002", "UK0003"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldMatchPartOfTheEarTagNumber()
    {
        var sut = CreateSut(HoldingWith(Animal("UK324537113234"), Animal("UK324537113235"), Animal("UK999999999999")));

        var query = Query();
        query.Q = "11323";

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK324537113234", "UK324537113235"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldMatchTheWholeSexAndNotAFragmentOfIt()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", sex: "Male"),
            Animal("UK0002", sex: "Female")));

        var query = Query();
        query.Q = "male";

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0001"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldMatchTheBreedCode()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", breedCode: "HO", breedName: "Holstein Friesian"),
            Animal("UK0002", breedCode: "AA", breedName: "Aberdeen Angus")));

        var query = Query();
        query.Q = "aa";

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK0002"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldMatchAnyPartOfTheBreedName()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK0001", breedCode: "HO", breedName: "Holstein Friesian"),
            Animal("UK0002", breedCode: "HOX", breedName: "Holstein Friesian Cross"),
            Animal("UK0003", breedCode: "AA", breedName: "Aberdeen Angus")));

        var crossQuery = Query();
        crossQuery.Q = "cross";

        var crossResult = await sut.GetAsync(crossQuery, TestContext.Current.CancellationToken);

        EarTags(crossResult!).Should().BeEquivalentTo(["UK0002"]);

        var partialQuery = Query();
        partialQuery.Q = "olstein";

        var partialResult = await sut.GetAsync(partialQuery, TestContext.Current.CancellationToken);

        EarTags(partialResult!).Should().BeEquivalentTo(["UK0001", "UK0002"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldReturnAnAnimalWhereAnyOneOfTheFieldsMatches()
    {
        var sut = CreateSut(HoldingWith(
            Animal("UK00HO01", sex: "Female", breedCode: "AA", breedName: "Aberdeen Angus"),
            Animal("UK0002", sex: "Female", breedCode: "HO", breedName: "Holstein Friesian"),
            Animal("UK0003", sex: "Female", breedCode: "SH", breedName: "Shorthorn")));

        var query = Query();
        query.Q = "HO";

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        EarTags(result!).Should().BeEquivalentTo(["UK00HO01", "UK0002", "UK0003"]);
    }

    [Fact]
    public async Task GetAsync_WithSearchTerm_ShouldPageAndTotalOverTheMatchingSetOnly()
    {
        var matching = Enumerable.Range(1, 6).Select(i => Animal(EarTag("UK99", i), sex: "Male")).ToArray();
        var other = Enumerable.Range(1, 20).Select(i => Animal(EarTag("UK11", i), sex: "Female")).ToArray();

        var sut = CreateSut(HoldingWith([.. matching, .. other]));

        var query = Query();
        query.Q = "male";
        query.Page = 2;
        query.PageSize = 4;

        var result = await sut.GetAsync(query, TestContext.Current.CancellationToken);

        result!.TotalRecords.Should().Be(6);
        result.TotalPages.Should().Be(2);
        result.Animals.Should().HaveCount(2);
        EarTags(result).Should().ContainInOrder("UK990005", "UK990006");
    }

    private static GetAnimalsOnCph Query() => new() { Cph = Cph };

    private static string EarTag(string prefix, int number) => prefix + number.ToString("D4");

    private static IEnumerable<string?> EarTags(AnimalCollectionDto collection)
        => collection.Animals.Select(a => a.Identifier?.Identifier);

    private static AnimalHoldingDto HoldingWith(params AnimalOnCphDto[] animals)
        => new() { Cph = Cph, Animals = animals };

    private static AnimalOnCphDto Animal(
        string earTag,
        string birthDate = "2023-01-01",
        string dateOnCph = "2023-01-02",
        string sex = "Female",
        string status = "Alive",
        string breedCode = "HO",
        string? breedName = "Holstein Friesian")
        => new()
        {
            Identifier = new AnimalIdentifierDto
            {
                Schema = "uk.gov.defra.ear-tag.conventional",
                Identifier = earTag
            },
            BirthDate = DateOnly.Parse(birthDate),
            MovedOnCph = DateOnly.Parse(dateOnCph),
            RegisteredOnCph = DateOnly.Parse(dateOnCph),
            DateOffCph = null,
            Species = "Cattle",
            Sex = sex,
            Status = status,
            BreedCode = new BreedCodeDto
            {
                Schema = "cts.breed",
                BreedName = breedName,
                Identifier = breedCode
            }
        };

    private static AnimalsOnCphQueryAdapter CreateSut(AnimalHoldingDto? holding)
        => new(holding is null
            ? new FakeAnimalsOnCphRepository()
            : new FakeAnimalsOnCphRepository(holding));
}