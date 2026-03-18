using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IAuditService
{
    Task<IEnumerable<UkvDto>> GetScrapieAsync(CancellationToken cancellationToken = default);
}