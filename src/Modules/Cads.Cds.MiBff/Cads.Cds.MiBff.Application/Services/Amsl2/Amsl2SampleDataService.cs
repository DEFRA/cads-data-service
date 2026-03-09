using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Amsl2;

public abstract class Amsl2SampleDataService<TDto>(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options,
    string filename = "amsl2_data.json") : SampleDataService<TDto>(
    filename,
     env,
     fileService,
     options)
    where TDto : class, IDataIdentity
{
}