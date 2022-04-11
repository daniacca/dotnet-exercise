using DataStructure.Interfaces;

namespace DataStructure.Tree.Common.Interfaces
{
    public interface IBinaryTreeNode<T> : INode<T>
    {
        IBinaryTreeNode<T> Left { get; set; }

        IBinaryTreeNode<T> Right { get; set; }
    }
}
