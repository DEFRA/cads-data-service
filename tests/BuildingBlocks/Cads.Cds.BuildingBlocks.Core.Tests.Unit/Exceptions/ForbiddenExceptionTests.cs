using Cads.Cds.BuildingBlocks.Core.Exceptions;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Exceptions;

public class ForbiddenExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new ForbiddenException("forbidden message");

        ex.Message.Should().Be("forbidden message");
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsInnerException()
    {
        var inner = new Exception("inner");
        var ex = new ForbiddenException("outer", inner);

        ex.Message.Should().Be("outer");
        ex.InnerException.Should().Be(inner);
    }
}