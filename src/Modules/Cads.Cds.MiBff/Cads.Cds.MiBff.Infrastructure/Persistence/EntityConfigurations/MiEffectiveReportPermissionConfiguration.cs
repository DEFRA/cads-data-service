using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiEffectiveReportPermissionConfiguration : IEntityTypeConfiguration<MiEffectiveReportPermission>
{
    public void Configure(EntityTypeBuilder<MiEffectiveReportPermission> builder)
    {
        builder.ToView("mi_effective_report_permission", SchemaName.Cads.GetDescription());
        builder.HasKey(x => new { x.ExternalSubject, x.ReportKey });

        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.ReportKey).HasColumnName("report_key");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.IsActive).HasColumnName("is_active");
        builder.Property(x => x.DisplayName).HasColumnName("display_name");
        builder.Property(x => x.ExternalSubject).HasColumnName("external_subject");
        builder.Property(x => x.Granted).HasColumnName("granted");
    }
}