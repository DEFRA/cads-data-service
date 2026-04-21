using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs.Ukv;
using Cads.Cds.MiBff.Core.Services.Ukv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Ukv;

public class JourneyHaulierService(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options)
    : UkvSampleDataService<UkvDto>(env, fileService, options), IJourneyHaulierService
{
}