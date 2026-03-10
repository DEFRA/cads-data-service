using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IDataQualityService
{
    Task<IEnumerable<UkvDto>> GetUnregisteredAsync(CancellationToken cancellationToken = default);
}