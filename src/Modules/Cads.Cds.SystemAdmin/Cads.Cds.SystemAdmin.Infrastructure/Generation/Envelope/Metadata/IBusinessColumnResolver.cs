using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

public interface IBusinessColumnResolver
{
    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType);
    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType, string businessKey);
}