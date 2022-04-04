using DataStructure.List;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataStructure.Tests
{
    public class SimpleLinkedListTests
    {
        static IEnumerable<int> RandomSequence => Enumerable.Range(0, 100).Select(x => new Random().Next());

        static IEnumerable<int> RandomSequence5K => Enumerable.Range(0, 5000).Select(x => new Random().Next(100));

        static IEnumerable<int> ProgressiveSequence => Enumerable.Range(0, 10).Select((_x, i) => i + 1);

        [Fact]
        public void List_Random_Test()
        {
            var testSequence = RandomSequence.ToList();
            var list = new SinglyLinkedList<int>();

            Assert.Equal(0, list.Count);

            list.Add(testSequence);

            Assert.Equal(100, list.Count);
            Assert.Equal(testSequence.OrderByDescending(n => n), list.ToList().OrderByDescending(n => n));
            Assert.Equal(testSequence.ToArray().OrderByDescending(n => n), list.ToArray().OrderByDescending(n => n));

            var random = new Random().Next();
            Assert.Equal(testSequence.Where(n => n > random).OrderBy(n => n).ToArray(), list.Find(n => n > random).OrderBy(n => n).ToArray());

            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void List_Simple_Find_Test()
        {
            var testSequence = ProgressiveSequence.ToList();
            var list = new SinglyLinkedList<int> { testSequence };

            Assert.Equal(10, list.Count);
            var filter = list.Find(x => x > 5).ToList();
            Assert.Equal(5, filter.Count);
            Assert.All(filter, n => Assert.True(n > 5));
        }

        [Fact]
        public void List_Simple_Remove_Test()
        {
            var testSequence = ProgressiveSequence.ToList();
            var list = new SinglyLinkedList<int> { testSequence };

            Assert.Equal(10, list.Count);
            Assert.True(list.Remove(10)); // Remove first element - Head
            Assert.Equal(9, list.Count);
            Assert.True(list.Remove(1)); // Remove last element - Tail
            Assert.Equal(8, list.Count);
            Assert.True(list.Remove(6)); // Remove one of the inner node
            Assert.Equal(7, list.Count);
            Assert.Equal(new int[] { 2, 3, 4, 5, 7, 8, 9 }, list.ToArray().OrderBy(x => x));
        }

        [Fact]
        public void List_Condition_Remove_Test()
        {
            var testSequence = ProgressiveSequence.ToList();
            var list = new SinglyLinkedList<int> { testSequence };

            var deleted = list.Remove(x => x > 3 && x < 8);
            Assert.Equal(4, deleted);
            Assert.Equal(6, list.Count);
            Assert.Equal(new int[] { 1, 2, 3, 8, 9, 10 }, list.ToArray().OrderBy(x => x));
        }

        [Fact]
        public void List_Contains_Test()
        {
            var testSequence = ProgressiveSequence.ToList();
            var list = new SinglyLinkedList<int> { testSequence };

            Assert.True(list.Contains(6));
            var deleted = list.Remove(x => x > 3 && x < 8);
            Assert.False(list.Contains(5));
        }

        [Fact]
        public void List_Get_Test()
        {
            var testSequence = ProgressiveSequence.ToList();
            var list = new SinglyLinkedList<int> { testSequence };

            Assert.Equal(4, list[6]);
            Assert.Equal(1, list[9]);
            Assert.Equal(10, list[0]);
            Assert.Throws<IndexOutOfRangeException>(() => list[-2]);
            Assert.Throws<IndexOutOfRangeException>(() => list[list.Count]);
            Assert.Throws<IndexOutOfRangeException>(() => list[list.Count + 3]);
        }

        [Fact]
        public void List_Add_Test()
        {
            var random = new Random();
            var list = new SinglyLinkedList<int>(2);

            Assert.Equal(1, list.Count);
            Assert.Equal(2, list[0]);

            list.Add(random.Next(50));
            list.Add(random.Next(50));

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void List_Stress_Test()
        {
            var testSequence = RandomSequence5K.ToList();
            var list = new SinglyLinkedList<int>();
            list.Add(testSequence);

            // this asserts will force to traverse all the list element
            Assert.DoesNotContain(101, list);

            var empty = list.Find(x => x > 100);
            Assert.Empty(empty);
        }
    }
}
