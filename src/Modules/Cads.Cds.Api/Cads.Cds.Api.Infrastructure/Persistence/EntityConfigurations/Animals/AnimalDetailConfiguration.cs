using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.Api.Infrastructure.Persistence.EntityConfigurations.Animals;

public class AnimalDetailConfiguration : IEntityTypeConfiguration<AnimalDetail>
{
    public void Configure(EntityTypeBuilder<AnimalDetail> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.ResourceType).HasColumnName("resource_type");
        builder.Property(x => x.Identifier).HasColumnName("identifier");
        builder.Property(x => x.EventDatetime).HasColumnName("event_datetime");
        builder.Property(x => x.SourceSystem).HasColumnName("source_system");
        builder.Property(x => x.SourceSchema).HasColumnName("source_schema");
        builder.Property(x => x.SourceSchemaVersion).HasColumnName("source_schema_version");
        builder.Property(x => x.AnimalResourceType).HasColumnName("animal_resource_type");
        builder.Property(x => x.IdentifierSchema).HasColumnName("identifier_schema");
        builder.Property(x => x.Species).HasColumnName("species");
        builder.Property(x => x.Sex).HasColumnName("sex");
        builder.Property(x => x.BirthDate).HasColumnName("birth_date");
        builder.Property(x => x.RegistrationDate).HasColumnName("registration_date");
        builder.Property(x => x.DateOnCph).HasColumnName("date_on_cph");
        builder.Property(x => x.BreedSchema).HasColumnName("breed_schema");
        builder.Property(x => x.BreedCode).HasColumnName("breed_code");
        builder.Property(x => x.BreedDisplayNameLong).HasColumnName("breed_display_name_long");
        builder.Property(x => x.BreedDisplayName).HasColumnName("breed_display_name");
        builder.Property(x => x.BreedName).HasColumnName("breed_name");
        builder.Property(x => x.State).HasColumnName("state");
        builder.Property(x => x.RestrictionStatus).HasColumnName("restriction_status");
        builder.Property(x => x.GeneticDamIdentifier).HasColumnName("genetic_dam_identifier");
        builder.Property(x => x.GeneticDamSchema).HasColumnName("genetic_dam_schema");
        builder.Property(x => x.SireIdentifier).HasColumnName("sire_identifier");
        builder.Property(x => x.SireSchema).HasColumnName("sire_schema");
    }
}