using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

public sealed record BusinessColumn(string Name, IReadOnlyProperty Property);