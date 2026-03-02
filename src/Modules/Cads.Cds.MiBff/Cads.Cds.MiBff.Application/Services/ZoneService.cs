using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services;

public class ZoneService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options) : UkvSampleDataService<UkvDto>(env, fileService, options), IZoneService
{
    public Task<IEnumerable<UkvDto>> GetByZoneIdAsync(string zoneId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}