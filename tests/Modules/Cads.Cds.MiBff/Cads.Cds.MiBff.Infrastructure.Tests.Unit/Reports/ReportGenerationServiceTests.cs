using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.Services.Reports;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Reports;

public class ReportGenerationServiceTests
{
    [Fact]
    public async Task CreateCattleRegistrationsReportTest()
    {
        var from = new DateOnly(2023, 1, 1);
        var to = new DateOnly(2024, 1, 1);
        var mockDataSource = new Mock<IMiReportRepository>();
        var mockXlsxGenerator = new Mock<IOpenXmlReportGenerator>();
        var stubData = new List<MiBirthSummaryResult>();
        mockDataSource.Setup(x => x.GetBirthSummaryAsync(from, to, It.IsAny<CancellationToken>())).ReturnsAsync(stubData);
        using var resultStream = new MemoryStream();
        mockXlsxGenerator.Setup(x => x.Generate(stubData)).Returns(resultStream);

        var sut = new ReportGenerationService(mockXlsxGenerator.Object, mockDataSource.Object);

        var result = await sut.GetCattleRegistrations(from, to, TestContext.Current.CancellationToken);
        result.Should().BeSameAs(resultStream); // assert result is exact same object as mock is set up to return - only possible if the mocks are called in sequence with correct params
    }
}