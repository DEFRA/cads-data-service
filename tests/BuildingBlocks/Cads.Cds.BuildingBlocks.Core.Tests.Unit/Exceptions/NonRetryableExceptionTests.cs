using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Exceptions;

public class NonRetryableExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new NonRetryableException("non-retry message");

        ex.Message.Should().Be("non-retry message");
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsInnerException()
    {
        var inner = new Exception("inner");
        var ex = new NonRetryableException("outer", inner);

        ex.Message.Should().Be("outer");
        ex.InnerException.Should().Be(inner);
    }
}