using DataStructure.Tree.Common.Types;
using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Common.Interfaces
{
    public interface IBinaryTree<T> : ITree<T>
    {
        T Min { get; }

        T Max { get; }

        void Traverse(Action<T> action, TraverseOrder order = TraverseOrder.InOrder);

        IEnumerable<T> Flatten(TraverseOrder order = TraverseOrder.InOrder);
    }
}
