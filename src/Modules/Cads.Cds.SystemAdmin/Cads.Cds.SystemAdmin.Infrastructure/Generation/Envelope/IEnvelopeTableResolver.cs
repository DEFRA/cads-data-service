using System;
using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope;

public interface IEnvelopeTableResolver
{
    EnvelopeTable Resolve(IReadOnlyEntityType entityType);
    EnvelopeTable Resolve(IReadOnlyModel model, Type clrType);
}