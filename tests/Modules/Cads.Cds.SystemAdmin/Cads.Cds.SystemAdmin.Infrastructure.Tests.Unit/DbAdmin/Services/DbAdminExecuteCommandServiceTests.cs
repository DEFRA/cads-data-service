using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;
using FluentAssertions;
using Moq;
using Npgsql;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.DbAdmin.Services;

public class DbAdminExecuteCommandServiceTests
{
    private readonly Mock<IPostgresDataSourceFactory> _factory = new();

    // Points at localhost on a port nothing is listening on, with a very short timeout.
    // This lets Npgsql fail fast (connection refused) without requiring a real database
    // or network access, while still exercising the real Npgsql connection pipeline.
    private const string UnreachableConnectionString =
        "Host=127.0.0.1;Port=1;Username=test;Password=test;Timeout=2;Command Timeout=2";

    [Fact]
    public async Task ExecuteAsync_ShouldRequestDataSource_UsingDefaultConnectionIdentifier()
    {
        var sut = CreateSut(out var dataSource);

        await using var _ = dataSource;

        var act = () => sut.ExecuteAsync("sessions_by_state", null, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<Exception>();

        _factory.Verify(x => x.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenConnectionCannotBeEstablished()
    {
        var sut = CreateSut(out var dataSource);

        await using var _ = dataSource;

        var act = () => sut.ExecuteAsync("sessions_by_state", null, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NpgsqlException>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowOperationCanceledException_WhenCancellationTokenIsAlreadyCancelled()
    {
        var sut = CreateSut(out var dataSource);

        await using var _ = dataSource;

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        var act = () => sut.ExecuteAsync("sessions_by_state", null, cts.Token);

        await act.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldNotThrow_ForArgsSerialization_WhenArgsIsNull()
    {
        // Ensures the null-coalescing of `args` to an empty JSON object happens before
        // any connection/database related failure - i.e. it never throws a NullReferenceException.
        var sut = CreateSut(out var dataSource);

        await using var _ = dataSource;

        var act = () => sut.ExecuteAsync("sessions_by_state", null, TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldNotThrow_ForArgsSerialization_WhenArgsIsProvided()
    {
        var sut = CreateSut(out var dataSource);

        await using var _ = dataSource;

        using var argsDoc = JsonDocument.Parse("""{"pid":123}""");

        var act = () => sut.ExecuteAsync("cancel_query", argsDoc.RootElement, TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync<NullReferenceException>();
    }

    private DbAdminExecuteCommandService CreateSut(out NpgsqlDataSource dataSource)
    {
        dataSource = NpgsqlDataSource.Create(UnreachableConnectionString);

        _factory
            .Setup(x => x.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier))
            .Returns(dataSource);

        return new DbAdminExecuteCommandService(_factory.Object);
    }
}