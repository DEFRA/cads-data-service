using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Infrastructure.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class XlsxExporterTests
{
    
    [Fact]
    public void CreateDocument()
    {
        var sut = new XlsxReportGenerator();
        var data = CattleRegistration.GetFakeData(25);
        
        sut.Save(data, "./file.xlsx");
    }
}