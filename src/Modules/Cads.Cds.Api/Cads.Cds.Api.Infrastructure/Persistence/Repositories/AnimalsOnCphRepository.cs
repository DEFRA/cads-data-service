using Cads.Cds.Api.Core.Configuration;
using Cads.Cds.Api.Core.Domain.Paging;
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

    public async Task<bool> HoldingExistsAsync(string cph, CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        return data.Any(h => Matches(h, cph));
    }

    public async Task<PagedResult<TResult>> QueryAnimalsAsync<TResult>(
        string cph,
        Func<IQueryable<AnimalOnCphDto>, IQueryable<TResult>> shape,
        Func<IQueryable<TResult>, IOrderedQueryable<TResult>> order,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var data = await GetAllAsync(cancellationToken);

        var animals = data
            .Where(h => Matches(h, cph))
            .SelectMany(h => h.Animals)
            .AsQueryable();

        var shaped = shape(animals);

        return new PagedResult<TResult>
        {
            TotalRecords = shaped.Count(),
            Items = [.. order(shaped).Skip((page - 1) * pageSize).Take(pageSize)]
        };
    }

    private static bool Matches(AnimalHoldingDto holding, string cph)
        => string.Equals(holding.Cph, cph, StringComparison.OrdinalIgnoreCase);

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