using System;
using System.Linq;
using Xunit;
using Utils.ExtensionMethods.IEnumerable;
using System.Collections.Generic;

namespace Utils.Tests
{
    public class SortingTests
    {
        static Func<int, int, int> NumberComparer => (int a, int b) => a - b;

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { SortingStrategy.insertion },
            new object[] { SortingStrategy.bubble },
            new object[] { SortingStrategy.merge },
            new object[] { SortingStrategy.quick },
            new object[] { SortingStrategy.heap },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Sort_Fixed_Input_Test(SortingStrategy strategy)
        {
            var array = new int[] { 88, 5, 2, 14, 3, 113, 9, 12, 15, 1, 4, 99, 250 };

            var sorted = array.Sort(strategy, NumberComparer);

            var expected = new int[] { 1, 2, 3, 4, 5, 9, 12, 14, 15, 88, 99, 113, 250 };
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Sort_Already_Sorted_Input_Test(SortingStrategy strategy)
        {
            var array = new int[] { 1, 2, 3, 4, 5, 9, 12, 14, 15, 88, 99, 113, 250 };

            var sorted = array.Sort(strategy, NumberComparer);

            Assert.NotNull(sorted);
            Assert.Equal(array, sorted);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Sort_Inverted_Fixed_Input_Test(SortingStrategy strategy)
        {
            var array = new int[] { 1000, 950, 700, 800, 650, 456, 444, 432, 432, 329, 300, 200, 100, 0 };

            var sorted = array.Sort(strategy, NumberComparer);

            var expected = array.OrderBy(i => i);
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }

        [Theory]
        [InlineData(SortingStrategy.insertion, 10000)]
        [InlineData(SortingStrategy.bubble, 10000)]
        [InlineData(SortingStrategy.merge, 20000)]
        [InlineData(SortingStrategy.quick, 20000)]
        [InlineData(SortingStrategy.heap, 20000)]
        public void Sort_Random_Test(SortingStrategy strategy, int n)
        {
            var random = new Random();
            var array = Enumerable.Range(0, n).Select(i => random.Next(10000)).ToArray();

            var sorted = array.Sort(strategy, NumberComparer);

            var expected = array.OrderBy(i => i);
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }

        [Theory]
        [InlineData(SortingStrategy.insertion, 10000)]
        [InlineData(SortingStrategy.bubble, 10000)]
        [InlineData(SortingStrategy.merge, 20000)]
        [InlineData(SortingStrategy.quick, 20000)]
        [InlineData(SortingStrategy.heap, 20000)]
        public void Sort_Inverted_Random_Test(SortingStrategy strategy, int n)
        {
            var random = new Random();
            var array = Enumerable.Range(0, n).Select(i => random.Next(10000)).OrderByDescending(i => i).ToArray();

            var sorted = array.Sort(strategy, NumberComparer);

            var expected = array.OrderBy(i => i);
            Assert.NotNull(sorted);
            Assert.Equal(expected, sorted);
        }
    }
}
