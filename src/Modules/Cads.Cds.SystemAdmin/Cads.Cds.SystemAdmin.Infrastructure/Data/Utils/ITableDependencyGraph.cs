using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Utils;

public interface ITableDependencyGraph<T>
    where T : DbContext
{
    // Null if `table` isn't a recognized cts-schema table.
    // Empty list if it is, but has no FK dependencies.
    IReadOnlyList<string>? DependenciesOf(string table);
}