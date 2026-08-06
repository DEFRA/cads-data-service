using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories
{
    public abstract class EFReadWriteRepository<TEntity, TReadContext, TWriteContext>(TReadContext readDbContext, TWriteContext writeDbContext)
    : EFReadOnlyRepository<TEntity, TReadContext>(readDbContext), IRepository<TEntity>
        where TEntity : class
        where TReadContext : CadsDbContext
        where TWriteContext : CadsDbContext
    {
        private readonly TWriteContext _writeDbContext = writeDbContext;

        public IQueryable<TEntity> Set()
            => _writeDbContext.Set<TEntity>();

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _writeDbContext.AddAsync(entity, cancellationToken);
        }

        public Task Remove(TEntity entity)
        {
            _writeDbContext.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<TEntity?> FindForWriteAsync(params object[] keyValues)
            => await _writeDbContext.Set<TEntity>().FindAsync(keyValues);
    }
}