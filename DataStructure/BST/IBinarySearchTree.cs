using System;
using System.Collections.Generic;

namespace DataStructure.BST
{
    public enum TraverseOrder
    {
        InOrder,
        PreOrder,
        PostOrder
    }

    public interface ITree<T>
    {
        IBinaryTreeNode<T> Root { get; }

        int Depth { get; }
    }

    public interface IBinarySearchTree<T> : ITree<T>
    {
        T Min { get; }

        T Max { get; }
        
        bool Add(T value);
        
        void Add(IEnumerable<T> values);
        
        void Remove(T value);
        
        IBinaryTreeNode<T> Find(T value);
        
        void Traverse(Action<T> action, TraverseOrder order = TraverseOrder.InOrder);
        
        IEnumerable<T> Traverse(TraverseOrder order = TraverseOrder.InOrder);

        void Clear();
    }
}
