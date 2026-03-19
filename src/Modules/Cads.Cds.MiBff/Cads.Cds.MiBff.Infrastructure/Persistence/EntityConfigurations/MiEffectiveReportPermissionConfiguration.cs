using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiEffectiveReportPermissionConfiguration : IEntityTypeConfiguration<MiEffectiveReportPermissionView>
{
    public void Configure(EntityTypeBuilder<MiEffectiveReportPermissionView> builder)
    {
        builder.ToView("mi_effective_report_permission");
        builder.HasKey(x => new { x.Email, x.ReportKey });

        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.ReportKey).HasColumnName("report_key");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Granted).HasColumnName("granted");
    }
}