using Cads.Cds.BuildingBlocks.Core.Domain.Livestock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Livestock;

[ExcludeFromCodeCoverage]
public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");

        builder.HasKey(x => x.Id)
            .HasName("location_pkey");
    }
}