using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Schemas.CtsTransactions;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.ContentGenerators;

public class CtLocationContentGeneratorTests
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
        var contentGenerator = new CtLocationContentGenerator(db);

        var keys = new List<decimal> { 100m, 101m };
        var results = contentGenerator.GenerateBulk(keys, seed: 123);

        Assert.Equal(2, results.Count);

        var first = results.First();

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
        var contentGenerator1 = new CtLocationContentGenerator(db);
        var contentGenerator2 = new CtLocationContentGenerator(db);

        var keys = new List<decimal> { 100m, 101m };
        var results1 = contentGenerator1.GenerateBulk(keys, seed: 123);
        var results2 = contentGenerator2.GenerateBulk(keys, seed: 123);

        // pairwise compare every item
        for (var i = 0; i < results1.Count; i++)
        {
            Assert.True(DictionariesEqual(results1[i], results2[i]), $"Mismatch at index {i}");
        }

        Assert.Equal(2, results1.Count);
        Assert.Equal(2, results2.Count);

        var first1 = results1.First();
        var first2 = results2.First();

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
    public void ContentGeneratorFactory_CreateContentGenerator_ReturnsCorrectGenerator()
    {
        using var db = CreateDbContext("CreateContentGenerator_ReturnsCorrectGenerator");
        var factory = new ContentGeneratorFactory();

        var result = factory.Create("ct_Locations", db);

        Assert.IsType<CtLocationContentGenerator>(result);
    }

    [Fact]
    public void ContentGeneratorFactory_GenerateBulk_SetsTransTypeToB_AndLocIdToBusinessKey_AndAppliesRules()
    {
        using var db = CreateDbContext("GenerateBulk_SetsTransTypeToB");
        var factory = new ContentGeneratorFactory();

        var contentCenerator = factory.Create("ct_Locations", db);

        var keys = new List<decimal> { 100m, 101m };
        var results = contentCenerator.GenerateBulk(keys, seed: 123);

        Assert.Equal(2, results.Count);

        var first = results.First();

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
        var factory = new ContentGeneratorFactory();

        var contentCenerator = factory.Create("ct_Locations", db);

        var result = contentCenerator.GenerateUpdate(200m, seed: 456);

        Assert.Equal(200m, Convert.ToDecimal(result["loc_id"]));
        Assert.Equal("U", result["trans_type"] as string);

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(result["loc_slt_id"]));
        Assert.Equal("Y", result["loc_receive_ppaf_flag"] as string);
    }

    private static bool DictionariesEqual(IReadOnlyDictionary<string, object> a, IReadOnlyDictionary<string, object> b)
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