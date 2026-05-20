using Cads.Cds.BuildingBlocks.Core.Domain.Livestock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Livestock;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("animal");

        builder.HasKey(x => x.Id)
            .HasName("animal_pkey");
    }
}
