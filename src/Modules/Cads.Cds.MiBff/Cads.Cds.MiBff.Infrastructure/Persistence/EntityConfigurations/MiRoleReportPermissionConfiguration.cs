using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiRoleReportPermissionConfiguration : IEntityTypeConfiguration<MiRoleReportPermission>
{
    public void Configure(EntityTypeBuilder<MiRoleReportPermission> builder)
    {
        builder.ToTable("mi_role_report_permission", SchemaName.Cads.GetDescription());

        builder.HasKey(x => new { x.RoleId, x.ReportId, x.PermissionId })
            .HasName("mi_role_report_permission_pkey");

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
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
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.RoleReportPermissions)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("mi_role_report_permission_role_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Report)
            .WithMany(r => r.RoleReportPermissions)
            .HasForeignKey(x => x.ReportId)
            .HasConstraintName("mi_role_report_permission_report_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permission)
            .WithMany(p => p.RoleReportPermissions)
            .HasForeignKey(x => x.PermissionId)
            .HasConstraintName("mi_role_report_permission_permission_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.RoleId, x.ReportId })
            .HasDatabaseName("mi_rrp_role_report_idx");

        builder.HasIndex(x => x.PermissionId)
            .HasDatabaseName("mi_rrp_permission_idx");

        builder.HasIndex(x => x.ReportId)
            .HasDatabaseName("mi_rrp_report_idx");
    }
}