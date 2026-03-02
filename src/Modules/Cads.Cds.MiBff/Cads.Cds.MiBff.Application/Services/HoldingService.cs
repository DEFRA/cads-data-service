using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services;

public class HoldingService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options) : IHoldingService
{
    private const string StaticDataFileName = "ukv_holdings.json";

    public async Task<IEnumerable<HoldingDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            var holdings = await LoadHoldingsFromFileAsync(cancellationToken);

            return holdings;
        }

        return [];
    }

    public async Task<IEnumerable<HoldingDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            var holdings = await LoadHoldingsFromFileAsync(cancellationToken);

            return holdings.Where(h => h.Cph.Equals(cph, StringComparison.OrdinalIgnoreCase));
        }

        return [];
    }

    private async Task<IEnumerable<HoldingDto>> LoadHoldingsFromFileAsync(CancellationToken cancellationToken = default)
    {
        var staticRoot = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path);
        var fullPath = Path.Combine(staticRoot, StaticDataFileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<HoldingDto>>(fullPath, cancellationToken) ?? [];
    }
}