using Cads.Cds.Ingester.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.EntityConfigurations;

public class DataSeedIngestionHistoryConfiguration : IEntityTypeConfiguration<DataSeedIngestionHistory>
{
    public void Configure(EntityTypeBuilder<DataSeedIngestionHistory> builder)
    {
        builder.ToTable("data_seed_ingestion_history");

        builder.HasKey(x => x.Id).HasName("data_seed_ingestion_history_pkey");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.FileName)
            .HasColumnName("file_name")
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.AppliedAt)
            .HasColumnName("applied_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.Property(x => x.Checksum)
            .HasColumnName("checksum")
            .HasColumnType("varchar(64)")
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => x.FileName)
            .IsUnique()
            .HasDatabaseName("data_seed_ingestion_history_file_name_key");
    }
}