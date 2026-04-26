using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public interface IReportRepository
{
    Task<List<CattleRegistration>> GetCattleRegistrationReport(DateTime dateTimeFrom, DateTime dateTimeTo);
}