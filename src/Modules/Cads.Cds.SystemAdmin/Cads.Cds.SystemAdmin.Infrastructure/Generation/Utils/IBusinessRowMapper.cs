using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public interface IBusinessRowMapper
{
    /// <summary>
    /// Builds a row of the entity's business columns only, keyed by store column name. Pipeline columns
    /// (see <see cref="Cads.Cds.BuildingBlocks.Application.Imports.Envelope.IPipelineColumns"/>) are never included.
    /// </summary>
    IReadOnlyDictionary<string, object?> Map<T>(T entity, IReadOnlyList<BusinessColumn> columns)
        where T : class;
}
