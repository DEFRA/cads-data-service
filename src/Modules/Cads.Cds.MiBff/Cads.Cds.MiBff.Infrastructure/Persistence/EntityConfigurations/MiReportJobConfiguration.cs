using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiReportJobConfiguration : IEntityTypeConfiguration<MiReportJob>
{
    public void Configure(EntityTypeBuilder<MiReportJob> builder)
    {
        builder.ToTable("mi_report_job", SchemaName.Cads.GetDescription());

        builder.HasKey(x => x.ReportJobId).HasName("mi_report_job_pkey");

        builder.Property(x => x.ReportJobId)
            .HasColumnName("report_job_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ReportKey)
            .HasColumnName("report_key")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.ExternalSubject)
            .HasColumnName("external_subject")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.RequestPayload)
            .HasColumnName("request_payload")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasColumnType("text")
            .IsRequired()
            .HasDefaultValue(ReportJobStatus.Pending);

        builder.Property(x => x.S3Key)
            .HasColumnName("s3_key")
            .HasColumnType("text");

        builder.Property(x => x.RowCount)
            .HasColumnName("row_count")
            .HasColumnType("integer");

        builder.Property(x => x.Error)
            .HasColumnName("error")
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.StartedAt)
            .HasColumnName("started_at")
            .HasColumnType("timestamptz");

        builder.Property(x => x.CompletedAt)
            .HasColumnName("completed_at")
            .HasColumnType("timestamptz");

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("timestamptz");

        builder.HasIndex(x => x.Status)
            .HasDatabaseName("mi_report_job_status_idx");

        builder.HasIndex(x => x.ExpiresAt)
            .HasDatabaseName("mi_report_job_expires_at_idx")
            .HasFilter("expires_at IS NOT NULL");
    }
}