using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public class S3SqlBulkLoadBackgroundService(
    Channel<CreateS3SqlImportJobDto> channel,
    ILogger<S3SqlBulkLoadBackgroundService> logger,
    IS3SqlScriptExecutorService processor)
    : S3BulkLoadBackgroundService<CreateS3SqlImportJobDto>(channel, logger, processor)
{
}   