using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Tests.Unit.Specimens;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Queries;

public class GetHoldingsByCphQueryHandlerTests
{
    [Fact]
    public async Task QueryHandlerShouldReturnCorrectHolding()
    {
        var holdings = HoldingsSampleDataHelper.GetSampleHoldings();

        var mockHoldingService = new Mock<IHoldingsService>();
        mockHoldingService
          .Setup(s => s.GetByCphAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(holdings.Take(1));

        var cph = "ABC123";

        var request = new GetHoldingsByCphQuery(cph);
        var adapter = new HoldingsQueryByCphAdapter(mockHoldingService.Object);
        var queryHandler = new GetHoldingByCphQueryHandler(adapter);

        var result = await queryHandler.Handle(request, CancellationToken.None);

        var expected = new HoldingDTO { Id = Guid.NewGuid(), Name = "Holding 1", Cph = cph };

        result.Results.First().Cph.Should().Be(expected.Cph);
    }

    [Fact]
    public async Task QueryHandlerShouldReturnEmpty()
    {
        var mockHoldingService = new Mock<IHoldingsService>();
        mockHoldingService
          .Setup(s => s.GetByCphAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync([]);

        var cph = "invalid";

        var request = new GetHoldingsByCphQuery(cph);
        var adapter = new HoldingsQueryByCphAdapter(mockHoldingService.Object);
        var queryHandler = new GetHoldingByCphQueryHandler(adapter); ;

        var result = await queryHandler.Handle(request, CancellationToken.None);

        result.Results.Count().Should().Be(0);
    }
}