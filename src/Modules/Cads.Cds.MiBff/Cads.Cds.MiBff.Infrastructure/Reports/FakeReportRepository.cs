using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class FakeReportRepository : IReportRepository
{
    public Task<List<CattleRegistration>> GetCattleRegistrationReport(DateTime dateTimeFrom, DateTime dateTimeTo, CancellationToken cancellationToken)
    {
        return Task.FromResult(CattleRegistration.GetFakeData(25));
    }
}