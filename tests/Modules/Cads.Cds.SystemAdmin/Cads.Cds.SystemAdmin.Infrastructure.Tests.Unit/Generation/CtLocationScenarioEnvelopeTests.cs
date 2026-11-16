using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios.CtLocationScenarios;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using static Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation.EnvelopeTestHelpers;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation;

/// <summary>The ct_locations scenarios produce envelopes the Bridge and staging COPY accept.</summary>
public class CtLocationScenarioEnvelopeTests
{
    [Fact]
    public async Task BulkScenario_ProducesABulkEnvelope_WithoutTransType()
    {
        using var db = CreateDbContext(nameof(BulkScenario_ProducesABulkEnvelope_WithoutTransType));
        var scenario = new CtLocationBulkScenario(
            db, new FileNameGenerator(), CreateFileAssembler(), CreateContentGeneratorFactory(), CreateEnvelopeTableResolver());

        var result = await scenario.ExecuteAsync(
            new CreateGenerationRequestDto { Scenario = scenario.Name, RowCount = 3 }, TestContext.Current.CancellationToken);

        var (columns, dataLines) = BridgeSplit(result.Content);

        Assert.Equal(["record_type", "record_count", "loc_id"], columns.Take(3));
        Assert.DoesNotContain("trans_type", columns);
        Assert.DoesNotContain(columns, c => c.Contains("_aud_"));
        Assert.Equal(3, dataLines.Length);
        Assert.Equal(3, ParseTrailer(result.Content, $"cads/cts/bulk/{result.FileName}"));
    }

    [Fact]
    public async Task UpdateScenario_ProducesADeltaEnvelope_WithAudTypeU()
    {
        using var db = CreateDbContext(nameof(UpdateScenario_ProducesADeltaEnvelope_WithAudTypeU));
        var scenario = new CtLocationUpdateScenario(
            db, new FileNameGenerator(), CreateFileAssembler(), CreateContentGeneratorFactory(), CreateEnvelopeTableResolver());

        var result = await scenario.ExecuteAsync(
            new CreateGenerationRequestDto { Scenario = scenario.Name, BusinessKeys = [501m, 502m] }, TestContext.Current.CancellationToken);

        var (columns, dataLines) = BridgeSplit(result.Content);

        Assert.Equal(["record_type", "record_count", "loc_aud_id", "loc_aud_type", "loc_aud_datetime", "loc_id"], columns.Take(6));
        Assert.DoesNotContain("trans_type", columns);
        Assert.All(dataLines, l => Assert.Equal("U", Fields(l)[3]));
        Assert.Equal(["501", "502"], dataLines.Select(l => Fields(l)[5]));
        Assert.Equal(2, ParseTrailer(result.Content, $"cads/cts/delta/{result.FileName}"));
    }
}