using DataStructure.Interfaces;


namespace DataStructure.List
{
    public interface IListNode<T> : INode<T>
    {
        IListNode<T> Next { get; set; }
    }

    internal class ListNode<T> : IListNode<T>
    {
        public T Data { get; set; } = default;

        public IListNode<T> Next { get; set; } = null;

        public ListNode()
        { }

        public ListNode(T value)
        {
            Data = value;
        }

        public ListNode(T value, IListNode<T> next)
        {
            Data = value;
            Next = next;
        }
    }
}
