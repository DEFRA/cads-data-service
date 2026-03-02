using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services;

public class AuditService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options) : UkvSampleDataService<UkvDto>(env, fileService, options), IAuditService
{
    public async Task<IEnumerable<UkvDto>> GetScrapieAsync(CancellationToken cancellationToken = default)
    {
        return await GetAllAsync(cancellationToken);
    }
}