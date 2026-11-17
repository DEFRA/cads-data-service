using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

public sealed record BusinessColumn(string Name, IReadOnlyProperty Property);