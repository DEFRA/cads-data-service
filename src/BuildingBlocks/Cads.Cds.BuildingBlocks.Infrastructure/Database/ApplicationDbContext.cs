using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}