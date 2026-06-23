using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiEffectiveReportAllPermissionConfiguration : IEntityTypeConfiguration<MiEffectiveReportAllPermission>
{
    public void Configure(EntityTypeBuilder<MiEffectiveReportAllPermission> builder)
    {
        builder.ToView("mi_effective_report_all_permission", SchemaName.Cads.GetDescription());

        builder.HasKey(x => new { x.ExternalSubject, x.ReportKey, x.PermissionKey });

        builder.Property(x => x.ReportKey).HasColumnName("report_key");
        builder.Property(x => x.ExternalSubject).HasColumnName("external_subject");
        builder.Property(x => x.PermissionKey).HasColumnName("permission_key");
    }
}