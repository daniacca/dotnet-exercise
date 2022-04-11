using DataStructure.Tree.Common.Abstract;
using DataStructure.Tree.Common.Interfaces;
using DataStructure.Tree.Common.Types;
using System;

namespace DataStructure.Tree.AVL
{
    public class AVL<T> : BinaryTree<T>
    {
        public AVL(Func<T, T, int> comparer) : base(comparer)
        { }
        
        public override void Add(T data)
        {
            var newNode = new BinaryTreeNode<T>(data);

            if (Root is null)
            {
                Root = newNode;
                return;
            }
            
            Root = Insert(Root, newNode);
        }

        public override void Remove(T target)
        {
            Root = Delete(Root, target);
        }

        private IBinaryTreeNode<T> Insert(IBinaryTreeNode<T> current, IBinaryTreeNode<T> toInsert)
        {
            if (current is null)
                return toInsert;

            if (Comparer(toInsert.Data, current.Data) == 0)
                return current;

            if (Comparer(toInsert.Data, current.Data) < 0)
                current.Left = Insert(current.Left, toInsert);
            else if (Comparer(toInsert.Data, current.Data) > 0)
                current.Right = Insert(current.Right, toInsert);
            
            current = BalanceTree(current);
            return current;
        }

        private IBinaryTreeNode<T> Delete(IBinaryTreeNode<T> current, T target)
        {
            IBinaryTreeNode<T> parent;
            if (current == null)
                return null;
            
            if (Comparer(target, current.Data) < 0)
            {
                current.Left = Delete(current.Left, target);
                if (BalanceFactor(current) == -2)
                {
                    if (BalanceFactor(current.Right) <= 0)
                        current = RotateRR(current);
                    else
                        current = RotateRL(current);
                }
            }
            else if (Comparer(target, current.Data) > 0)
            {
                current.Right = Delete(current.Right, target);
                if (BalanceFactor(current) == 2)
                {
                    if (BalanceFactor(current.Left) >= 0)
                        current = RotateLL(current);
                    else
                        current = RotateLR(current);
                }
            }
            else
            {
                if (current.Right != null)
                {
                    parent = current.Right;
                    while (parent.Left != null)
                        parent = parent.Left;
                    
                    current.Data = parent.Data;
                    current.Right = Delete(current.Right, parent.Data);
                    if (BalanceFactor(current) == 2)
                    {
                        if (BalanceFactor(current.Left) >= 0)
                            current = RotateLL(current);
                        else
                            current = RotateLR(current);
                    }
                }
                else
                    return current.Left;
            }
            
            return current;
        }

        private IBinaryTreeNode<T> BalanceTree(IBinaryTreeNode<T> current)
        {
            int b_factor = BalanceFactor(current);

            if (b_factor > 1)
            {
                if (BalanceFactor(current.Left) > 0)
                    current = RotateLL(current);
                else
                    current = RotateLR(current);
            }
            else if (b_factor < -1)
            {
                if (BalanceFactor(current.Right) > 0)
                    current = RotateRL(current);
                else
                    current = RotateRR(current);
            }

            return current;
        }

        private int BalanceFactor(IBinaryTreeNode<T> current)
        {
            return GetDepth(current.Left) - GetDepth(current.Right);
        }

        private IBinaryTreeNode<T> RotateRR(IBinaryTreeNode<T> parent)
        {
            var pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        private IBinaryTreeNode<T> RotateLL(IBinaryTreeNode<T> parent)
        {
            var pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        private IBinaryTreeNode<T> RotateLR(IBinaryTreeNode<T> parent)
        {
            var pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private IBinaryTreeNode<T> RotateRL(IBinaryTreeNode<T> parent)
        {
            var pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
