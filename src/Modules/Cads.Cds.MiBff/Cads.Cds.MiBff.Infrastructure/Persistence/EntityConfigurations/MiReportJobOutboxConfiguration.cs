using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiReportJobOutboxConfiguration : IEntityTypeConfiguration<MiReportJobOutbox>
{
    public void Configure(EntityTypeBuilder<MiReportJobOutbox> builder)
    {
        builder.ToTable("mi_report_job_outbox", SchemaName.Cads.GetDescription());

        builder.HasKey(x => x.OutboxId).HasName("mi_report_job_outbox_pkey");

        builder.Property(x => x.OutboxId)
            .HasColumnName("outbox_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ReportJobId)
            .HasColumnName("report_job_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasColumnName("subject")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Payload)
            .HasColumnName("payload")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasColumnType("text")
            .IsRequired()
            .HasDefaultValue(ReportJobOutboxStatus.Pending);

        builder.Property(x => x.Attempts)
            .HasColumnName("attempts")
            .HasColumnType("integer")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LastError)
            .HasColumnName("last_error")
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.DispatchedAt)
            .HasColumnName("dispatched_at")
            .HasColumnType("timestamptz");

        builder.HasOne(x => x.ReportJob)
            .WithMany(j => j.OutboxMessages)
            .HasForeignKey(x => x.ReportJobId)
            .HasConstraintName("mi_report_job_outbox_report_job_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName("mi_report_job_outbox_pending_idx")
            .HasFilter("status = 'pending'");
    }
}