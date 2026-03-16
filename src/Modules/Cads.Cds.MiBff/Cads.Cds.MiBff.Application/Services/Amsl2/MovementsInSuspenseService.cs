using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs.Amls2;
using Cads.Cds.MiBff.Core.Services.Amsl2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Amsl2;

public class MovementsInSuspenseService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options) : Amsl2SampleDataService<Amsl2Dto>(env, fileService, options), IMovementsInSuspenseService
{
}