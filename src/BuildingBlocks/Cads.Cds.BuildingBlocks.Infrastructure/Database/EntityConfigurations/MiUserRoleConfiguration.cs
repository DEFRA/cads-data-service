using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;

public class MiUserRoleConfiguration : IEntityTypeConfiguration<MiUserRole>
{
    public void Configure(EntityTypeBuilder<MiUserRole> builder)
    {
        builder.ToTable("mi_user_role");

        builder.HasKey(x => new { x.UserId, x.RoleId })
            .HasName("mi_user_role_pkey");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.GrantedAt)
            .HasColumnName("granted_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("mi_user_role_user_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("mi_user_role_role_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("mi_user_role_user_idx");

        builder.HasIndex(x => x.RoleId)
            .HasDatabaseName("mi_user_role_role_idx");
    }
}