using Cads.Cds.BuildingBlocks.Core.Extensions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Extensions;

public class StringExtensionTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("abc", "ABC")]
        [InlineData("AbC", "ABC")]
        [InlineData("ABC", "ABC")]
        [InlineData("123", "123")]
        [InlineData("hello world", "HELLO WORLD")]
        public void NormalizeToUpper_ReturnsExpectedResult(string? input, string? expected)
        {
            var result = input.NormalizeToUpper();
            Assert.Equal(expected, result);
        }
    }
}