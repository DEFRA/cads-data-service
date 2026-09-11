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

public class AnimalDetailRepositoryTests
{
    private static readonly List<AnimalDetailDto> s_animals =
    [
        new() { Identifier = "UK324537113234" },
        new() { Identifier = "UK324537113243" },
    ];

    [Fact]
    public async Task GetByIdentifierAsync_ShouldReturnOnlyMatchingAnimal()
    {
        var sut = CreateSut(enabled: true);

        var result = await sut.GetByIdentifierAsync("UK324537113243", TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Identifier.Should().Be("UK324537113243");
    }

    [Fact]
    public async Task GetByIdentifierAsync_WhenStaticDataDisabled_ShouldReturnNull()
    {
        var sut = CreateSut(enabled: false);

        var result = await sut.GetByIdentifierAsync("UK324537113234", TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }

    private static AnimalDetailRepository CreateSut(bool enabled)
    {
        var fileService = new Mock<IFileService>();
        fileService
            .Setup(f => f.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<AnimalDetailDto>>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(s_animals);

        var env = new Mock<IHostEnvironment>();
        env.SetupGet(e => e.ContentRootPath).Returns("/app");

        var options = Options.Create(new ApiModuleConfiguration
        {
            StaticData = new StaticDataConfig { Enabled = enabled, Path = "StaticData" }
        });

        return new AnimalDetailRepository(env.Object, fileService.Object, options);
    }
}