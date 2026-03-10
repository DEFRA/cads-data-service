using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Ukv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Ukv;

public class CohortService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    : UkvSampleDataService<UkvDto>(env, fileService, options), ICohortService
{
    public async Task<IEnumerable<UkvDto>> GetByAnimalIdAsync(Guid animalId, CancellationToken cancellationToken = default)
    {
        return await GetByIdAsync(animalId, cancellationToken);
    }
}