using DataStructure.Tree.Common.Interfaces;

namespace DataStructure.Tree.RBT
{
    public enum Color
    {
        Red,
        Black
    }

    interface IRedBlackTreeNode<T> : IBinaryTreeNode<T>
    {
        Color Color { get; set; }

        new IRedBlackTreeNode<T> Left { get; set; }

        new IRedBlackTreeNode<T> Right { get; set; }

        IRedBlackTreeNode<T> Parent { get; set; }
    }
}
