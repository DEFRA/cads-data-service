using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiUserReportPermissionConfiguration : IEntityTypeConfiguration<MiUserReportPermission>
{
    public void Configure(EntityTypeBuilder<MiUserReportPermission> builder)
    {
        builder.ToTable("mi_user_report_permission");

        builder.HasKey(x => new { x.UserId, x.ReportId, x.PermissionId })
            .HasName("mi_user_report_permission_pkey");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ReportId)
            .HasColumnName("report_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.PermissionId)
            .HasColumnName("permission_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Granted)
            .HasColumnName("granted")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasColumnName("reason")
            .HasColumnType("text");

        builder.Property(x => x.GrantedAt)
            .HasColumnName("granted_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserReportPermissions)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("mi_user_report_permission_user_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Report)
            .WithMany(r => r.UserReportPermissions)
            .HasForeignKey(x => x.ReportId)
            .HasConstraintName("mi_user_report_permission_report_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permission)
            .WithMany(p => p.UserReportPermissions)
            .HasForeignKey(x => x.PermissionId)
            .HasConstraintName("mi_user_report_permission_permission_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.UserId, x.ReportId })
            .HasDatabaseName("mi_urp_user_report_idx");

        builder.HasIndex(x => x.ReportId)
            .HasDatabaseName("mi_urp_report_idx");

        builder.HasIndex(x => x.PermissionId)
            .HasDatabaseName("mi_urp_permission_idx");

        builder.HasIndex(x => new { x.UserId, x.ReportId, x.PermissionId })
            .HasDatabaseName("mi_urp_user_report_permission_idx");
    }
}