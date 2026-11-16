using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation.EnvelopeTestHelpers;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation;

/// <summary>Prefix and business-column resolution against the real cts_transactions model.</summary>
public class CtsTransactionsEnvelopeTableTests
{
    private readonly TablePrefixResolver _prefixResolver = new();
    private readonly IEnvelopeTableResolver _tableResolver = CreateEnvelopeTableResolver();
    private readonly PipelineColumns _pipelineColumns = new();
    private readonly ImportDataTypeLookup _importDataTypeLookup = new();

    [Theory]
    [InlineData(typeof(CtLocation), "LOC")]
    [InlineData(typeof(CtAnimalClaim), "ANC")]
    [InlineData(typeof(CtAddress), "ADR")]
    [InlineData(typeof(CtAnimalChange), "ACH")]
    public void Prefix_IsDerivedFromTheAudIdColumn(Type entity, string expected)
    {
        using var db = CreateDbContext(nameof(Prefix_IsDerivedFromTheAudIdColumn));

        Assert.Equal(expected, _prefixResolver.Resolve(db.Model.FindEntityType(entity)!));
    }

    [Fact]
    public void CtLocations_BusinessColumns_PutLocIdFirst_ThenDdlOrder()
    {
        using var db = CreateDbContext(nameof(CtLocations_BusinessColumns_PutLocIdFirst_ThenDdlOrder));

        var table = _tableResolver.Resolve(db.Model, typeof(CtLocation));

        Assert.Equal("ct_locations", table.TableName);
        Assert.Equal("LOC", table.Prefix);
        Assert.Equal("loc_id", table.BusinessKey);
        Assert.Equal(
            [
                "loc_id", "loc_receive_ppaf_flag", "loc_slt_id", "loc_lty_id", "loc_cty_id", "loc_receive_labels_flag",
                "loc_effective_from", "loc_effective_to", "loc_cessation_reason", "loc_premises_type", "loc_comments",
                "loc_map_reference", "loc_source_identifier", "loc_source_reference", "loc_tel_number", "loc_mobile_number",
                "loc_fax_number", "loc_email_address", "loc_current_status", "loc_current_user", "loc_current_modified_date",
                "loc_current_pid", "loc_reason_code", "loc_version"
            ],
            table.BusinessColumns);
    }

    /// <summary>
    /// Every mapped cts_transactions table whose TableInfo Cts key is a real column resolves: one aud id column,
    /// the key first, and no pipeline column among its business columns. Catches model drift for tables not yet
    /// used by a scenario. Some tables map a placeholder key (<c>id</c>) that isn't a column; they're skipped.
    /// </summary>
    [Fact]
    public void EveryMappedCtsTransactionsTableWithARealKey_Resolves()
    {
        using var db = CreateDbContext(nameof(EveryMappedCtsTransactionsTableWithARealKey_Resolves));

        var entityTypes = db.Model.GetEntityTypes()
            .Where(e => e.GetSchema() == "cts_transactions")
            .Where(e => _importDataTypeLookup.FindByTableName(e.GetTableName()!, SchemaName.CtsTransactions) != ImportDataType.None)
            .Where(e =>
            {
                var store = StoreObjectIdentifier.Table(e.GetTableName()!, e.GetSchema());
                var key = _importDataTypeLookup.GetBusinessKey(e.GetTableName()!);
                return e.GetProperties().Any(p => p.GetColumnName(store) == key);
            })
            .ToList();

        Assert.True(entityTypes.Count > 90, $"Only {entityTypes.Count} tables were checked.");

        Assert.All(entityTypes, entityType =>
        {
            var table = _tableResolver.Resolve(entityType);

            Assert.Equal(table.BusinessColumns.Count, table.BusinessColumns.Distinct().Count());
            Assert.DoesNotContain(table.BusinessColumns, c => _pipelineColumns.IsExcluded(c, table.Prefix));
        });
    }
}
