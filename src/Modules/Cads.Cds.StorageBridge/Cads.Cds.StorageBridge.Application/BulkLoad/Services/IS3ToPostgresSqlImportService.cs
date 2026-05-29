using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3ToPostgresSqlImportService : IS3ToPostgresService<CreateS3SqlImportJobDto>
{
}