using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

/// <summary>
/// Resolves the business columns of a <c>cts_transactions</c> entity from the EF Core model: every store column
/// except <see cref="IPipelineColumns"/>, with the business key first and the rest in DDL order.
/// </summary>
public interface IBusinessColumnResolver
{
    /// <summary>
    /// Resolves the business columns, using the <c>Cts</c> schema primary key from <c>TableInfo</c> as the business key.
    /// </summary>
    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType);

    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType, string businessKey);
}
