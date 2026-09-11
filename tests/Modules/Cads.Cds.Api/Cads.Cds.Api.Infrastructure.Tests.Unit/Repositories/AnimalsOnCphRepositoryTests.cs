using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.Api.Core.Domain.Paging;
using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.Api.Infrastructure.Persistence.Repositories;
using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;

namespace Cads.Cds.Api.Infrastructure.Tests.Unit.Repositories;

public class AnimalsOnCphRepositoryTests
{
    private static readonly List<AnimalHoldingDto> Holdings =
    [
        new()
        {
            Cph = "08/065/0077",
            Animals =
            [
                Animal("UK324537113234"),
                Animal("UK324537113235"),
                Animal("UK324537113236")
            ]
        },
        new() { Cph = "08/065/0078", Animals = [] },
    ];

    [Fact]
    public async Task HoldingExistsAsync_WhenCphIsRecognised_ShouldReturnTrue()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.HoldingExistsAsync("08/065/0077", TestContext.Current.CancellationToken);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task HoldingExistsAsync_WhenTheHoldingHoldsNoAnimals_ShouldReturnTrue()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.HoldingExistsAsync("08/065/0078", TestContext.Current.CancellationToken);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task HoldingExistsAsync_WhenCphIsNotRecognised_ShouldReturnFalse()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.HoldingExistsAsync("99/999/9999", TestContext.Current.CancellationToken);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task HoldingExistsAsync_WhenStaticDataDisabled_ShouldReturnFalse()
    {
        var sut = CreateSut(enabled: false);

        var result = await sut.HoldingExistsAsync("08/065/0077", TestContext.Current.CancellationToken);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task QueryAnimalsAsync_ShouldOnlyQueryTheAnimalsOnTheMatchingHolding()
    {
        var sut = CreateSut(enabled: true);

        var result = await Query(sut, "08/065/0078");

        result.TotalRecords.Should().Be(0);
        result.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task QueryAnimalsAsync_ShouldApplyTheSuppliedShapeAndOrder()
    {
        var sut = CreateSut(enabled: true);

        var result = await Query(
            sut,
            "08/065/0077",
            shape: animals => animals.Where(a => a.Identifier!.Identifier != "UK324537113235"),
            order: animals => animals.OrderByDescending(a => a.Identifier!.Identifier));

        result.TotalRecords.Should().Be(2);
        result.Items.Select(a => a.Identifier!.Identifier)
            .Should().ContainInOrder("UK324537113236", "UK324537113234");
    }

    [Fact]
    public async Task QueryAnimalsAsync_ShouldPageTheShapedResultsAndTotalOverAllOfThem()
    {
        var sut = CreateSut(enabled: true);

        var result = await Query(sut, "08/065/0077", page: 2, pageSize: 2);

        result.TotalRecords.Should().Be(3);
        result.Items.Should().HaveCount(1);
        result.Items.Single().Identifier!.Identifier.Should().Be("UK324537113236");
    }

    [Fact]
    public async Task QueryAnimalsAsync_WhenStaticDataDisabled_ShouldReturnNoAnimals()
    {
        var sut = CreateSut(enabled: false);

        var result = await Query(sut, "08/065/0077");

        result.TotalRecords.Should().Be(0);
        result.Items.Should().BeEmpty();
    }

    private static Task<PagedResult<AnimalOnCphDto>> Query(
        AnimalsOnCphRepository sut,
        string cph,
        Func<IQueryable<AnimalOnCphDto>, IQueryable<AnimalOnCphDto>>? shape = null,
        Func<IQueryable<AnimalOnCphDto>, IOrderedQueryable<AnimalOnCphDto>>? order = null,
        int page = 1,
        int pageSize = 25)
        => sut.QueryAnimalsAsync(
            cph,
            shape ?? (animals => animals),
            order ?? (animals => animals.OrderBy(a => a.Identifier!.Identifier)),
            page,
            pageSize,
            TestContext.Current.CancellationToken);

    private static AnimalOnCphDto Animal(string earTag)
        => new() { Identifier = new AnimalIdentifierDto { Identifier = earTag } };

    private static AnimalsOnCphRepository CreateSut(bool enabled)
    {
        var fileService = new Mock<IFileService>();
        fileService
            .Setup(f => f.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<AnimalHoldingDto>>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Holdings);

        var env = new Mock<IHostEnvironment>();
        env.SetupGet(e => e.ContentRootPath).Returns("/app");

        var options = Options.Create(new ApiModuleConfiguration
        {
            StaticData = new StaticDataConfig { Enabled = enabled, Path = "StaticData" }
        });

        return new AnimalsOnCphRepository(env.Object, fileService.Object, options);
    }
}