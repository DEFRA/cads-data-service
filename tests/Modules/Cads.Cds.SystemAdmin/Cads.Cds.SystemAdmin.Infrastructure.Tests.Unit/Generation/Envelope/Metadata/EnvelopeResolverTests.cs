using Cads.Cds.SystemAdmin.Application.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Imports.Utilities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation.Envelope.Metadata;

public class EnvelopeResolverTests
{
    private static readonly EntityTypeStore EntityTypeStore = new();
    private readonly TablePrefixResolver _prefixResolver = new(EntityTypeStore);
    private readonly PipelineColumns _pipelineColumns = new();
    private readonly BusinessColumnResolver _columnResolver;

    public EnvelopeResolverTests()
    {
        _columnResolver = new BusinessColumnResolver(_prefixResolver, _pipelineColumns, new ImportDataTypeLookup(), EntityTypeStore);
    }

    private sealed class Widget
    {
        public long TransId { get; set; }
        public string TransType { get; set; } = null!;
        public string? WdgFlag { get; set; }
        public decimal WdgId { get; set; }
        public string? WdgName { get; set; }
        public decimal FakeData { get; set; }
        public decimal? RowNumber { get; set; }
        public long? WdgAudId { get; set; }
        public string? WdgAudType { get; set; }
        public DateTime? WdgAudDatetime { get; set; }
        public string? RecordType { get; set; }
        public decimal? RecordCount { get; set; }
        public DateTime? ImportedDate { get; set; }
        public long? CtsFileImportId { get; set; }
    }

    private sealed class NoAud
    {
        public long TransId { get; set; }
        public decimal NoaId { get; set; }
    }

    private sealed class TwoAud
    {
        public long TransId { get; set; }
        public long? AaaAudId { get; set; }
        public long? BbbAudId { get; set; }
    }

    private sealed class WidgetContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Widget>(e =>
            {
                e.ToTable("ct_widgets", "cts_transactions");
                e.HasKey(x => x.TransId);
                Snake(e.Metadata);
            });
            modelBuilder.Entity<NoAud>(e =>
            {
                e.ToTable("ct_no_aud", "cts_transactions");
                e.HasKey(x => x.TransId);
                Snake(e.Metadata);
            });
            modelBuilder.Entity<TwoAud>(e =>
            {
                e.ToTable("ct_two_aud", "cts_transactions");
                e.HasKey(x => x.TransId);
                Snake(e.Metadata);
            });
        }

        private static void Snake(IMutableEntityType entityType)
        {
            foreach (var p in entityType.GetProperties())
            {
                var name = string.Concat(p.Name.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + char.ToLowerInvariant(c) : char.ToLowerInvariant(c).ToString()));
                p.SetColumnName(name);
            }
        }
    }

    private static IModel CreateModel()
    {
        var options = new DbContextOptionsBuilder<WidgetContext>()
            .UseInMemoryDatabase(nameof(EnvelopeResolverTests))
            .Options;

        using var context = new WidgetContext(options);
        return context.Model;
    }

    private static IReadOnlyEntityType Entity<T>() => CreateModel().FindEntityType(typeof(T))!;

    [Fact]
    public void Prefix_IsThePartBeforeTheSingleAudIdColumn_UpperCased()
    {
        Assert.Equal("WDG", _prefixResolver.Resolve(Entity<Widget>()));
    }

    [Fact]
    public void Prefix_Throws_WhenThereIsNoAudIdColumn()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _prefixResolver.Resolve(Entity<NoAud>()));
        Assert.Contains("_aud_id", ex.Message);
    }

    [Fact]
    public void Prefix_Throws_WhenThereIsMoreThanOneAudIdColumn()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _prefixResolver.Resolve(Entity<TwoAud>()));
        Assert.Contains("ambiguous", ex.Message);
    }

    [Fact]
    public void BusinessColumns_PutTheKeyFirst_ThenDeclarationOrder_AndExcludePipelineColumns()
    {
        var columns = _columnResolver.Resolve(Entity<Widget>(), "wdg_id");

        Assert.Equal(["wdg_id", "wdg_flag", "wdg_name"], columns.Select(c => c.Name));
    }

    [Fact]
    public void BusinessColumns_Throw_WhenTheKeyIsNotABusinessColumn()
    {
        Assert.Throws<InvalidOperationException>(() => _columnResolver.Resolve(Entity<Widget>(), "trans_id"));
        Assert.Throws<InvalidOperationException>(() => _columnResolver.Resolve(Entity<Widget>(), "missing"));
    }

    [Theory]
    [InlineData("trans_id")]
    [InlineData("trans_type")]
    [InlineData("row_number")]
    [InlineData("record_type")]
    [InlineData("record_count")]
    [InlineData("imported_date")]
    [InlineData("cts_file_import_id")]
    [InlineData("fake_data")]
    [InlineData("wdg_aud_id")]
    [InlineData("WDG_AUD_TYPE")]
    [InlineData("wdg_aud_datetime")]
    public void PipelineColumns_ExcludeEveryFixedAndAudColumn(string column)
    {
        Assert.True(_pipelineColumns.IsExcluded(column, "WDG"));
    }

    [Theory]
    [InlineData("wdg_id")]
    [InlineData("loc_aud_id")]
    public void PipelineColumns_KeepBusinessColumnsAndOtherTablesAudColumns(string column)
    {
        Assert.False(_pipelineColumns.IsExcluded(column, "WDG"));
    }

    [Fact]
    public void AudColumns_AreInEnvelopeOrder()
    {
        Assert.Equal(["loc_aud_id", "loc_aud_type", "loc_aud_datetime"], _pipelineColumns.AudColumns("LOC"));
    }
}