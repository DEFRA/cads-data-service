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

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("abc", "abc")]
        [InlineData("AbC", "abc")]
        [InlineData("ABC", "abc")]
        [InlineData("123", "123")]
        [InlineData("HELLO WORLD", "hello world")]
        public void NormalizeToLower_ReturnsExpectedResult(string? input, string? expected)
        {
            var result = input.NormalizeToLower();
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, "", null)]
        [InlineData(null, "abc", null)]
        [InlineData("", null,"")]
        [InlineData("", "","")]
        [InlineData("", "abc","")]
        [InlineData("abc", null, "abc")]
        [InlineData("abc", "", "abc")]
        [InlineData("abc", "abc", "")]
        [InlineData("uptohere", "here", "upto")]
        public void ParseUpToFirstOccurrence_ReturnsExpectedResult(string? input, string? occurrence, string? expected)
        {
            var act = () => input.ParseUpToFirstOccurrence(occurrence);
            if(input == null)
            {
                Assert.Throws<ArgumentNullException>(act);
            }
            else
            {
                var result = act();
                Assert.Equal(expected, result);
            }
        }
    }
}