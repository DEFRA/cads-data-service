using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IMovementInSuspenseService
{
    Task<IEnumerable<Amsl2Dto>> GetAllAsync(CancellationToken cancellationToken = default);
}