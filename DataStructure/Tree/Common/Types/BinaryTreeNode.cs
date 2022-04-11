using DataStructure.Tree.Common.Interfaces;

namespace DataStructure.Tree.Common.Types
{
    internal class BinaryTreeNode<T> : IBinaryTreeNode<T>
    {
        public T Data { get; set; } = default;

        public IBinaryTreeNode<T> Left { get; set; } = null;

        public IBinaryTreeNode<T> Right { get; set; } = null;

        public BinaryTreeNode() { }

        public BinaryTreeNode(T data, IBinaryTreeNode<T> left = null, IBinaryTreeNode<T> right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
}
