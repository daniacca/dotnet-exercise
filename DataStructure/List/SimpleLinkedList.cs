using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.List
{
    public class SimpleLinkedList<T> : IEnumerable<T>
    {
        IListNode<T> Head { get; set; } = null;

        public int Count { get; private set; } = 0;

        public T this[int index] => Get(index);

        public SimpleLinkedList()
        { }

        public SimpleLinkedList(T head)
        {
            Head = new ListNode<T>(head);
            Count = 1;
        }

        public SimpleLinkedList(IEnumerable<T> anotherList)
        {
            Add(anotherList);
        }

        public void Add(T value)
        {
            Head = new ListNode<T>(value, Head);
            Count++;
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        public bool Remove(T value)
        {
            // Check if we have to delete the Head
            if(Head.Data.Equals(value))
            {
                Head = Head.Next;
                Count--;
                return true;
            }    

            // Iterate over the list
            IListNode<T> prev, actual = Head;
            while (actual.Next is not null)
            {
                prev = actual;
                actual = actual.Next;

                if (actual.Data.Equals(value))
                {
                    prev.Next = actual.Next;
                    Count--;
                    return true;
                }
            }

            return false;
        }

        public int Remove(Func<T, bool> condition)
        {
            int deleted = 0;

            if (condition(Head.Data))
            {
                Head = Head.Next;
                deleted++;
            }

            bool hasDeleted;
            do
            {
                hasDeleted = false;
                
                IListNode<T> prev, actual = Head;
                while (actual.Next is not null)
                {
                    prev = actual;
                    actual = actual.Next;

                    if (condition(actual.Data))
                    {
                        prev.Next = actual.Next;
                        hasDeleted = true;
                        break;
                    }
                }

                if (hasDeleted) 
                    deleted++;
            }
            while (hasDeleted);

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

        public T[] ToArray()
        {
            var arr = new T[Count];

            var node = Head;
            for (int i = 0; i < Count; i++)
            {
                arr[i] = node.Data;
                node = node.Next;
            }

            return arr;
        }

        public List<T> ToList()
        {
            var list = new List<T>(Count);
            var node = Head;

            while (node is not null)
            {
                list.Add(node.Data);
                node = node.Next;
            }

            return list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        private IEnumerable<T> Traverse()
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

        private T Get(int i)
        {
            T GetElement(IListNode<T> current, int index)
            {
                if (index == i)
                    return current.Data;

                return GetElement(current.Next, index + 1);
            }

            if (i < 0 || i >= Count)
                throw new IndexOutOfRangeException();

            return GetElement(Head, 0);
        }
    }
}
