using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.List
{
    public class SinglyLinkedList<T> : IEnumerable<T>, ICollection<T>
    {
        IListNode<T> Head { get; set; } = null;
        
        IListNode<T> Tail => GetNode(Count - 1);

        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public T this[int index] => Get(index);

        public SinglyLinkedList()
        { }

        public SinglyLinkedList(T head)
        {
            Add(head);
        }

        public SinglyLinkedList(IEnumerable<T> anotherList)
        {
            Add(anotherList);
        }

        public void Add(T value)
        {
            Head = new ListNode<T>(value, Head);
            Count++;
        }

        public void AddAtTail(T value)
        {
            Tail.Next = new ListNode<T>(value);
            Count++;
        }

        public void Add(T value, int index)
        {
            if (index < 0 && index > Count)
                throw new IndexOutOfRangeException();

            if (Head is null || index is 0)
            {
                Add(value);
                return;
            }

            if(index == Count)
            {
                AddAtTail(value);
                return;
            }

            var before = GetNode(index - 1);
            before.Next = new ListNode<T>(value, before.Next);
            Count++;
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        public bool Remove(T value)
        {
            if (Head is null)
                return false;

            int deleted = 0;
            while (Head.Data.Equals(value))
            {
                Head = Head.Next;
                deleted++;
            }

            var actual = Head;
            while (actual is not null)
            {
                while (actual.Next is not null && actual.Next.Data.Equals(value))
                {
                    actual.Next = actual.Next?.Next;
                    deleted++;
                }

                actual = actual.Next;
            }

            Count -= deleted;
            return deleted > 0;
        }

        public int Remove(Func<T, bool> condition)
        {
            if (Head is null)
                return 0;

            int deleted = 0;
            while (condition(Head.Data))
            {
                Head = Head.Next;
                deleted++;
            }

            var actual = Head;
            while (actual is not null)
            {
                while (actual.Next is not null && condition(actual.Next.Data))
                {
                    actual.Next = actual.Next?.Next;
                    deleted++;
                }

                actual = actual.Next;
            }

            Count -= deleted;
            return deleted;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            bool Find(IListNode<T> node)
            {
                if (node is null)
                    return false;

                if (node.Data.Equals(item))
                    return true;

                return Find(node.Next);
            }

            return Find(Head);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            IEnumerable<T> Loop(IListNode<T> node)
            {
                if (node is null) 
                    yield break;

                if (predicate(node.Data))
                    yield return node.Data;

                foreach (var next in Loop(node.Next))
                    yield return next;
            }

            return Loop(Head);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var current = Head;
            while (current is not null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public void Reverse()
        {
            IListNode<T> prev = null, current = Head;
            while (current is not null)
            {
                var next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        IEnumerable<T> Traverse()
        {
            var actual = Head;
            while (actual is not null)
            {
                yield return actual.Data;
                actual = actual.Next;
            }

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        T Get(int i)
        {
            return GetNode(i).Data;
        }

        IListNode<T> GetNode(int i)
        {
            IListNode<T> GetElement(IListNode<T> current, int index)
            {
                if (index == i)
                    return current;

                return GetElement(current.Next, index + 1);
            }

            if (i < 0 || i >= Count)
                throw new IndexOutOfRangeException();

            return GetElement(Head, 0);
        }
    }
}
