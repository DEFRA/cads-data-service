using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;

public class MiRoleConfiguration : IEntityTypeConfiguration<MiRole>
{
    public void Configure(EntityTypeBuilder<MiRole> builder)
    {
        builder.ToTable("mi_role");

        builder.HasKey(x => x.RoleId).HasName("mi_role_pkey");

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.RoleKey)
            .HasColumnName("role_key")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasIndex(x => x.RoleKey)
            .IsUnique()
            .HasDatabaseName("mi_role_role_key_key");
    }
}