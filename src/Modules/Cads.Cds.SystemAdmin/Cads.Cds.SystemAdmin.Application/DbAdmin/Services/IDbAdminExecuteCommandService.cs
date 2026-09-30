using System.Text.Json;
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

namespace Cads.Cds.SystemAdmin.Application.DbAdmin.Services;

public interface IDbAdminExecuteCommandService
{
    Task<JsonDocument> ExecuteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : DbAdminRequestBaseDto;
}