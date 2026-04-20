using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class FakeReportRepository : IReportRepository
{
    public Task<List<CattleMovement>> GetCattleMovementReport()
    {
        return Task.FromResult(CattleMovement.GetFakeData(25));
    }
}