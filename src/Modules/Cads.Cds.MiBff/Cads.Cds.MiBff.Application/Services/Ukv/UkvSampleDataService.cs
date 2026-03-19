using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services.Ukv;

public abstract class UkvSampleDataService<TDto>(
    IWebHostEnvironment env,
    IFileService fileService,
    IOptions<MiBffModuleConfiguration> options,
    string fileName = "ukv_data.json") : SampleDataService<TDto>(
    fileName,
     env,
     fileService,
     options)
    where TDto : class, IDataIdentity
{ }