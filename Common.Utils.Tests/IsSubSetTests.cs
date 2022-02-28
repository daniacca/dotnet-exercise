using Common.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.ExtensionMethods.IEnumerable;
using Xunit;

namespace Common.Utils.Tests
{
    public class IsSubSetTests
    {
        [Fact]
        public void Test_Same_Set_Should_Return_True()
        {
            var superSet = new List<int> { 1, 2, 3, 4 };
            var subSet = new List<int> { 1, 2, 3, 4 };

            Assert.True(subSet.IsSubSet(superSet));
        }

        [Fact]
        public void Test_Empty_Set_Should_Return_True()
        {
            var superSet = new List<int> { };
            var subSet = new List<int> { };

            Assert.True(subSet.IsSubSet(superSet));
        }

        [Fact]
        public void Test_Wrong_Set_Should_Return_False()
        {
            var superSet = new List<int> { 5, 4, 2, 11, 23 };
            var subSet = new List<int> { 5, 11, 88, 99 };

            Assert.False(subSet.IsSubSet(superSet));
        }

        [Fact]
        public void Test_Greater_SubSet_Should_Return_False()
        {
            var superSet = new List<int> { 5, 4, 2, 11, 23 };
            var subSet = new List<int> { 5, 4, 2, 11, 23, 99 };

            Assert.False(subSet.IsSubSet(superSet));
        }

        [Fact]
        public void Test_Correct_SubSet_Should_Return_True()
        {
            var superSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var subSet = new List<int> { 1, 5, 10 };

            Assert.True(subSet.IsSubSet(superSet));
        }

        [Fact]
        public void Test_Random_SubSet()
        {
            var random = new Random();
            var superSet = Enumerable.Range(0, 1000).Select((e, i) => random.Next()).ToList();

            IEnumerable<int> subSet = null;
            var toTake = random.Next(1000);
            foreach(var sub in superSet.SubArrays())
            {
                if (toTake-- == 0)
                {
                    subSet = sub;
                    break;
                }
            }

            Assert.NotNull(subSet);
            Assert.True(subSet.IsSubSet(superSet));
        }
    }
}
