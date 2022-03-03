using System;
using System.Linq;
using Xunit;
using Utils.ExtensionMethods.IEnumerable;

namespace Utils.Tests
{
    public class SortingTests
    {
        [Fact]
        public void InsertionSort_Fixed_Input_Test()
        {
            var array = new int[] { 5, 2, 3, 9, 12, 15, 1, 4, 99 };

            var sorted = array.InsertionSort((int a, int b) => a > b ? 1 : 0);

            var expected = new int[] { 1, 2, 3, 4, 5, 9, 12, 15, 99 };
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void InsertionSort_Random_Test(int n)
        {
            var random = new Random();
            var array = Enumerable.Range(0, n).Select(i => random.Next(1000)).ToArray();

            var sorted = array.InsertionSort((int a, int b) => a > b ? 1 : 0);

            Assert.NotNull(sorted);
            Assert.All(sorted.ChunksOf(2), chunk => Assert.True(chunk[0] <= chunk[1]));
        }

        [Fact]
        public void MergeSort_Fixed_Input_Test()
        {
            var array = new int[] { 5, 2, 3, 9, 12, 15, 1, 4, 99 };

            var sorted = array.MergeSort((int a, int b) => a > b ? 1 : -1);

            var expected = new int[] { 1, 2, 3, 4, 5, 9, 12, 15, 99 };
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void MergeSort_Random_Test(int n)
        {
            var random = new Random();
            var array = Enumerable.Range(0, n).Select(i => random.Next(1000)).ToArray();

            var sorted = array.MergeSort((int a, int b) => a > b ? 1 : -1);

            Assert.NotNull(sorted);
            Assert.All(sorted.ChunksOf(2), chunk => Assert.True(chunk[0] <= chunk[1]));
        }
    }
}
