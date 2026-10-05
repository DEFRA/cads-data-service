using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.Api.Infrastructure.Persistence.EntityConfigurations.Animals;

public class AnimalOnHoldingConfiguration : IEntityTypeConfiguration<AnimalOnHolding>
{
    public void Configure(EntityTypeBuilder<AnimalOnHolding> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.CphNumber).HasColumnName("cph_number");
        builder.Property(x => x.AnimalId).HasColumnName("animal_id");
        builder.Property(x => x.EarTagNumber).HasColumnName("ear_tag_number");
        builder.Property(x => x.EarTagUrlIdentifier).HasColumnName("ear_tag_url_identifier");
        builder.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
        builder.Property(x => x.DateRegistered).HasColumnName("date_registered");
        builder.Property(x => x.Sex).HasColumnName("sex");
        builder.Property(x => x.BreedCode).HasColumnName("breed_code");
        builder.Property(x => x.Breed).HasColumnName("breed");
        builder.Property(x => x.AnimalStatus).HasColumnName("animal_status");
        builder.Property(x => x.ResourceType).HasColumnName("resource_type");
        builder.Property(x => x.CphSchema).HasColumnName("cph_schema");
        builder.Property(x => x.LocationName).HasColumnName("location_name");
        builder.Property(x => x.IdentifierSchema).HasColumnName("identifier_schema");
        builder.Property(x => x.DateOnCph).HasColumnName("date_on_cph");
        builder.Property(x => x.DateOffCph).HasColumnName("date_off_cph");
        builder.Property(x => x.Species).HasColumnName("species");
        builder.Property(x => x.BreedSchema).HasColumnName("breed_schema");
        builder.Property(x => x.BreedName).HasColumnName("breed_name");
        builder.Property(x => x.BreedIdentifier).HasColumnName("breed_identifier");
        builder.Property(x => x.TotalCount).HasColumnName("total_count");
    }
}