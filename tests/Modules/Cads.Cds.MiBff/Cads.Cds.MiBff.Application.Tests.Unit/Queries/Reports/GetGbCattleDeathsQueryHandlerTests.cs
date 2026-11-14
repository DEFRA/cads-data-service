using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using FluentAssertions;
using Moq;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries.Reports;

public class GetGbCattleDeathsQueryHandlerTests
{
    private readonly Mock<IMiDeathSummaryRepository> _repositoryMock = new();

    private GetGbCattleDeathsQueryHandler CreateSut() => new(_repositoryMock.Object);

    [Fact]
    public async Task Handle_QueriesRepositoryForWholeYear()
    {
        _repositoryMock
            .Setup(r => r.GetDeathSummaryAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        await CreateSut().Handle(new GetGbCattleDeathsQuery(GetGbCattleDeathsQuery.ExpectedKey, 2024), TestContext.Current.CancellationToken);

        _repositoryMock.Verify(r => r.GetDeathSummaryAsync(
            new DateOnly(2024, 1, 1), new DateOnly(2025, 1, 1), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsJsonStreamOfResults()
    {
        _repositoryMock
            .Setup(r => r.GetDeathSummaryAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
            [
                new MiDeathSummary { DeathYear = 2024, DeathMonth = "January", Country = "England", Breed = "Angus", NumberOfDeaths = 12 },
                new MiDeathSummary { DeathYear = 2024, DeathMonth = "February", Country = "Wales", County = null, NumberOfDeaths = 3 }
            ]);

        var result = await CreateSut().Handle(
            new GetGbCattleDeathsQuery(GetGbCattleDeathsQuery.ExpectedKey, 2024), TestContext.Current.CancellationToken);

        result.ContentType.Should().Be("application/json");
        result.FileName.Should().StartWith("gb_cattle_deaths_").And.EndWith(".json");
        result.Stream.Position.Should().Be(0);

        using var document = await JsonDocument.ParseAsync(result.Stream, cancellationToken: TestContext.Current.CancellationToken);
        var rows = document.RootElement;
        rows.GetArrayLength().Should().Be(2);
        rows[0].GetProperty("deathMonth").GetString().Should().Be("January");
        rows[0].GetProperty("numberOfDeaths").GetInt64().Should().Be(12);
        rows[1].GetProperty("country").GetString().Should().Be("Wales");
        rows[1].GetProperty("county").ValueKind.Should().Be(JsonValueKind.Null);
    }

    [Fact]
    public async Task Handle_WithNoResults_ReturnsEmptyJsonArray()
    {
        _repositoryMock
            .Setup(r => r.GetDeathSummaryAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await CreateSut().Handle(
            new GetGbCattleDeathsQuery(GetGbCattleDeathsQuery.ExpectedKey, 2000), TestContext.Current.CancellationToken);

        new StreamReader(result.Stream).ReadToEnd().Should().Be("[]");
    }
}