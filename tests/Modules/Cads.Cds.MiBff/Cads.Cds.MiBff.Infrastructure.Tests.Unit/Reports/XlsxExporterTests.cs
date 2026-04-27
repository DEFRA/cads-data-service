using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Infrastructure.Reports;
using FluentAssertions;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class XlsxExporterTests
{

    [Fact]
    public async Task CreateDocument()
    {
        var sut = new XlsxReportGenerator();
        var data = await new FakeReportRepository().GetCattleRegistrationReport(DateTime.MinValue, DateTime.MaxValue);

        using var stream = sut.Generate(data);

        /*
        stream.Position = 0;
        using (FileStream file = new FileStream("file.xlsx", FileMode.Create, System.IO.FileAccess.Write))
            await stream.CopyToAsync(file, TestContext.Current.CancellationToken);*/

        stream.Should().NotBeNull(); // weak assertion, can improve later
    }
}