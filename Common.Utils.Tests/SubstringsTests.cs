using System.Collections.Generic;
using System.Linq;
using Utils.ExtensionMethods.String;
using Xunit;

namespace Utils.Tests
{
    public class SubstringsTests
    {
        [Fact]
        public void SubStrings_Test()
        {
            var substrings = "ABC".Substrings().ToList();

            Assert.NotNull(substrings);
            Assert.Equal(6, substrings.Count);

            var expected = new List<string> { "ABC", "AB", "A", "BC", "B", "C" };
            Assert.Equal(expected, substrings);
        }
    }
}
