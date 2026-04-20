using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Core.Services.Reports;

public interface IXlsxReportGenerator
{
    MemoryStream Generate(List<CattleMovement> data);
}