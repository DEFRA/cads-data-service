using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Api.Infrastructure.Persistence.Repositories;

public class AnimalsOnCphRepository(
    IHostEnvironment env,
    IFileService fileService,
    IOptions<ApiModuleConfiguration> options) : IAnimalsOnCphRepository
{
    public const string FileName = "bovine_animals_on_cph.json";

    public async Task<AnimalHoldingDto?> GetByCphAsync(string cph, CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data.SingleOrDefault(h => string.Equals(h.Cph, cph, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<IEnumerable<AnimalHoldingDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!options.Value.StaticData.Enabled)
        {
            return [];
        }

        var fullPath = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path, FileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<AnimalHoldingDto>>(fullPath, cancellationToken) ?? [];
    }
}