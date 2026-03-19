using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiReportGroupConfiguration : IEntityTypeConfiguration<MiReportGroup>
{
    public void Configure(EntityTypeBuilder<MiReportGroup> builder)
    {
        builder.ToTable("mi_report_group");

        builder.HasKey(x => x.GroupId).HasName("mi_report_group_pkey");

        builder.Property(x => x.GroupId)
            .HasColumnName("group_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.GroupKey)
            .HasColumnName("group_key")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("text")
            .IsRequired();

        builder.HasIndex(x => x.GroupKey)
            .IsUnique()
            .HasDatabaseName("mi_report_group_group_key_key");
    }
}