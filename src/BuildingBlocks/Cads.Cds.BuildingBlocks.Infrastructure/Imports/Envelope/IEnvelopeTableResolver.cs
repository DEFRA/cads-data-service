using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

/// <summary>Resolves the <see cref="EnvelopeTable"/> of a <c>cts_transactions</c> entity.</summary>
public interface IEnvelopeTableResolver
{
    EnvelopeTable Resolve(IReadOnlyEntityType entityType);

    EnvelopeTable Resolve(IReadOnlyModel model, Type clrType);
}
