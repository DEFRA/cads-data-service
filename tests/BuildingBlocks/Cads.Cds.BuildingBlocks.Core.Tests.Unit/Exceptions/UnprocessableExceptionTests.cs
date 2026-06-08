using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Exceptions;

public class UnprocessableExceptionTests
{
    [Fact]
    public void Constructor_WithNameAndKey_SetsFormattedMessage()
    {
        var ex = new UnprocessableException("User", 123);

        ex.Message.Should().Be("'User' (123) was uprocessable.");
        ex.Title.Should().Be("Unprocessable content");
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new UnprocessableException("custom message");

        ex.Message.Should().Be("custom message");
        ex.Title.Should().Be("Unprocessable content");
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsInnerException()
    {
        var inner = new Exception("inner");
        var ex = new UnprocessableException("outer", inner);

        ex.Message.Should().Be("outer");
        ex.InnerException.Should().Be(inner);
    }
}