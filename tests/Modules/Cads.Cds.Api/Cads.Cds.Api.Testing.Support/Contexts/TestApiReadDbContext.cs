using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Contexts;

public partial class TestApiReadDbContext(DbContextOptions<ApiReadDbContext> options)
    : ApiReadDbContext(options)
{
    /// <summary>
    /// Give fake keys so EF Core can track them (after base.OnModelCreating)
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAnimalFunctions(modelBuilder);
        ConfigureHoldingFunctions(modelBuilder);
    }
}