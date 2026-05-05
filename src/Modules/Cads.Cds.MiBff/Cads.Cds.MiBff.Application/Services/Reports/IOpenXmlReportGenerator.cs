using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public interface IOpenXmlReportGenerator
{
    MemoryStream Generate(List<MiBirthSummaryResult> data);
}