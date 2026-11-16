using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

public sealed class EnvelopeTableResolver(
    ITablePrefixResolver tablePrefixResolver,
    IBusinessColumnResolver businessColumnResolver) : IEnvelopeTableResolver
{
    public EnvelopeTable Resolve(IReadOnlyEntityType entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        return new EnvelopeTable(
            EntityTypeStore.GetTable(entityType).Name,
            tablePrefixResolver.Resolve(entityType),
            [.. businessColumnResolver.Resolve(entityType).Select(c => c.Name)]);
    }

    public EnvelopeTable Resolve(IReadOnlyModel model, Type clrType)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(clrType);

        var entityType = model.FindEntityType(clrType)
            ?? throw new InvalidOperationException($"Entity '{clrType.Name}' is not mapped in the model.");

        return Resolve(entityType);
    }
}