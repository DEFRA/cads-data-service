using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Core.Domain.Entities.Holdings;
using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Infrastructure.Persistence.Contexts;

public class ApiReadDbContext(DbContextOptions<ApiReadDbContext> options) : CadsDbContext(options)
{
    // # Module-specific entities

    // # Tables

    // # Functions

    // ## Animals
    public virtual IQueryable<AnimalDetail> GetAnimalDetail(string eartag)
        => FromExpression(() => GetAnimalDetail(eartag));

    public virtual IQueryable<AnimalOnHolding> GetAnimalsOnHolding(
        string cph,
        bool includeHistorical,
        long rowFrom,
        long rowTo,
        string? sortField,
        string? sortDirection,
        string? breedCode,
        string? sex)
        => FromExpression(() => GetAnimalsOnHolding(
            cph, includeHistorical, rowFrom, rowTo, sortField, sortDirection, breedCode, sex));

    // ## Holdings
    public virtual IQueryable<LocationSummary> GetLocationsSummary(string? cph, DateOnly? lastModifiedDate)
        => FromExpression(() => GetLocationsSummary(cph, lastModifiedDate));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // # Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApiReadDbContext).Assembly
        );

        // # Functions

        // ## Animals
        modelBuilder.HasDbFunction(
            GetType().GetMethod(nameof(GetAnimalDetail))!)
            .HasName("get_animal_detail")
            .HasSchema(SchemaName.Cads.GetDescription());

        modelBuilder.HasDbFunction(
            GetType().GetMethod(nameof(GetAnimalsOnHolding))!)
            .HasName("get_animals_on_holding")
            .HasSchema(SchemaName.Cads.GetDescription());

        // ## Holdings
        modelBuilder.HasDbFunction(
            GetType().GetMethod(nameof(GetLocationsSummary))!)
            .HasName("get_locations")
            .HasSchema(SchemaName.Cads.GetDescription());

        base.OnModelCreating(modelBuilder);
    }
}