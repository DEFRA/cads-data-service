using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;

public class MiPermissionConfiguration : IEntityTypeConfiguration<MiPermission>
{
    public void Configure(EntityTypeBuilder<MiPermission> builder)
    {
        builder.ToTable("mi_permission");

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

        builder.HasMany(x => x.EffectiveReportPermissions)
            .WithOne(x => x.Permission)
            .HasForeignKey(x => x.PermissionId)
            .HasPrincipalKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}