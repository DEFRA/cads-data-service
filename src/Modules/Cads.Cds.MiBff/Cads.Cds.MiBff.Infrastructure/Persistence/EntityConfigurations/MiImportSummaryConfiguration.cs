using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiImportSummaryConfiguration : IEntityTypeConfiguration<MiImportSummary>
{
    public void Configure(EntityTypeBuilder<MiImportSummary> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.Country).HasColumnName("country");
        builder.Property(x => x.MonthYear).HasColumnName("month_year");
        builder.Property(x => x.AgeAtImport).HasColumnName("age_at_import_months");
        builder.Property(x => x.AgeBand).HasColumnName("age_band");
        builder.Property(x => x.BreedType).HasColumnName("breed_type");
        builder.Property(x => x.Sex).HasColumnName("sex");
        builder.Property(x => x.NumberOfImports).HasColumnName("number_of_imports");
    }
}