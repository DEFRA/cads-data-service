using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Schemas.CtsTransactions;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.ContentGenerattors;

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
    public void ContentGeneratorFactory_CreateContentGenerator_ReturnsCorrectGenerator()
    {
        using var db = CreateDbContext("CreateContentGenerator_ReturnsCorrectGenerator");
        var factory = new ContentGeneratorFactory();

        var result = factory.Create("CtLocation", db);

        Assert.IsType<CtLocationContentGenerator>(result);
    }

    [Fact]
    public void ContentGeneratorFactory_GenerateBulk_SetsTransTypeToB_AndLocIdToBusinessKey_AndAppliesRules()
    {
        using var db = CreateDbContext("GenerateBulk_SetsTransTypeToB");
        var factory = new ContentGeneratorFactory();

        var contentCenerator = factory.Create("CtLocation", db);

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

        var contentCenerator = factory.Create("CtLocation", db);

        var result = contentCenerator.GenerateUpdate(200m, seed: 456);

        Assert.Equal(200m, Convert.ToDecimal(result["loc_id"]));
        Assert.Equal("U", result["trans_type"] as string);

        // RulesBuilder overrides asserted
        Assert.Equal(1m, Convert.ToDecimal(result["loc_slt_id"]));
        Assert.Equal("Y", result["loc_receive_ppaf_flag"] as string);
    }
}