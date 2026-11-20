using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories.Strategies;
using FluentAssertions;
using Moq;
using Npgsql;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Promotion;

public class PostgresPromotionStrategyTests
{
    private static PostgresPromotionStrategy Create(PromotionType type) => type switch
    {
        PromotionType.CtsBulk => new CtsBulkPromotionStrategy(Mock.Of<IPostgresDataSourceFactory>()),
        PromotionType.CtsDelta => new CtsDeltaPromotionStrategy(Mock.Of<IPostgresDataSourceFactory>()),
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    private static PromotionJob Job(PromotionType type, bool? deleteSource = null) =>
        new(type, "corr-1", deleteSource, "user");

    [Theory]
    [InlineData(PromotionType.CtsBulk)]
    [InlineData(PromotionType.CtsDelta)]
    public void GivenStrategy_WhenPromotionTypeRead_ShouldMatchItsType(PromotionType type)
    {
        Create(type).PromotionType.Should().Be(type);
    }

    [Theory]
    [InlineData(PromotionType.CtsBulk, "cads.prepare_cts_parallel_bulk_import(")]
    [InlineData(PromotionType.CtsDelta, "cads.prepare_cts_parallel_delta_import(")]
    public void GivenJob_WhenConfigureCommand_ShouldCallTheCorrectProcedure(PromotionType type, string expectedCall)
    {
        using var cmd = new NpgsqlCommand();

        Create(type).ConfigureCommand(cmd, Job(type));

        cmd.CommandText.Should().Contain($"CALL {expectedCall}");
        cmd.CommandText.Should().Contain("@delete_source").And.Contain("@range_size")
            .And.Contain("@truncate_cts").And.Contain("@correlation_id");
    }

    [Theory]
    [InlineData(PromotionType.CtsBulk)]
    [InlineData(PromotionType.CtsDelta)]
    public void GivenJob_WhenConfigureCommand_ShouldSetNoTimeoutAndFixedParameters(PromotionType type)
    {
        using var cmd = new NpgsqlCommand();

        Create(type).ConfigureCommand(cmd, Job(type));

        cmd.CommandTimeout.Should().Be(0);
        cmd.Parameters["range_size"].Value.Should().Be(100_000L);
        cmd.Parameters["truncate_cts"].Value.Should().Be(false);
        cmd.Parameters["correlation_id"].Value.Should().Be("corr-1");
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void GivenDeleteSource_WhenConfigureCommand_ShouldDefaultToTrueWhenNull(bool? deleteSource, bool expected)
    {
        using var cmd = new NpgsqlCommand();

        Create(PromotionType.CtsBulk).ConfigureCommand(cmd, Job(PromotionType.CtsBulk, deleteSource));

        cmd.Parameters["delete_source"].Value.Should().Be(expected);
    }

    [Fact]
    public void GivenLongResult_WhenToRunId_ShouldReturnIt()
    {
        Create(PromotionType.CtsBulk).ToRunId(42L).Should().Be(42L);
    }

    [Theory]
    [InlineData(PromotionType.CtsBulk, "prepare_cts_parallel_bulk_import")]
    [InlineData(PromotionType.CtsDelta, "prepare_cts_parallel_delta_import")]
    public void GivenNoRunId_WhenToRunId_ShouldThrowNamingTheProcedure(PromotionType type, string procedure)
    {
        var sut = Create(type);

        foreach (var bad in new object?[] { null, DBNull.Value, 42, "42" })
        {
            var act = () => sut.ToRunId(bad);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage($"{procedure} did not return a run id.");
        }
    }
}