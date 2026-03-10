using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IAuditService
{
    Task<IEnumerable<UkvDto>> GetScrapieAsync(CancellationToken cancellationToken = default);
}