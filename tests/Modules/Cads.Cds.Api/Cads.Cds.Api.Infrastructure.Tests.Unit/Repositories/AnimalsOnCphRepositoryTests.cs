using Cads.Cds.Api.Core.Configuration;
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
            Animals = [new AnimalOnCphDto { Identifier = new AnimalIdentifierDto { Identifier = "UK324537113234" } }]
        },
        new() { Cph = "08/065/0078", Animals = [] },
    ];

    [Fact]
    public async Task GetByCphAsync_ShouldReturnOnlyTheMatchingHolding()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.GetByCphAsync("08/065/0077", TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Cph.Should().Be("08/065/0077");
        result.Animals.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetByCphAsync_WhenTheHoldingHoldsNoAnimals_ShouldReturnTheHoldingWithAnEmptyCollection()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.GetByCphAsync("08/065/0078", TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Animals.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByCphAsync_WhenCphIsNotRecognised_ShouldReturnNull()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.GetByCphAsync("99/999/9999", TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByCphAsync_WhenStaticDataDisabled_ShouldReturnNull()
    {
        var sut = CreateSut(enabled: false);

        var result = await sut.GetByCphAsync("08/065/0077", TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }

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