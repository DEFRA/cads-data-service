using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

public class FakeReportAccessService : IReportAccessService
{
    public Task<IEnumerable<string>> GetUserReportPermissionsAsync(string externalSubject, string reportKey, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReportDto>> GetUserReportsAsync(string identifier, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasReportAccessAsync(string externalSubject, string reportKey, CancellationToken ct)
        => Task.FromResult(true);
}
