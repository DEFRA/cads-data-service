using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3CsvImportBackgroundService(
    Channel<CreateS3CsvImportJobDto> channel,
    ILogger<S3CsvImportBackgroundService> logger,
    IS3ToPostgresCopyService processor
)
    : S3ImportBackgroundService<CreateS3CsvImportJobDto>(channel, logger, processor)
{
}