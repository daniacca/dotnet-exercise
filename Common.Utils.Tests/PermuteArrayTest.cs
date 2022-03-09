using System.Linq;
using Utils.ExtensionMethods.IEnumerable;
using Xunit;

namespace Utils.Tests
{
    public class PermuteArrayTest
    {
        [Fact]
        public void Permute_Test()
        {
            var permutations = new int[] { 1, 2, 3 }.Permute().ToList();

            Assert.NotNull(permutations);
            Assert.Equal(6, permutations.Count);
        }
    }
}
