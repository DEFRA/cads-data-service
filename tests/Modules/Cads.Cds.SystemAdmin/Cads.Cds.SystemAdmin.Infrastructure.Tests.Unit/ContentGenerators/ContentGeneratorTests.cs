using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Entities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.ContentGenerators;

public class ContentGeneratorTests
{
    private static CtsTransactionsSystemAdminDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<CtsTransactionsSystemAdminDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new CtsTransactionsSystemAdminDbContext(options);
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
                entity.TransType = "B";
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
                entity.TransType = "B";
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
        Assert.Equal("B", first1["trans_type"] as string);
        Assert.Equal(100m, Convert.ToDecimal(first2["loc_id"]));
        Assert.Equal("B", first2["trans_type"] as string);

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
                entity.TransType = "B";
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 100m, 101m };
        var results = contentCenerator.Generate(keys, seed: 123);

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
                entity.TransType = "U";
                entity.LocId = key;
                return entity;
            });

        var keys = new List<decimal> { 200m };
        var results = contentCenerator.Generate(keys, seed: 456);

        var first = results[0];

        Assert.Equal(200m, Convert.ToDecimal(first["loc_id"]));
        Assert.Equal("U", first["trans_type"] as string);

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