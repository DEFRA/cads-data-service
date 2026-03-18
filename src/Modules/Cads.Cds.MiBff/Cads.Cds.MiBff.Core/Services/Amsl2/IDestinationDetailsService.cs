using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IDestinationDetailsService
{
    Task<IEnumerable<DestinationDetailsDto>> GetByIdAndTypeAsync(Guid desinationId, string destinationType, CancellationToken cancellationToken = default);
}