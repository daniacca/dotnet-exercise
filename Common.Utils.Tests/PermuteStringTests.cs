using System.Linq;
using Utils.ExtensionMethods.String;
using Xunit;

namespace Utils.Tests
{
    public class PermuteStringTests
    {
        [Fact]
        public void Permute_Test()
        {
            var permutations = "ABC".Permute().ToList();

            Assert.NotNull(permutations);
            Assert.Equal(6, permutations.Count);
        }
    }
}
