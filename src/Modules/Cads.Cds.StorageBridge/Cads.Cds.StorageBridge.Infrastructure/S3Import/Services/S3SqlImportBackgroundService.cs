using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3SqlImportBackgroundService(
    Channel<CreateS3SqlImportJobDto> channel,
    ILogger<S3SqlImportBackgroundService> logger,
    IS3SqlScriptExecutorService processor)
    : S3ImportBackgroundService<CreateS3SqlImportJobDto>(channel, logger, processor)
{
}