using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs.Amls2;
using Cads.Cds.MiBff.Core.Services.Amsl2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Amsl2;

public class DepartureDetailsService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    : Amsl2SampleDataService<DepartureDetailsDto>(env, fileService, options, "amsl2_departure_details.json"), IDepartureDetailsService
{
    public async Task<IEnumerable<DepartureDetailsDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data.Where(h => h.Cph == cph);
    }
}