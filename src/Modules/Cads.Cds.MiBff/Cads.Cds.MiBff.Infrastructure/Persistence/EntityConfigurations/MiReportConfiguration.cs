using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiReportConfiguration : IEntityTypeConfiguration<MiReport>
{
    public void Configure(EntityTypeBuilder<MiReport> builder)
    {
        builder.ToTable("mi_report", SchemaName.Cads.GetDescription());

        builder.HasKey(x => x.ReportId).HasName("mi_report_pkey");

        builder.Property(x => x.ReportId)
            .HasColumnName("report_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ReportKey)
            .HasColumnName("report_key")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasIndex(x => x.ReportKey)
            .IsUnique()
            .HasDatabaseName("mi_report_report_key_key");
    }
}