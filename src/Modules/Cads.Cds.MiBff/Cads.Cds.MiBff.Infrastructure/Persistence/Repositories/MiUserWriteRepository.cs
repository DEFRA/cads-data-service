using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiUserWriteRepository
    : EfRepository<MiUser, MiBffWriteDbContext>
{
    public MiUserWriteRepository(MiBffWriteDbContext dbContext)
        : base(dbContext) { }
}