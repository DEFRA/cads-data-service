using System.Collections.Generic;
using Cads.Cds.SystemAdmin.Application.Imports.Envelope;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public interface IBusinessRowMapper
{
    /// <summary>
    /// Builds a row of the entity's business columns only, keyed by store column name. Pipeline columns
    /// (see <see cref="IPipelineColumns"/>) are never included.
    /// </summary>
    IReadOnlyDictionary<string, object?> Map<T>(T entity, IReadOnlyList<BusinessColumn> columns)
        where T : class;
}