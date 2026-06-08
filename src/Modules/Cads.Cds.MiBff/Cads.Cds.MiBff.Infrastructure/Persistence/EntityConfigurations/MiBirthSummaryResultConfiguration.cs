using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiBirthSummaryResultConfiguration : IEntityTypeConfiguration<MiBirthSummary>
{
    public void Configure(EntityTypeBuilder<MiBirthSummary> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.BirthYear).HasColumnName("birth_year");
        builder.Property(x => x.BirthMonth).HasColumnName("birth_month");
        builder.Property(x => x.Country).HasColumnName("country");
        builder.Property(x => x.GovRegion).HasColumnName("gov_region");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.BreedType).HasColumnName("breed_type");
        builder.Property(x => x.Breed).HasColumnName("breed");
        builder.Property(x => x.Sex).HasColumnName("sex");
        builder.Property(x => x.ApplicationType).HasColumnName("application_type");
        builder.Property(x => x.NumberOfBirths).HasColumnName("number_of_births");
    }
}