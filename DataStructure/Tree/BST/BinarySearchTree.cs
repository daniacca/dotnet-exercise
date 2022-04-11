using DataStructure.Tree.Common.Abstract;
using DataStructure.Tree.Common.Interfaces;
using DataStructure.Tree.Common.Types;
using System;

namespace DataStructure.Tree.BST
{
    public class BinarySearchTree<T> : BinaryTree<T>
    {
        #region ctor
        public BinarySearchTree(Func<T, T, int> comparer) : base(comparer)
        { }

        public BinarySearchTree(Func<T, T, int> comparer, T value): base(comparer)
        {
            Root = new BinaryTreeNode<T>(value);
        }
        #endregion

        #region override
        public override void Add(T value)
        {
            if (Root is null)
            {
                Root = new BinaryTreeNode<T>(value);
                return;
            }

            var before = FindBefore(value);
            if (before is null)
                return;

            var newNode = new BinaryTreeNode<T>(value);
            if (Comparer(value, before.Data) < 0)
                before.Left = newNode;
            else
                before.Right = newNode;
        }

        public override void Remove(T value)
        {
            Root = Remove(Root, value);
        }
        #endregion

        #region private method
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
            if (parent is null)
                return parent;

            if (Comparer(value, parent.Data) < 0) 
                parent.Left = Remove(parent.Left, value);
            else if (Comparer(value, parent.Data) > 0) 
                parent.Right = Remove(parent.Right, value);
            else
            {
                if (parent.Left is null)
                    return parent.Right;

                if (parent.Right is null)
                    return parent.Left;

                parent.Data = MinValue(parent.Right);
                parent.Right = Remove(parent.Right, parent.Data);
            }

            return parent;
        }
        #endregion
    }
}
