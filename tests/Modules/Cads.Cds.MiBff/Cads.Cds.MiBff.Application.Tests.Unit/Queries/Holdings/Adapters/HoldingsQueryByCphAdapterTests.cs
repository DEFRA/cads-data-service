using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Tests.Unit.Specimens;
using Cads.Cds.MiBff.Core.Services;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries.Holdings.Adapters;

public class HoldingsQueryByCphAdapterTests
{
    [Fact]
    public async Task GetHoldingsAsync_ShouldCallServiceWithCorrectParameters()
    {
        var holdings = HoldingsSampleDataHelper.GetSampleHoldings();

        var mockHoldingService = new Mock<IHoldingService>();
        mockHoldingService
          .Setup(s => s.GetByCphAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(holdings.Take(1));

        var adapter = new HoldingsQueryAdapter(mockHoldingService.Object);

        var query = new GetHoldingsByCphQuery("ABC123");

        var (items, count) = await adapter.GetAsync(query, TestContext.Current.CancellationToken);

        items.Should().BeEquivalentTo(new[] { holdings[0] });
        count.Should().Be(1);
    }
}