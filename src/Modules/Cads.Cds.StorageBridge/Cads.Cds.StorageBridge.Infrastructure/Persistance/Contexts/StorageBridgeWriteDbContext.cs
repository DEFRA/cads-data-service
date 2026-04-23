using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class StorageBridgeWriteDbContext(DbContextOptions<StorageBridgeWriteDbContext> options) : DbContext(options)
{
}