using System;
using System.Linq;
using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope;

public sealed class EnvelopeTableResolver(
    ITablePrefixResolver tablePrefixResolver,
    IBusinessColumnResolver businessColumnResolver,
    IEntityTypeStore entityTypeStore) : IEnvelopeTableResolver
{
    public EnvelopeTable Resolve(IReadOnlyEntityType entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        var businessColumns = businessColumnResolver.Resolve(entityType);
        var businessColumnNames = businessColumns.Select(b => b.Name).ToArray();

        return new EnvelopeTable(
            entityTypeStore.GetTable(entityType).Name,
            tablePrefixResolver.Resolve(entityType),
            businessColumnNames);
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