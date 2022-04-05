using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public enum SortingStrategy
    {
        bubble,
        insertion,
        merge,
        quick,
        heap
    }

    public static class SortingExtensions
    {
        private static void Swap<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private static IEnumerable<T> Insertion<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            void InsSort(T[] a, int n)
            {
                if (n <= 1) return;

                InsSort(a, n - 1);

                T last = a[n - 1];
                int j = n - 2;
                while (j >= 0 && comparer(a[j], last) > 0)
                {
                    a[j + 1] = a[j];
                    j--;
                }

                a[j + 1] = last;
            }

            var arr = sequence.ToArray();
            InsSort(arr, arr.Length);
            return arr;
        }

        private static IEnumerable<T> Merge<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            T[] Merge(T[] l, T[] r)
            {
                var left = new Queue<T>(l);
                var right = new Queue<T>(r);

                var merged = new List<T>(l.Length + r.Length);
                while (left.Count > 0 && right.Count > 0)
                {
                    if (comparer(left.Peek(), right.Peek()) < 0)
                        merged.Add(left.Dequeue());
                    else
                        merged.Add(right.Dequeue());
                }

                if (left.Any())
                    merged.AddRange(left);

                if (right.Any())
                    merged.AddRange(right);

                return merged.ToArray();
            }

            T[] MergeSort(T[] array)
            {
                if (array.Length < 2)
                    return array;

                int half = array.Length / 2;
                var left = array.Take(half).ToArray();
                var rigth = array.Skip(half).ToArray();
                return Merge(MergeSort(left), MergeSort(rigth));
            }

            var array = sequence.ToArray();
            return MergeSort(array);
        }

        private static IEnumerable<T> Bubble<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            IEnumerable<T> Bubble(T[] arr, int n)
            {
                if (n == 1) return arr;

                for (int i = 0; i < n - 1; i++)
                    if (comparer(arr[i], arr[i + 1]) > 0)
                        Swap(arr, i, i + 1);

                return Bubble(arr, n - 1);
            }

            var array = sequence.ToArray();
            return Bubble(array, array.Length);
        }

        private static IEnumerable<T> Quick<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            int partition(T[] arr, int low, int high)
            {
                var pivot = arr[high];
                var i = low - 1;

                for (var j = low; j <= high - 1; j++)
                {
                    if (comparer(arr[j], pivot) < 0)
                    {
                        i++;
                        Swap(arr, i, j);
                    }
                }

                Swap(arr, i + 1, high);
                return i + 1;
            }

            void quick(T[] arr, int low, int high)
            {
                if (low < high)
                {
                    var pi = partition(arr, low, high);
                    quick(arr, low, pi - 1);
                    quick(arr, pi + 1, high);
                }
            }

            var array = sequence.ToArray();
            quick(array, 0, array.Length - 1);
            return array;
        }

        private static IEnumerable<T> Heap<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            void heapify(T[] arr, int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && comparer(arr[left], arr[largest]) > 0)
                    largest = left;

                if (right < n && comparer(arr[right], arr[largest]) > 0)
                    largest = right;

                if (largest != i)
                {
                    Swap(arr, i, largest);
                    heapify(arr, n, largest);
                }
            }

            void sort(T[] arr, int n)
            {
                for (int i = n / 2 - 1; i >= 0; i--)
                    heapify(arr, n, i);

                for (int i = n - 1; i > 0; i--)
                {
                    Swap(arr, 0, i);
                    heapify(arr, i, 0);
                }
            }

            var array = sequence.ToArray();
            sort(array, array.Length);
            return array;
        }

        /// <summary>
        /// Sort the collection, using the specified strategy and using
        /// the comparer logic 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="strategy">specifies which sorting algorithm to use</param>
        /// <param name="comparer">
        ///     function that gives the logic for comparing two element of the sequence.
        ///     (<typeparamref name="T"/> A, <typeparamref name="T"/> B) should return: 
        ///     1  if A > B,
        ///     0  if A = B,
        ///     -1 if A < B
        /// </param>
        /// <returns>the sorted collection</returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> sequence, SortingStrategy strategy, Func<T, T, int> comparer)
        {
            return strategy switch
            {
                SortingStrategy.bubble => sequence.Bubble(comparer),
                SortingStrategy.insertion => sequence.Insertion(comparer),
                SortingStrategy.merge => sequence.Merge(comparer),
                SortingStrategy.quick => sequence.Quick(comparer),
                SortingStrategy.heap => sequence.Heap(comparer),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
