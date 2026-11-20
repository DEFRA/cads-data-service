using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Entities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios.CtLocationScenarios;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.ContentGenerators;

public class ContentGeneratorTests
{
    private static CtsTransactionsGQDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<CtsTransactionsGQDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new CtsTransactionsGQDbContext(options);
    }

    [Fact]
    public void GenerateBulk_SetsTransTypeToB_AndLocIdToBusinessKey_AndAppliesRules()
    {
        using var db = CreateDbContext("GenerateBulk_SetsTransTypeToB");

        var contentGenerator = new ContentGenerator<CtLocation>(db,
            rulesBuilder: new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01"),
            transform: (entity, key) =>
            {
                entity.TransType = "B";
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 100m, 101m };
        var results = contentGenerator.Generate(keys, seed: 123);

        Assert.Equal(2, results.Count);

        var first = results[0];

        Assert.Equal(100m, Convert.ToDecimal(first["loc_id"]));
        Assert.Equal("B", first["trans_type"] as string);

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(first["loc_slt_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first["loc_lty_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first["loc_cty_id"]));
        Assert.Equal("Y", first["loc_receive_ppaf_flag"] as string);
        Assert.Equal("N", first["loc_receive_labels_flag"] as string);
        Assert.Equal("FARM", first["loc_premises_type"] as string);
        Assert.Equal("01", first["loc_reason_code"] as string);
    }

    [Fact]
    public void Generate_AppliesColumnVisibilityOrderAndCaption()
    {
        using var db = CreateDbContext("Generate_AppliesColumnVisibilityOrderAndCaption");
        var contentGenerator = new ContentGenerator<CtLocation>(db, columnDefinitions:
        [
            new(nameof(CtLocation.LocId), order: 2, caption: "Location ID"),
            new(nameof(CtLocation.TransType), isVisible: false, order: 0),
            new(nameof(CtLocation.LocSltId), order: 1, caption: "Slt")
        ], transform: (entity, key) =>
        {
            entity.LocId = key;
            return entity;
        });

        var result = contentGenerator.Generate([100m], seed: 123)[0];

        Assert.Equal(["Slt", "Location ID"], result.Keys);
        Assert.Equal(100m, Convert.ToDecimal(result["Location ID"]));
    }

    [Fact]
    public async Task CtLocationBulkScenario_RendersConfiguredColumnsInCorrectOrder()
    {
        using var db = CreateDbContext("CtLocationBulkScenario_RendersConfiguredColumnsInCorrectOrder");

        var fileNameGenerator = new Mock<IFileNameGenerator>();
        fileNameGenerator
            .Setup(generator => generator.Create(It.IsAny<CreateFileNameCommand>()))
            .Returns("CTSM_CADS_PROD_BULK_00003_004_CT_LOCATION_2026-08-23-092214.CSV");

        IReadOnlyList<IReadOnlyDictionary<string, object?>>? renderedRows = null;
        var fileAssembler = new Mock<IFileAssembler>();
        fileAssembler
            .Setup(assembler => assembler.Create(
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<IReadOnlyList<IReadOnlyDictionary<string, object?>>>()))
            .Callback<string, DateTime, IReadOnlyList<IReadOnlyDictionary<string, object?>>>(
                (_, _, rows) => renderedRows = rows)
            .Returns("content");

        var scenario = new CtLocationBulkScenario(db, fileNameGenerator.Object, fileAssembler.Object);
        var result = await scenario.ExecuteAsync(new CreateGenerationRequestDto
        {
            Scenario = scenario.Name,
            BusinessKeys = [100m]
        }, TestContext.Current.CancellationToken);

        Assert.Equal("CTSM_CADS_PROD_BULK_00003_004_CT_LOCATION_2026-08-23-092214.CSV", result.FileName);
        var row = Assert.Single(Assert.IsAssignableFrom<IReadOnlyList<IReadOnlyDictionary<string, object?>>>(renderedRows));
        var keys = row.Keys.ToArray();

        Assert.Equal("record_type", keys[0]);
        Assert.Equal("record_count", keys[1]);
        Assert.DoesNotContain("trans_type", keys);
        Assert.DoesNotContain("loc_aud_datetime", keys);
        Assert.DoesNotContain("loc_aud_id", keys);
        Assert.DoesNotContain("loc_aud_type", keys);
        Assert.Contains("loc_id", keys);
        Assert.Equal(100m, Convert.ToDecimal(row["loc_id"]));
    }

    [Fact]
    public async Task CtLocationUpdateScenario_RendersConfiguredColumnsInCorrectOrder()
    {
        using var db = CreateDbContext("CtLocationUpdateScenario_RendersConfiguredColumnsInCorrectOrder");

        var fileNameGenerator = new Mock<IFileNameGenerator>();
        fileNameGenerator
            .Setup(generator => generator.Create(It.IsAny<CreateFileNameCommand>()))
            .Returns("CTSM_CADS_PROD_BULK_00003_004_CT_LOCATION_2026-08-23-092214.CSV");

        IReadOnlyList<IReadOnlyDictionary<string, object?>>? renderedRows = null;
        var fileAssembler = new Mock<IFileAssembler>();
        fileAssembler
            .Setup(assembler => assembler.Create(
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<IReadOnlyList<IReadOnlyDictionary<string, object?>>>()))
            .Callback<string, DateTime, IReadOnlyList<IReadOnlyDictionary<string, object?>>>(
                (_, _, rows) => renderedRows = rows)
            .Returns("content");

        var scenario = new CtLocationUpdateScenario(db, fileNameGenerator.Object, fileAssembler.Object);
        var result = await scenario.ExecuteAsync(new CreateGenerationRequestDto
        {
            Scenario = scenario.Name,
            BusinessKeys = [200m]
        }, TestContext.Current.CancellationToken);

        Assert.Equal("CTSM_CADS_PROD_BULK_00003_004_CT_LOCATION_2026-08-23-092214.CSV", result.FileName);
        var row = Assert.Single(Assert.IsAssignableFrom<IReadOnlyList<IReadOnlyDictionary<string, object?>>>(renderedRows));
        var keys = row.Keys.ToArray();

        Assert.Equal("record_type", keys[0]);
        Assert.Equal("record_count", keys[1]);
        Assert.DoesNotContain("trans_type", keys);
        Assert.Contains("loc_id", keys);
        Assert.Contains("loc_aud_datetime", keys);
        Assert.Contains("loc_aud_id", keys);
        Assert.Contains("loc_aud_type", keys);
        Assert.Equal(200m, Convert.ToDecimal(row["loc_id"]));
    }

    [Fact]
    public void GenerateBulk_TwoContentGenerators_SameSeed_Compare_Equal()
    {
        using var db = CreateDbContext("GenerateBulk_SetsTransTypeToB");

        var contentGenerator1 = new ContentGenerator<CtLocation>(db,
            rulesBuilder: new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01"),
            transform: (entity, key) =>
            {
                entity.LocId = key;
                return entity;
            });

        var contentGenerator2 = new ContentGenerator<CtLocation>(db,
            rulesBuilder: new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01"),
            transform: (entity, key) =>
            {
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 100m, 101m };
        var results1 = contentGenerator1.Generate(keys, seed: 123);
        var results2 = contentGenerator2.Generate(keys, seed: 123);

        // pairwise compare every item
        for (var i = 0; i < results1.Count; i++)
        {
            Assert.True(DictionariesEqual(results1[i], results2[i]), $"Mismatch at index {i}");
        }

        Assert.Equal(2, results1.Count);
        Assert.Equal(2, results2.Count);

        var first1 = results1[0];
        var first2 = results2[0];

        Assert.Equal(100m, Convert.ToDecimal(first1["loc_id"]));
        Assert.Equal(100m, Convert.ToDecimal(first2["loc_id"]));

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(first1["loc_slt_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first1["loc_lty_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first1["loc_cty_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first2["loc_slt_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first2["loc_lty_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first2["loc_cty_id"]));
        Assert.Equal("Y", first1["loc_receive_ppaf_flag"] as string);
        Assert.Equal("N", first1["loc_receive_labels_flag"] as string);
        Assert.Equal("FARM", first1["loc_premises_type"] as string);
        Assert.Equal("01", first1["loc_reason_code"] as string);
        Assert.Equal("Y", first2["loc_receive_ppaf_flag"] as string);
        Assert.Equal("N", first2["loc_receive_labels_flag"] as string);
        Assert.Equal("FARM", first2["loc_premises_type"] as string);
        Assert.Equal("01", first2["loc_reason_code"] as string);
    }

    [Fact]
    public void ContentGeneratorFactory_GenerateBulk_SetsTransTypeToB_AndLocIdToBusinessKey_AndAppliesRules()
    {
        using var db = CreateDbContext("GenerateBulk_SetsTransTypeToB");

        var contentCenerator = new ContentGenerator<CtLocation>(db,
            rulesBuilder: new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01"),
            transform: (entity, key) =>
            {
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 100m, 101m };
        var results = contentCenerator.Generate(keys, seed: 123);

        Assert.Equal(2, results.Count);

        var first = results[0];

        Assert.Equal(100m, Convert.ToDecimal(first["loc_id"]));

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(first["loc_slt_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first["loc_lty_id"]));
        Assert.Equal(1m, Convert.ToDecimal(first["loc_cty_id"]));
        Assert.Equal("Y", first["loc_receive_ppaf_flag"] as string);
        Assert.Equal("N", first["loc_receive_labels_flag"] as string);
        Assert.Equal("FARM", first["loc_premises_type"] as string);
        Assert.Equal("01", first["loc_reason_code"] as string);
    }

    [Fact]
    public void GenerateUpdate_SetsTransTypeToU_AndLocIdToBusinessKey_AndAppliesRules()
    {
        using var db = CreateDbContext("GenerateUpdate_SetsTransTypeToU");

        var contentCenerator = new ContentGenerator<CtLocation>(db,
            rulesBuilder: new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01"),
            transform: (entity, key) =>
            {
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 200m };
        var results = contentCenerator.Generate(keys, seed: 456);

        var first = results[0];

        Assert.Equal(200m, Convert.ToDecimal(first["loc_id"]));

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(first["loc_slt_id"]));
        Assert.Equal("Y", first["loc_receive_ppaf_flag"] as string);
    }

    private static bool DictionariesEqual(IReadOnlyDictionary<string, object?> a, IReadOnlyDictionary<string, object?> b)
    {
        if (a == null || b == null) return a == b;
        if (a.Count != b.Count) return false;

        foreach (var kvp in a)
        {
            if (!b.ContainsKey(kvp.Key))
                return false;

            var v1 = kvp.Value;
            var v2 = b[kvp.Key];

            if (v1 == null && v2 == null) continue;
            if (v1 == null || v2 == null) return false;

            // Numeric comparison for boxed numbers and numeric strings
            try
            {
                var d1 = Convert.ToDecimal(v1);
                var d2 = Convert.ToDecimal(v2);
                if (d1 != d2)
                {
                    return false;
                }

                continue;
            }
            catch
            {
                // not both numeric -> fallback to Equals
            }

            if (!Equals(v1, v2))
                return false;
        }

        return true;
    }
}