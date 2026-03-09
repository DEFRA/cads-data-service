using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services;

public abstract class SampleDataService<TDto>(
    string fileName,
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    where TDto : class, IDataIdentity, IDataCode
{
    public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            return await LoadFromFileAsync(cancellationToken);
        }

        return [];
    }

    public virtual async Task<IEnumerable<TDto>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        if (options.Value.StaticData.Enabled)
        {
            var data = await LoadFromFileAsync(cancellationToken);

            return data.Where(h => h.Code == code);
        }

        return [];
    }

    public virtual async Task<IEnumerable<TDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
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
        var fullPath = Path.Combine(staticRoot, fileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<TDto>>(fullPath, cancellationToken) ?? [];
    }
}