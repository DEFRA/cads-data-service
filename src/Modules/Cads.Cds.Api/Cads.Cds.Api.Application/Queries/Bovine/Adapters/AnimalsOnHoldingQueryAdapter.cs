using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;
using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.Api.Application.Queries.Bovine.Adapters;

public class AnimalsOnHoldingQueryAdapter(
    IHostEnvironment env,
    IFileService fileService,
    IOptions<ApiModuleConfiguration> options)
{
    public const string FileName = "bovine_animals_on_holding.json";

    public async Task<AnimalsOnHoldingDto?> SearchAsync(
        GetAnimalsOnHolding query,
        CancellationToken cancellationToken = default)
    {
        var data = await GetAsync(cancellationToken);

        return data;
    }

    private async Task<AnimalsOnHoldingDto?> GetAsync(CancellationToken cancellationToken)
    {
        if (!options.Value.StaticData.Enabled)
        {
            return null;
        }

        var fullPath = Path.Combine(env.ContentRootPath, options.Value.StaticData.Path, FileName);

        return await fileService.ReadJsonFromFileAndReturnAsModelAsync<AnimalsOnHoldingDto>(fullPath, cancellationToken) ?? null;
    }
}