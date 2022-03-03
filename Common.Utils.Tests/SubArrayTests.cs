using Common.Utils.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using Utils.ExtensionMethods.IEnumerable;
using Xunit;

namespace Common.Utils.Tests
{
    public class SubArrayTests
    {
        [Fact]
        public void Test_SubArrays_Fixed_Input()
        {
            var input = new List<int> { 1, 2, 3, 4 };
            var subArrays = input.SubArrays().ToList();

            Assert.True(subArrays.Count == 10);
            Assert.All(subArrays, arr => Assert.True(arr.IsSubSet(input)));
        }

        [Fact]
        public void Test_SubArraysLinq_Fixed_Input()
        {
            var input = new List<int> { 1, 2, 3, 4 };
            var subArrays = input.SubArraysLinq().ToList();

            Assert.True(subArrays.Count == 10);
            Assert.All(subArrays, arr => Assert.True(arr.IsSubSet(input)));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void Test_SubArrays_Variable_Input(int maxObj)
        {
            var input = Enumerable.Range(0, maxObj).Select((e,i) => i+1);
            var subArrays = input.SubArrays().ToList();

            Assert.True(subArrays.Count > 0);
            Assert.All(subArrays, arr => Assert.True(arr.IsSubSet(input)));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void Test_SubArraysLinq_Variable_Input(int maxObj)
        {
            var input = Enumerable.Range(0, maxObj).Select((e, i) => i + 1);
            var subArrays = input.SubArraysLinq().ToList();

            Assert.True(subArrays.Count > 0);
            Assert.All(subArrays, arr => Assert.True(arr.IsSubSet(input)));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        public void Test_SubArrays_Big_Fixed_Input(int maxObj)
        {
            var input = Enumerable.Range(0, maxObj).Select((e, i) => i + 1);
            var subArrays = input.SubArrays().ToList();

            Assert.True(subArrays.Count > 0);
            Assert.All(subArrays, arr => Assert.True(arr.Count() <= input.Count()));
        }
    }
}
