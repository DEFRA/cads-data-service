using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IDestinationDetailsService
{
    Task<IEnumerable<DestinationDetailsDto>> GetByIdAndTypeAsync(Guid desinationId, string destinationType, CancellationToken cancellationToken = default);
}