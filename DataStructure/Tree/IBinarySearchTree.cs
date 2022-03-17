using System;
using System.Collections.Generic;

namespace DataStructure.Tree
{
    public enum TraverseOrder
    {
        InOrder,
        PreOrder,
        PostOrder
    }

    public interface ITree<T>
    {
        int Depth { get; }
    }

    public interface IBinarySearchTree<T> : ITree<T>
    {
        T Min { get; }

        T Max { get; }
        
        bool Add(T value);
        
        void Add(IEnumerable<T> values);
        
        void Remove(T value);
        
        bool Contains(T value);
        
        void Traverse(Action<T> action, TraverseOrder order = TraverseOrder.InOrder);
        
        IEnumerable<T> Flatten(TraverseOrder order = TraverseOrder.InOrder);

        void Clear();
    }
}
