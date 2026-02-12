using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_WithNameAndKey_SetsFormattedMessage()
    {
        var ex = new NotFoundException("User", 123);

        ex.Message.Should().Be("'User' (123) was not found.");
        ex.Title.Should().Be("Not Found");
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new NotFoundException("custom message");

        ex.Message.Should().Be("custom message");
        ex.Title.Should().Be("Not Found");
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsInnerException()
    {
        var inner = new Exception("inner");
        var ex = new NotFoundException("outer", inner);

        ex.Message.Should().Be("outer");
        ex.InnerException.Should().Be(inner);
    }
}