using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiPermissionConfiguration : IEntityTypeConfiguration<MiPermission>
{
    public void Configure(EntityTypeBuilder<MiPermission> builder)
    {
        builder.ToTable("mi_permission", SchemaName.Cads.GetDescription());

        builder.HasKey(x => x.PermissionId).HasName("mi_permission_pkey");

        builder.Property(x => x.PermissionId)
            .HasColumnName("permission_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.PermissionKey)
            .HasColumnName("permission_key")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.HasIndex(x => x.PermissionKey)
            .IsUnique()
            .HasDatabaseName("mi_permission_permission_key_key");
    }
}