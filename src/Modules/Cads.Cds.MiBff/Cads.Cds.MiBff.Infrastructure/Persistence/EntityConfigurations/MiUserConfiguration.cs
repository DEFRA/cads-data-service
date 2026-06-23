using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiUserConfiguration : IEntityTypeConfiguration<MiUser>
{
    public void Configure(EntityTypeBuilder<MiUser> builder)
    {
        builder.ToTable("mi_user", SchemaName.Cads.GetDescription());

        builder.HasKey(x => x.UserId)
            .HasName("mi_user_pkey");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ExternalSubject)
            .HasColumnName("external_subject")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasColumnName("display_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasColumnType("text");

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasIndex(x => x.ExternalSubject)
            .IsUnique()
            .HasDatabaseName("mi_user_external_subject_key");
    }
}