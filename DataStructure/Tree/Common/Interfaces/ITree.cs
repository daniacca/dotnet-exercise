namespace DataStructure.Tree.Common.Interfaces
{
    public interface ITree<T>
    {
        int Depth { get; }

        void Add(T value);

        void Remove(T value);

        bool Contains(T value);

        void Clear();
    }
}
