using DataStructure.Tree.BST;
using DataStructure.Tree.Common.Types;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class BinarySearchTreeTests
    {
        private static Func<int, int, int> IntComparer => (a, b) => a - b;

        [Fact]
        public void Test_Create_And_Add_Root()
        {
            var bst = new BinarySearchTree<int>(IntComparer);

            Assert.Equal(0, bst.Depth);

            // Add a Root node
            bst.Add(5);
            
            Assert.Equal(1, bst.Depth);
            Assert.True(bst.Contains(5));
            Assert.False(bst.Contains(15));

            // Create new tree with value in ctor
            bst = new BinarySearchTree<int>(IntComparer, 15);
            Assert.Equal(1, bst.Depth);
            Assert.True(bst.Contains(15));
            Assert.False(bst.Contains(5));
        }

        [Fact]
        public void Test_Create_And_Traverse()
        {
            var bst = new BinarySearchTree<int>(IntComparer, 5);
            
            // Add two node on the Left from root
            bst.Add(4);
            bst.Add(1);

            // Add two node on the right from root
            bst.Add(8);
            bst.Add(9);

            Assert.Equal(3, bst.Depth);
            Assert.Equal(1, bst.Min);
            Assert.Equal(9, bst.Max);

            var preOrder = bst.Flatten(TraverseOrder.PreOrder).ToArray();
            
            Assert.NotNull(preOrder);
            Assert.Equal(5, preOrder.Length);
            Assert.Equal(new int[] { 5, 4, 1, 8, 9 }, preOrder);

            var postOrder = bst.Flatten(TraverseOrder.PostOrder).ToArray();

            Assert.NotNull(postOrder);
            Assert.Equal(5, preOrder.Length);
            Assert.Equal(new int[] { 1, 4, 9, 8, 5 }, postOrder);

            var inOrder = bst.Flatten(TraverseOrder.InOrder).ToArray();

            Assert.NotNull(inOrder);
            Assert.Equal(5, inOrder.Length);
            Assert.Equal(new int[] { 1, 4, 5, 8, 9 }, inOrder);
        }

        [Fact]
        public void Test_Add_And_Remove()
        {
            var bst = new BinarySearchTree<int>(IntComparer);

            // add some data to BST
            bst.Add(12);
            bst.Add(24);
            bst.Add(32);
            bst.Add(8);
            bst.Add(6);
            bst.Add(14);
            bst.Add(4);
            bst.Add(1);

            // BST should keep only one item at a time
            bst.Add(1);

            // Check that one item is contained
            Assert.True(bst.Contains(6));

            // Get depth:
            var dept = bst.Depth;

            // Remove it
            bst.Remove(6);

            Assert.False(bst.Contains(6));

            // Remove other items from the tree
            bst.Remove(8);
            bst.Remove(12);
            bst.Remove(14);

            // Check that depth should be changed
            Assert.NotEqual(dept, bst.Depth);

            // Check tree content
            var inOrder = bst.Flatten().ToArray();
            Assert.Equal(new int[] { 1, 4, 24, 32 }, inOrder);
        }
    }
}
