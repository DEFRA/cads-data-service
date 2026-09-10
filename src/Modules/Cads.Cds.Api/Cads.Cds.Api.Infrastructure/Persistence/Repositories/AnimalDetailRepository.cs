using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Api.Infrastructure.Persistence.Repositories;

public class AnimalDetailRepository(
    IHostEnvironment env,
    IFileService fileService,
    IOptions<ApiModuleConfiguration> options) : IAnimalDetailRepository
{
    public const string FileName = "bovine_animal_details.json";

    public async Task<IEnumerable<AnimalDetailDto>> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data.Where(a => a.Identifier == identifier);
    }

    private async Task<IEnumerable<AnimalDetailDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!options.Value.StaticData.Enabled)
        {
            return [];
        }

        var fullPath = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path, FileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<IEnumerable<AnimalDetailDto>>(fullPath, cancellationToken) ?? [];
    }
}