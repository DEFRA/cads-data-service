using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Testing.Support.Fakes.DbFunctions;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Contexts;

public partial class TestApiReadDbContext
{
    public DbSet<AnimalDetail> AnimalDetails => Set<AnimalDetail>();
    public override IQueryable<AnimalDetail> GetAnimalDetail(string eartag)
        => AnimalDetails.Where(x => x.Identifier == eartag);

    public DbSet<AnimalOnHolding> AnimalOnHoldings => Set<AnimalOnHolding>();
    public override IQueryable<AnimalOnHolding> GetAnimalsOnHolding(
        string cph, bool includeHistorical, long rowFrom, long rowTo,
        string? sortField, string? sortDirection, string? breedCode, string? sex)
        => AnimalOnHoldings.ApplyGetAnimalsOnHolding(
            cph, rowFrom, rowTo, sortField, sortDirection, breedCode, sex);

    private static void ConfigureAnimalFunctions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalDetail>()
            .HasKey(x => x.Identifier);

        modelBuilder.Entity<AnimalOnHolding>()
            .HasKey(x => new { x.CphNumber, x.AnimalId, x.DateOnCph });
    }
}