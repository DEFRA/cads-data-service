using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Api.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class ApiReadDbContext(DbContextOptions options) : CadsDbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

    // Tables

    // Functions
    public virtual IQueryable<LocationSummary> GetLocationsSummary(string? cph, DateOnly? lastModifiedDate)
        => FromExpression(() => GetLocationsSummary(cph, lastModifiedDate));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApiReadDbContext).Assembly
        );

        modelBuilder.HasDbFunction(
            GetType().GetMethod(nameof(GetLocationsSummary))!)
            .HasName("get_locations")
            .HasSchema(SchemaName.Cads.GetDescription());

        base.OnModelCreating(modelBuilder);
    }
}