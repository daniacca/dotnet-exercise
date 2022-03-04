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
        private static void swap<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
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

        private static IEnumerable<T> MergeSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            T[] Merge(T[] l, T[] r)
            {
                var left = new Queue<T>(l);
                var right = new Queue<T>(r);

                var merged = new List<T>(l.Length + r.Length);

                // Merge left and right with correct order
                while (left.Count > 0 && right.Count > 0)
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

        private static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            static (T a, T b) Swap(T a, T b) => (b, a);

            IEnumerable<T> Loop(T[] arr, int n)
            {
                if (n == 1) return arr;

                for (int i = 0; i < n - 1; i++)
                {
                    if (comparer(arr[i], arr[i + 1]) > 0)
                    {
                        var (a, b) = Swap(arr[i], arr[i + 1]);
                        arr[i] = a;
                        arr[i + 1] = b;
                    }
                }

                return Loop(arr, n - 1);
            }

            var array = sequence.ToArray();
            return Loop(array, array.Length);
        }

        private static IEnumerable<T> QuickSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
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
                        swap(arr, i, j);
                    }
                }

                swap(arr, i + 1, high);
                return i + 1;
            }

            void quick(T[] arr, int low, int high)
            {
                if(low < high)
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

        private static IEnumerable<T> HeapSort<T>(this IEnumerable<T> sequence, Func<T, T, int> comparer)
        {
            void heapify(T[] arr, int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                // If left child is larger than root
                if (left < n && comparer(arr[left], arr[largest]) > 0)
                    largest = left;

                // If right child is larger than largest so far
                if (right < n && comparer(arr[right], arr[largest]) > 0)
                    largest = right;

                // If largest is not root
                if (largest != i)
                {
                    swap(arr, i, largest);
                    heapify(arr, n, largest);
                }
            }

            void sort(T[] arr, int n)
            {
                // Build heap
                for (int i = n / 2 - 1; i >= 0; i--)
                    heapify(arr, n, i);

                // One by one extract an element from heap
                for (int i = n - 1; i > 0; i--)
                {
                    // Move current root to end
                    swap(arr, 0, i);
                    
                    // call max heapify on the reduced heap
                    heapify(arr, i, 0);
                }
            }

            var array = sequence.ToArray();
            sort(array, array.Length);
            return array;
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> sequence, SortingStrategy strategy, Func<T, T, int> comparer)
        {
            return strategy switch
            {
                SortingStrategy.bubble => sequence.BubbleSort(comparer),
                SortingStrategy.insertion => sequence.InsertionSort(comparer),
                SortingStrategy.merge => sequence.MergeSort(comparer),
                SortingStrategy.quick => sequence.QuickSort(comparer),
                SortingStrategy.heap => sequence.HeapSort(comparer),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
