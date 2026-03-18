using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;
using Cads.Cds.MiBff.Core.Services.Amsl2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Amsl2;

public class DestinationDetailsService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
: Amsl2SampleDataService<DestinationDetailsDto>(env, fileService, options, "amsl2_destination_details.json"), IDestinationDetailsService
{
    public async Task<IEnumerable<DestinationDetailsDto>> GetByIdAndTypeAsync(Guid desinationId, string destinationType, CancellationToken cancellationToken = default)
    {
        var data = await GetByIdAsync(desinationId, cancellationToken);

        return data.Where(h => h.Type == destinationType);
    }
}