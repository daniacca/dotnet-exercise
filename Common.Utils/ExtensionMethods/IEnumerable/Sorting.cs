using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class SortingExtensions
    {
        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            void InsSort(T[] a, int n)
            {
                // Base case
                if (n <= 1) return;

                // Sort first n-1 elements
                InsSort(a, n - 1);

                // Insert last element at its correct position in sorted array
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

        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            T[] Merge(T[] l, T[] r)
            {
                var left = new Queue<T>(l);
                var right = new Queue<T>(r);

                var merged = new List<T>(l.Length + r.Length);

                // Merge left and right in right order
                while(left.Count > 0 && right.Count > 0)
                {
                    if (comparer(left.Peek(), right.Peek()) < 0)
                        merged.Add(left.Dequeue());
                    else
                        merged.Add(right.Dequeue());
                }

                // Add any remaning items on left queue
                if (left.Any())
                    merged.AddRange(left);

                // Add any remaining items on right queu
                if (right.Any())
                    merged.AddRange(right);

                return merged.ToArray();
            }

            T[] MergeSort(T[] array)
            {
                if (array.Length < 2)
                    return array;

                int half = array.Length / 2;
                var left = array.Skip(0).Take(half).ToArray();
                var rigth = array.Skip(half).ToArray();
                return Merge(MergeSort(left), MergeSort(rigth));
            }

            var array = sequence.ToArray();
            return MergeSort(array);
        }
    }
}
