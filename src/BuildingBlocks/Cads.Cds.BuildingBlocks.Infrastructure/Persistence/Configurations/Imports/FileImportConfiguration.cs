using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Imports;

public class FileImportConfiguration : IEntityTypeConfiguration<FileImport>
{
    public void Configure(EntityTypeBuilder<FileImport> builder)
    {
        builder.ToTable(
            "cts_file_imports",
            SchemaName.Cads.GetDescription(),
            table =>
            {
                table.HasCheckConstraint(
                    "cts_file_imports_rows_found_check",
                    "rows_found >= 0");

                table.HasCheckConstraint(
                    "cts_file_imports_total_rows_to_process_check",
                    "total_rows_to_process >= 0");
            });

        // PK
        builder.HasKey(x => x.Id)
            .HasName("cts_file_imports_pkey");

        builder.Property(x => x.Id)
            .HasColumnName("cts_file_import_id")
            .ValueGeneratedOnAdd();

        // Core fields
        builder.Property(x => x.DestinationTableName)
            .HasColumnName("destination_table_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.FileName)
            .HasColumnName("file_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.TotalRowsToProcess)
            .HasColumnName("total_rows_to_process")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.RowsFound)
            .HasColumnName("rows_found")
            .HasColumnType("bigint")
            .HasDefaultValue(0)
            .IsRequired();

        // Status fields
        builder.Property(x => x.ImportStatus)
            .HasColumnName("import_status_id")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.ProcessingStatus)
            .HasColumnName("processing_status_id")
            .HasColumnType("smallint")
            .IsRequired();

        // Timestamps
        builder.Property(x => x.AddedAt)
            .HasColumnName("added_at")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("clock_timestamp()")
            .IsRequired();

        builder.Property(x => x.ImportStartAt)
            .HasColumnName("import_start_at")
            .HasColumnType("timestamptz");

        builder.Property(x => x.ImportEndAt)
            .HasColumnName("import_end_at")
            .HasColumnType("timestamptz");

        builder.Property(x => x.ProcessingStartAt)
            .HasColumnName("processing_start_at")
            .HasColumnType("timestamptz");

        builder.Property(x => x.ProcessingEndAt)
            .HasColumnName("processing_end_at")
            .HasColumnType("timestamptz");
    }
}