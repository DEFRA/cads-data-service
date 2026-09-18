using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Api.Application.Queries.Bovine.Adapters;

public class AnimalDetailsQueryAdapter(
    IHostEnvironment env,
    IFileService fileService,
    IOptions<ApiModuleConfiguration> options)
{
    public const string FileName = "bovine_animal_details.json";

    public async Task<AnimalDetailsDto?> GetByIdentifierAsync(
        GetAnimalDetailsByIdentifier query,
        CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data;
    }

    private async Task<AnimalDetailsDto?> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!options.Value.StaticData.Enabled)
        {
            return null;
        }

        var fullPath = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path, FileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<AnimalDetailsDto>(fullPath, cancellationToken) ?? null;
    }
}