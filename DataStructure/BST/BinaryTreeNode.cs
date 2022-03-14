namespace DataStructure.BST
{
    public interface INode<T>
    {
        T Data { get; set; }
    }

    public interface IBinaryTreeNode<T> : INode<T>
    {
        IBinaryTreeNode<T> Left { get; set; }
        IBinaryTreeNode<T> Right { get; set; }
    }
 
    public class BinaryTreeNode<T> : IBinaryTreeNode<T>
    {
        public T Data { get; set; }
        public IBinaryTreeNode<T> Left { get; set; }
        public IBinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode()
        { }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public BinaryTreeNode(T data, IBinaryTreeNode<T> left, IBinaryTreeNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
}
