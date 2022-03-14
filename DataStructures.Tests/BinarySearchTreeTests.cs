using DataStructure.BST;
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

            Assert.Null(bst.Root);
            Assert.Equal(0, bst.Depth);

            // Add a Root node
            bst.Add(5);

            Assert.NotNull(bst.Root);
            Assert.Equal(5, bst.Root.Data);
            Assert.Null(bst.Root.Left);
            Assert.Null(bst.Root.Right);
            Assert.Equal(1, bst.Depth);

            // Create new tree with value in ctor
            bst = new BinarySearchTree<int>(IntComparer, 15);
            
            Assert.NotNull(bst.Root);
            Assert.Equal(15, bst.Root.Data);
            Assert.Null(bst.Root.Left);
            Assert.Null(bst.Root.Right);
            Assert.Equal(1, bst.Depth);

            // Create new Tree with tree node in ctor
            var root = new BinaryTreeNode<int>(30);
            bst = new BinarySearchTree<int>(IntComparer, root);

            Assert.NotNull(bst.Root);
            Assert.Equal(root, bst.Root);
            Assert.Equal(1, bst.Depth);
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

            var preOrder = bst.Traverse(TraverseOrder.PreOrder).ToArray();
            
            Assert.NotNull(preOrder);
            Assert.Equal(5, preOrder.Length);
            Assert.Equal(new int[] { 5, 4, 1, 8, 9 }, preOrder);

            var postOrder = bst.Traverse(TraverseOrder.PostOrder).ToArray();

            Assert.NotNull(postOrder);
            Assert.Equal(5, preOrder.Length);
            Assert.Equal(new int[] { 1, 4, 9, 8, 5 }, postOrder);

            var inOrder = bst.Traverse(TraverseOrder.InOrder).ToArray();

            Assert.NotNull(inOrder);
            Assert.Equal(5, inOrder.Length);
            Assert.Equal(new int[] { 1, 4, 5, 8, 9 }, inOrder);
        }
    }
}
