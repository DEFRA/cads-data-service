using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;
using FluentAssertions;
using Moq;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine;

public class AnimalDetailsQueryAdapterTests
{
    [Fact]
    public async Task GetAsync_ShouldPassIdentifierToRepository_AndReturnItsResult()
    {
        var expected = new List<AnimalDetailDto> { new() { Identifier = "UK324537113234" } };

        var repository = new Mock<IAnimalDetailRepository>();
        repository
            .Setup(r => r.GetByIdentifierAsync("UK324537113234", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var sut = new AnimalDetailsQueryAdapter(repository.Object);

        var result = await sut.GetAsync(new GetAnimalDetailsQuery { Identifier = "UK324537113234" }, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(expected);
    }
}
