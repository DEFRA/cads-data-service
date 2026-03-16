using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;

public class MiReportConfiguration : IEntityTypeConfiguration<MiReport>
{
    public void Configure(EntityTypeBuilder<MiReport> builder)
    {
        builder.ToTable("mi_report");

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

        builder.HasMany(x => x.EffectiveReportPermissions)
            .WithOne(x => x.Report)
            .HasForeignKey(x => x.ReportId)
            .HasPrincipalKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}