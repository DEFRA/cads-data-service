using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Testing.Support.Data;

public record ReportData(List<MiDeathSummary> Deaths, List<MiBirthSummary> Births);