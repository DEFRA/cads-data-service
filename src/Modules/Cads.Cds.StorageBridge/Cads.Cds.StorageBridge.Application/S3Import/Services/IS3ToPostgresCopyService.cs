using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IS3ToPostgresCopyService : IS3ToPostgresService<CreateS3CsvImportJobDto>
{
}