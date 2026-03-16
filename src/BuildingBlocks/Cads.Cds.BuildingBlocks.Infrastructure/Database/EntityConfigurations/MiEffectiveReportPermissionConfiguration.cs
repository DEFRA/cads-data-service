using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;

public class MiEffectiveReportPermissionConfiguration : IEntityTypeConfiguration<MiEffectiveReportPermission>
{
    public void Configure(EntityTypeBuilder<MiEffectiveReportPermission> builder)
    {
        builder.ToView("mi_effective_report_permission");
        builder.HasKey(x => new { x.UserId, x.ReportId, x.PermissionId });

        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.PermissionId).HasColumnName("permission_id");
        builder.Property(x => x.Granted).HasColumnName("granted");

        // Navigation relationships (no database constraints; for query composition)
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Report)
            .WithMany()
            .HasForeignKey(x => x.ReportId)
            .HasPrincipalKey(r => r.ReportId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Permission)
            .WithMany()
            .HasForeignKey(x => x.PermissionId)
            .HasPrincipalKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}