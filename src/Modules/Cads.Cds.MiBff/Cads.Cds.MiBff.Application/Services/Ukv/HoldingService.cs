using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Ukv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Ukv;

public class HoldingService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    : UkvSampleDataService<HoldingDto>(env, fileService, options, "ukv_holdings.json"), IHoldingService
{
    public async Task<IEnumerable<HoldingDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data.Where(h => h.Cph == cph);
    }
}