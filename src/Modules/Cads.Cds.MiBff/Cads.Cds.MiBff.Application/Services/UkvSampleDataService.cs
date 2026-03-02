using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services;

public abstract class UkvSampleDataService<TDto>(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    where TDto : class, IDataIdentity
{
    private const string StaticDataFileName = "ukv_data.json";

    public async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            return await LoadFromFileAsync(cancellationToken);
        }

        return [];
    }

    public async Task<IEnumerable<TDto>> GetByidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            var data = await LoadFromFileAsync(cancellationToken);

            return data.Where(h => h.Id == id);
        }

        return [];
    }

    private async Task<IEnumerable<TDto>> LoadFromFileAsync(CancellationToken cancellationToken = default)
    {
        var staticRoot = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path);
        var fullPath = Path.Combine(staticRoot, StaticDataFileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<TDto>>(fullPath, cancellationToken) ?? [];
    }
}