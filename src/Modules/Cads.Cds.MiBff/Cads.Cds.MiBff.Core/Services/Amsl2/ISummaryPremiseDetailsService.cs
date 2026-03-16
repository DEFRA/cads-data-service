using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface ISummaryPremiseDetailsService
{
    Task<IEnumerable<Amsl2Dto>> GetAllAsync(CancellationToken cancellationToken = default);
}