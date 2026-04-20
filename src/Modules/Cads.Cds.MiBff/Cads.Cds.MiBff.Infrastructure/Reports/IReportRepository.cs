using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public interface IReportRepository
{
    Task<List<CattleMovement>> GetCattleMovementReport();
}