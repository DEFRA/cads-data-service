using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiDeathSummaryConfiguration : IEntityTypeConfiguration<MiDeathSummary>
{
    public void Configure(EntityTypeBuilder<MiDeathSummary> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.DeathYear).HasColumnName("death_year");
        builder.Property(x => x.DeathMonth).HasColumnName("death_month");
        builder.Property(x => x.Country).HasColumnName("country");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.BreedType).HasColumnName("breed_type");
        builder.Property(x => x.Breed).HasColumnName("breed");
        builder.Property(x => x.BreedCode).HasColumnName("breed_code");
        builder.Property(x => x.Sex).HasColumnName("sex");
        builder.Property(x => x.AgeAtDeathInMonths).HasColumnName("age_at_death_months");
        builder.Property(x => x.NumberOfDeaths).HasColumnName("number_of_deaths");
    }
}