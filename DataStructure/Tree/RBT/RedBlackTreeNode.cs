using DataStructure.Tree.Common.Types;

namespace DataStructure.Tree.RBT
{
    internal class RedBlackTreeNode<T> : BinaryTreeNode<T>, IRedBlackTreeNode<T>
    {
        public Color Color { get; set; }

        public new IRedBlackTreeNode<T> Left { get; set; }

        public new IRedBlackTreeNode<T> Right { get; set; }

        public IRedBlackTreeNode<T> Parent { get; set; } = null;

        public RedBlackTreeNode(T data) : base(data)
        { }

        public RedBlackTreeNode(Color col) : base()
        {
            Color = col;
        }

        public RedBlackTreeNode(T data, Color col) : base(data)
        {
            Color = col;
        }
    }
}
