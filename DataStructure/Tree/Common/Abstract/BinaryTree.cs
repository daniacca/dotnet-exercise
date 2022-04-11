using DataStructure.Tree.Common.Interfaces;
using DataStructure.Tree.Common.Types;
using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Common.Abstract
{
    public abstract class BinaryTree<T> : IBinaryTree<T>
    {
        protected IBinaryTreeNode<T> Root { get; set; }

        protected Func<T, T, int> Comparer { get; }

        public T Min => MinValue(Root);

        public T Max => MaxValue(Root);

        public int Depth => GetDepth(Root);

        protected BinaryTree(Func<T, T, int> comparer)
        {
            Comparer = comparer;
        }

        #region abstract
        public abstract void Add(T value);

        public abstract void Remove(T value);
        #endregion

        #region public method
        public bool Contains(T value)
        {
            bool Loop(IBinaryTreeNode<T> node)
            {
                if (node is null)
                    return false;
                else if (Comparer(value, node.Data) == 0)
                    return true;
                else if (Comparer(value, node.Data) < 0)
                    return Loop(node.Left);
                else
                    return Loop(node.Right);
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

        public IEnumerable<T> Flatten(TraverseOrder order = TraverseOrder.InOrder)
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

        #region protected method
        protected static int GetDepth(IBinaryTreeNode<T> parent)
        {
            return parent == null ? 0 : Math.Max(GetDepth(parent.Left), GetDepth(parent.Right)) + 1;
        }

        protected static T MinValue(IBinaryTreeNode<T> node)
        {
            if (node is null)
                return default;

            while (node.Left is not null)
                node = node.Left;

            return node.Data;
        }

        protected static T MaxValue(IBinaryTreeNode<T> node)
        {
            if (node is null)
                return default;

            while (node.Right is not null)
                node = node.Right;

            return node.Data;
        }
        #endregion
    }
}
