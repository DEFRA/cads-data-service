using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public class S3CsvBulkLoadBackgroundService(
    Channel<CreateS3CsvBulkLoadJobDto> channel,
    ILogger<S3CsvBulkLoadBackgroundService> logger,
    IS3ToPostgresCopyService processor
)
    : S3BulkLoadBackgroundService<CreateS3CsvBulkLoadJobDto>(channel, logger, processor)
{
}