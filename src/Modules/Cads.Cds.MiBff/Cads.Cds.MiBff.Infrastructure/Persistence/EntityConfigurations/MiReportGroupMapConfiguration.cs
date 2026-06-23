using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiReportGroupMapConfiguration : IEntityTypeConfiguration<MiReportGroupMap>
{
    public void Configure(EntityTypeBuilder<MiReportGroupMap> builder)
    {
        builder.ToTable("mi_report_group_map", SchemaName.Cads.GetDescription());

        builder.HasKey(x => new { x.GroupId, x.ReportId })
            .HasName("mi_report_group_map_pkey");

        builder.Property(x => x.GroupId)
            .HasColumnName("group_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ReportId)
            .HasColumnName("report_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasOne(x => x.Group)
            .WithMany(g => g.ReportGroupMaps)
            .HasForeignKey(x => x.GroupId)
            .HasConstraintName("mi_report_group_map_group_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Report)
            .WithMany(r => r.ReportGroupMaps)
            .HasForeignKey(x => x.ReportId)
            .HasConstraintName("mi_report_group_map_report_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);
    }
}