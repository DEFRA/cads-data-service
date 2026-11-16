using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public sealed record BusinessColumn(string Name, IReadOnlyProperty Property);