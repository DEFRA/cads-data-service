using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Services;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries.Adapters;

public class HoldingsQueryAdapterTests
{
    [Fact]
    public async Task GetHoldingsAsync_ShouldCallServiceWithCorrectParameters()
    {
        var holdings = HoldingsSampleDataHelper.GetSampleHoldings();

        var mockHoldingService = new Mock<IHoldingsService>();
        mockHoldingService
          .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(holdings);

        var adapter = new HoldingsQueryAdapter(mockHoldingService.Object);

        var query = new GetHoldingsQuery
        {
            Page = 1,
            PageSize = 20
        };

        var (items, count) = await adapter.GetHoldingsAsync(query,TestContext.Current.CancellationToken);

        items.Should().BeEquivalentTo(holdings);
        count.Should().Be(5);
    }
}
