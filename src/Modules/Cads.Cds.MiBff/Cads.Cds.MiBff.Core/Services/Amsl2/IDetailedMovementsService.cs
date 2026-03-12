using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IDetailedMovementsService
{
    Task<IEnumerable<Amsl2Dto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Amsl2Dto>> GetByMovementIdAsync(Guid movementId, CancellationToken cancellationToken = default);
}