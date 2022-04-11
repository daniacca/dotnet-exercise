using DataStructure.Tree.Common.Abstract;
using System;

namespace DataStructure.Tree.RBT
{
    public class RBTree<T> : BinaryTree<T>
    {
        new IRedBlackTreeNode<T> Root { get; set; }

        public RBTree(Func<T, T, int> comp) : base(comp)
        { }

        public override void Add(T value)
        {
            var newItem = new RedBlackTreeNode<T>(value);

            if (Root is null)
            {
                Root = newItem;
                Root.Color = Color.Black;
                return;
            }
            
            IRedBlackTreeNode<T> Y = null, X = Root;            
            while (X != null)
            {
                Y = X;
                if (Comparer(newItem.Data, X.Data) < 0)
                    X = X.Left;
                else
                    X = X.Right;
            }

            newItem.Parent = Y;
            if (Y == null)
                Root = newItem;
            else if (Comparer(newItem.Data, X.Data) < 0)
                Y.Left = newItem;
            else
                Y.Right = newItem;
            
            newItem.Color = Color.Red;
            InsertFixUp(newItem);
        }

        public override void Remove(T key)
        {
            var item = FindNode(key);
            if (item == null)
                return;

            IRedBlackTreeNode<T> Y;
            if (item.Left == null || item.Right == null)
                Y = item;
            else
                Y = TreeSuccessor(item);

            IRedBlackTreeNode<T> X;
            if (Y.Left != null)
                X = Y.Left;
            else
                X = Y.Right;

            if (X != null)
                X.Parent = Y;
            
            if (Y.Parent == null)
                Root = X;
            else if (Y == Y.Parent.Left)
                Y.Parent.Left = X;
            else
                Y.Parent.Left = X;
            
            if (Y != item)
                item.Data = Y.Data;
           
            if (Y.Color == Color.Black)
                DeleteFixUp(X);
        }

        #region private method
        private IRedBlackTreeNode<T> FindNode(T value)
        {
            IRedBlackTreeNode<T> Loop(IRedBlackTreeNode<T> node)
            {
                if (node is null)
                    return null;
                else if (Comparer(value, node.Data) == 0)
                    return node;
                else if (Comparer(value, node.Data) < 0)
                    return Loop(node.Left);
                else
                    return Loop(node.Right);
            }

            return Loop(Root);
        }

        private void LeftRotate(IRedBlackTreeNode<T> X)
        {
            var Y = X.Right;
            X.Right = Y.Left;
            
            if (Y.Left != null)
                Y.Left.Parent = X;
            
            if (Y != null)
                Y.Parent = X.Parent;
            
            if (X.Parent == null)
                Root = Y;
            
            if (X == X.Parent.Left)
                X.Parent.Left = Y;
            else
                X.Parent.Right = Y;
            
            Y.Left = X;
            if (X != null)
                X.Parent = Y;
        }

        private void RightRotate(IRedBlackTreeNode<T> Y)
        {
            var X = Y.Left;
            Y.Left = X.Right;
            if (X.Right != null)
                X.Right.Parent = Y;
            
            if (X != null)
                X.Parent = Y.Parent;
            
            if (Y.Parent == null)
                Root = X;
            
            if (Y == Y.Parent.Right)
                Y.Parent.Right = X;
            
            if (Y == Y.Parent.Left)
                Y.Parent.Left = X;

            X.Right = Y;
            if (Y != null)
                Y.Parent = X;
        }

        private void InsertFixUp(IRedBlackTreeNode<T> item)
        {
            while (item != Root && item.Parent.Color == Color.Red)
            {
                if (item.Parent == item.Parent.Parent.Left)
                {
                    IRedBlackTreeNode<T> Y = item.Parent.Parent.Right;
                    if (Y != null && Y.Color == Color.Red)
                    {
                        item.Parent.Color = Color.Black;
                        Y.Color = Color.Black;
                        item.Parent.Parent.Color = Color.Red;
                        item = item.Parent.Parent;
                    }
                    else
                    {
                        if (item == item.Parent.Right)
                        {
                            item = item.Parent;
                            LeftRotate(item);
                        }

                        item.Parent.Color = Color.Black;
                        item.Parent.Parent.Color = Color.Red;
                        RightRotate(item.Parent.Parent);
                    }
                }
                else
                {
                    IRedBlackTreeNode<T> X = item.Parent.Parent.Left;
                    if (X != null && X.Color == Color.Black)
                    {
                        item.Parent.Color = Color.Red;
                        X.Color = Color.Red;
                        item.Parent.Parent.Color = Color.Black;
                        item = item.Parent.Parent;
                    }
                    else
                    {
                        if (item == item.Parent.Left)
                        {
                            item = item.Parent;
                            RightRotate(item);
                        }
                        
                        item.Parent.Color = Color.Black;
                        item.Parent.Parent.Color = Color.Red;
                        LeftRotate(item.Parent.Parent);
                    }
                }

                Root.Color = Color.Black;
            }
        }

        private void DeleteFixUp(IRedBlackTreeNode<T> X)
        {
            while (X != null && X != Root && X.Color == Color.Black)
            {
                if (X == X.Parent.Left)
                {
                    var W = X.Parent.Right;
                    if (W.Color == Color.Red)
                    {
                        W.Color = Color.Black; //case 1
                        X.Parent.Color = Color.Red; //case 1
                        LeftRotate(X.Parent); //case 1
                        W = X.Parent.Right; //case 1
                    }
                    if (W.Left.Color == Color.Black && W.Right.Color == Color.Black)
                    {
                        W.Color = Color.Red; //case 2
                        X = X.Parent; //case 2
                    }
                    else if (W.Right.Color == Color.Black)
                    {
                        W.Left.Color = Color.Black; //case 3
                        W.Color = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.Parent.Right; //case 3
                    }

                    W.Color = X.Parent.Color; //case 4
                    X.Parent.Color = Color.Black; //case 4
                    W.Right.Color = Color.Black; //case 4
                    LeftRotate(X.Parent); //case 4
                    X = Root; //case 4
                }
                else
                {
                    var W = X.Parent.Left;
                    if (W.Color == Color.Red)
                    {
                        W.Color = Color.Black;
                        X.Parent.Color = Color.Red;
                        RightRotate(X.Parent);
                        W = X.Parent.Left;
                    }
                    if (W.Right.Color == Color.Black && W.Left.Color == Color.Black)
                    {
                        W.Color = Color.Black;
                        X = X.Parent;
                    }
                    else if (W.Left.Color == Color.Black)
                    {
                        W.Right.Color = Color.Black;
                        W.Color = Color.Red;
                        LeftRotate(W);
                        W = X.Parent.Left;
                    }
                    W.Color = X.Parent.Color;
                    X.Parent.Color = Color.Black;
                    W.Left.Color = Color.Black;
                    RightRotate(X.Parent);
                    X = Root;
                }
            }
            if (X != null)
                X.Color = Color.Black;
        }

        private IRedBlackTreeNode<T> Minimum(IRedBlackTreeNode<T> X)
        {
            while (X.Left.Left != null)
                X = X.Left;

            if (X.Left.Right != null)
                X = X.Left.Right;

            return X;
        }

        private IRedBlackTreeNode<T> TreeSuccessor(IRedBlackTreeNode<T> X)
        {
            if (X.Left != null)
                return Minimum(X);
            
            var Y = X.Parent;
            while (Y != null && X == Y.Right)
            {
                X = Y;
                Y = Y.Parent;
            }

            return Y;
        }
        #endregion
    }
}
