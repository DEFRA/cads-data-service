using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new DomainException("test message");

        ex.Message.Should().Be("test message");
        ex.Title.Should().Be("Bad Request");
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsInnerException()
    {
        var inner = new Exception("inner");
        var ex = new DomainException("outer", inner);

        ex.Message.Should().Be("outer");
        ex.InnerException.Should().Be(inner);
    }
}