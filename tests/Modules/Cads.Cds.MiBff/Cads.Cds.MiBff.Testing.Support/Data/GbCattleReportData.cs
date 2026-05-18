using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Testing.Support.Data;

public record GbCattleReportData(
    List<MiDeathSummary> DeathSummaries,
    List<MiBirthSummary> BirthSummaries);