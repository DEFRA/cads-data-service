using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Core.Exceptions;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Dispatchers;

public class ScenarioGenerationDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_UnknownScenario_ThrowsGenerationValidationException()
    {
        // Arrange
        var dispatcher = new ScenarioGenerationDispatcher(new IGenerationScenario[] { });
        var request = new CreateGenerationRequestDto { Scenario = "NonExisting", RowCount =1 };

        // Act & Assert
        await Assert.ThrowsAsync<GenerationValidationException>(() =>
            dispatcher.DispatchAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task DispatchAsync_KnownScenario_InvokesScenarioExecuteAndReturnsResult()
    {
        // Arrange
        var expected = new CreateGenerationResponseDto { FileName = "file.txt", Content = "content", BusinessKeys = new decimal[] { 1m, 2m } };
        var mockScenario = new Mock<IGenerationScenario>();
        mockScenario.SetupGet(s => s.Name).Returns("TestScenario");
        mockScenario
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateGenerationRequestDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected)
            .Verifiable();

        var dispatcher = new ScenarioGenerationDispatcher(new[] { mockScenario.Object });
        var request = new CreateGenerationRequestDto { Scenario = "TestScenario", RowCount = 10 };

        // Act
        var result = await dispatcher.DispatchAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal(expected, result);
        mockScenario.Verify(s => s.ExecuteAsync(request, CancellationToken.None), Times.Once);
    }
}