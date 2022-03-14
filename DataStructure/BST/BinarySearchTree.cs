using System;
using System.Collections.Generic;

namespace DataStructure.BST
{
    public class BinarySearchTree<T> : IBinarySearchTree<T>
    {
        public IBinaryTreeNode<T> Root { get; private set; } = null;

        public T Min => MinValue(Root);

        public T Max => MaxValue(Root);

        public int Depth => GetTreeDepth(Root);

        private Func<T, T, int> Comparer { get; }

        #region ctor
        public BinarySearchTree(Func<T, T, int> comparer)
        {
            Comparer = comparer;
        }

        public BinarySearchTree(Func<T, T, int> comparer, T value)
        {
            Comparer = comparer;
            Root = new BinaryTreeNode<T>(value);
        }

        public BinarySearchTree(Func<T, T, int> comparer, IBinaryTreeNode<T> root)
        {
            Comparer = comparer;
            Root = root;
        }
        #endregion

        #region Public method
        public bool Add(T value)
        {
            if (Root is null)
            {
                Root = new BinaryTreeNode<T>(value);
                return true;
            }

            var before = FindBefore(value);
            if (before is null) return false;

            var newNode = new BinaryTreeNode<T>(value);
            if (Comparer(value, before.Data) < 0)
                before.Left = newNode;
            else
                before.Right = newNode;

            return true;
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        public void Remove(T value)
        {
            Root = Remove(Root, value);
        }

        public IBinaryTreeNode<T> Find(T value)
        {
            IBinaryTreeNode<T> Loop(IBinaryTreeNode<T> currentNode)
            {
                if (currentNode is null)
                    return null;
                else if (Comparer(value, currentNode.Data) == 0)
                    return currentNode;
                else if (Comparer(value, currentNode.Data) < 0)
                    return Loop(currentNode.Left);
                else
                    return Loop(currentNode.Right);
            }

            return Loop(Root);
        }

        public void Traverse(Action<T> action, TraverseOrder order = TraverseOrder.InOrder)
        {
            void PreOrder(IBinaryTreeNode<T> current)
            {
                if (current is null)
                    return;

                action(current.Data);
                PreOrder(current.Left);
                PreOrder(current.Right);
            }

            void InOrder(IBinaryTreeNode<T> current)
            {
                if (current is null)
                    return;

                InOrder(current.Left);
                action(current.Data);
                InOrder(current.Right);
            }

            void PostOrder(IBinaryTreeNode<T> current)
            {
                if (current is null)
                    return;

                PostOrder(current.Left);
                PostOrder(current.Right);
                action(current.Data);
            }

            if (order is TraverseOrder.InOrder) InOrder(Root);
            else if (order is TraverseOrder.PostOrder) PostOrder(Root);
            else PreOrder(Root);
        }

        public IEnumerable<T> Traverse(TraverseOrder order = TraverseOrder.InOrder)
        {
            IEnumerable<T> PreOrder(IBinaryTreeNode<T> parent)
            {
                if (parent is null)
                    yield break;

                yield return parent.Data;

                foreach (var value in PreOrder(parent.Left))
                    yield return value;

                foreach (var value in PreOrder(parent.Right))
                    yield return value;
            }

            IEnumerable<T> InOrder(IBinaryTreeNode<T> parent)
            {
                if (parent is null)
                    yield break;

                foreach (var value in InOrder(parent.Left))
                    yield return value;

                yield return parent.Data;

                foreach (var value in InOrder(parent.Right))
                    yield return value;
            }

            IEnumerable<T> PostOrder(IBinaryTreeNode<T> parent)
            {
                if (parent is null)
                    yield break;

                foreach (var value in PostOrder(parent.Left))
                    yield return value;

                foreach (var value in PostOrder(parent.Right))
                    yield return value;

                yield return parent.Data;
            }

            return order switch
            {
                TraverseOrder.InOrder => InOrder(Root),
                TraverseOrder.PreOrder => PreOrder(Root),
                TraverseOrder.PostOrder => PostOrder(Root),
                _ => throw new NotImplementedException()
            };
        }

        public void Clear()
        {
            Root = null;
        }
        #endregion

        #region Private method
        private int GetTreeDepth(IBinaryTreeNode<T> parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.Left), GetTreeDepth(parent.Right)) + 1;
        }

        private IBinaryTreeNode<T> FindBefore(T value)
        {
            IBinaryTreeNode<T> before = null, after = Root;

            while (after != null)
            {
                before = after;
                if (Comparer(value, after.Data) < 0)
                    after = after.Left;
                else if (Comparer(value, after.Data) > 0)
                    after = after.Right;
                else
                    return null;
            }

            return before;
        }

        private IBinaryTreeNode<T> Remove(IBinaryTreeNode<T> parent, T value)
        {
            if (parent == null) return parent;

            if (Comparer(value, parent.Data) < 0) parent.Left = Remove(parent.Left, value);
            else if (Comparer(value, parent.Data) > 0) parent.Right = Remove(parent.Right, value);
            else
            {
                if (parent.Left is null)
                    return parent.Right;

                if (parent.Right is null)
                    return parent.Left;

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.Data = MinValue(parent.Right);

                // Delete the inorder successor  
                parent.Right = Remove(parent.Right, parent.Data);
            }

            return parent;
        }

        private static T MinValue(IBinaryTreeNode<T> node)
        {
            if (node is null)
                return default;

            while (node.Left != null)
                node = node.Left;

            return node.Data;
        }

        private static T MaxValue(IBinaryTreeNode<T> node)
        {
            if (node is null)
                return default;

            while (node.Right != null)
                node = node.Right;

            return node.Data;
        }
        #endregion
    }
}
