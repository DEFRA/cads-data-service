using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class FakeReportRepository : IReportRepository
{
    public Task<List<CattleRegistration>> GetCattleRegistrationReport()
    {
        return Task.FromResult(CattleRegistration.GetFakeData(25));
    }
}