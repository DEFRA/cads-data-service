using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Services;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries;

public class GetHoldingsQueryHandlerTests
{
    [Fact]
    public async Task QueryHandlerShouldReturnRecordinCorrectMapping()
    {
        var holdings = HoldingsSampleDataHelper.GetSampleHoldings();

        var mockHoldingService = new Mock<IHoldingsService>();
        mockHoldingService
          .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(holdings);

        var request = new GetHoldingsQuery();
        var adapter = new HoldingsQueryAdapter(mockHoldingService.Object);
        var queryHandler = new GetHoldingsQueryHandler(adapter);

        var result = await queryHandler.Handle(request, CancellationToken.None);

        result.Values.Should().BeEquivalentTo(holdings);
        result.Count.Should().Be(holdings.Count);
    }
}