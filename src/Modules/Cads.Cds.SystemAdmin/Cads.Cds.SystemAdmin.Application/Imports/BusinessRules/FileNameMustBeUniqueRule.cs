using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using System.Net;

namespace Cads.Cds.SystemAdmin.Application.Imports.BusinessRules;

public class FileNameMustBeUniqueRule(
    ISystemAdminFileImportRepository repository,
    string fileName,
    CancellationToken cancellationToken) : IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        var existingFileImport = repository
            .GetByFileNameAsync(fileName, cancellationToken)
            .GetAwaiter()
            .GetResult();

        return existingFileImport != null;
    }

    public string Message =>
        $"A record exists with matching file name '{fileName}'.";
}