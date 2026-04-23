using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Testing.Support.Seeding;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Tests.Integration.Repositories;

[Collection("MiBffRepositoryIntegration"), Trait("Dependence", "testcontainers")]
public class MiReportRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly PostgresFixture _postgresFixture;
    private MiBffReadDbContext _dbContext = null!;
    private MiReportRepository _repository = null!;

    public MiReportRepositoryIntegrationTests(PostgresFixture postgresFixture)
    {
        _postgresFixture = postgresFixture;
    }

    public async ValueTask InitializeAsync()
    {
        // Seed CT table data directly via SQL — Liquibase has already created the
        // tables and the get_births_summary function by the time tests run
        MiBffBirthSummaryDataSeeder.Seed(_postgresFixture.Container!);

        // Build a DbContext pointed at the local mapped port (not the Docker-network alias)
        var options = new DbContextOptionsBuilder<MiBffReadDbContext>()
            .UseNpgsql(MiBffBirthSummaryDataSeeder.LocalConnectionString(_postgresFixture.Container!))
            .Options;

        _dbContext = new MiBffReadDbContext(options);
        _repository = new MiReportRepository(_dbContext);

        await Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenAnimalsExistInDateRange_ReturnsSummaryRows()
    {
        // Act
        var results = await _repository.GetBirthSummaryAsync(
            MiBffBirthSummaryDataSeeder.QueryFrom,
            MiBffBirthSummaryDataSeeder.QueryTo,
            TestContext.Current.CancellationToken);

        // Assert
        var list = results.ToList();
        list.Should().NotBeNull();
        list.Should().NotBeEmpty();
        list.Should().AllSatisfy(r => r.NumberOfBirths.Should().BeGreaterThan(0));
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenAnimalsExistInDateRange_ExcludesAnimalsOutsideRange()
    {
        // The out-of-range animal has birth year 2021 — it must not appear
        var results = await _repository.GetBirthSummaryAsync(
            MiBffBirthSummaryDataSeeder.QueryFrom,
            MiBffBirthSummaryDataSeeder.QueryTo,
            TestContext.Current.CancellationToken);

        var list = results.ToList();
        list.Should().NotContain(r => r.BirthYear == MiBffBirthSummaryDataSeeder.BirthDateOutOfRange.Year);
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenAnimalsExistInDateRange_ReturnsCorrectBreedType()
    {
        var results = await _repository.GetBirthSummaryAsync(
            MiBffBirthSummaryDataSeeder.QueryFrom,
            MiBffBirthSummaryDataSeeder.QueryTo,
            TestContext.Current.CancellationToken);

        var list = results.ToList();
        list.Should().Contain(r => r.BreedType == "Dairy");
        list.Should().Contain(r => r.BreedType == "Non Dairy");
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenDateRangeHasNoAnimals_ReturnsEmpty()
    {
        // A date range with no seeded animals
        var results = await _repository.GetBirthSummaryAsync(
            new DateOnly(2000, 1, 1),
            new DateOnly(2001, 1, 1),
            TestContext.Current.CancellationToken);

        results.Should().BeEmpty();
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenAnimalsExistInDateRange_ReturnsCorrectCountry()
    {
        var results = await _repository.GetBirthSummaryAsync(
            MiBffBirthSummaryDataSeeder.QueryFrom,
            MiBffBirthSummaryDataSeeder.QueryTo,
            TestContext.Current.CancellationToken);

        var list = results.ToList();
        list.Should().Contain(r => r.Country == "England");
        list.Should().Contain(r => r.Country == "Wales");
    }

    [Fact]
    public async Task GetBirthSummaryAsync_WhenAnimalsExistInDateRange_ApplicationTypeIsMapped()
    {
        // vap_application_type = 'B' should map to "Birth Application"
        var results = await _repository.GetBirthSummaryAsync(
            MiBffBirthSummaryDataSeeder.QueryFrom,
            MiBffBirthSummaryDataSeeder.QueryTo,
            TestContext.Current.CancellationToken);

        results.Should().Contain(r => r.ApplicationType == "Birth Application");
    }
}