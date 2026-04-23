using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.EntityConfigurations;

public class MiBirthSummaryResultConfiguration : IEntityTypeConfiguration<MiBirthSummaryResult>
{
    public void Configure(EntityTypeBuilder<MiBirthSummaryResult> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.BirthYear).HasColumnName("Birth Year");
        builder.Property(x => x.BirthMonth).HasColumnName("Birth Month");
        builder.Property(x => x.Country).HasColumnName("Country");
        builder.Property(x => x.GovRegion).HasColumnName("Gov Region");
        builder.Property(x => x.County).HasColumnName("County");
        builder.Property(x => x.BreedType).HasColumnName("Breed Type");
        builder.Property(x => x.Breed).HasColumnName("Breed");
        builder.Property(x => x.Sex).HasColumnName("Sex");
        builder.Property(x => x.ApplicationType).HasColumnName("Application Type");
        builder.Property(x => x.NumberOfBirths).HasColumnName("Number Of Births");
    }
}