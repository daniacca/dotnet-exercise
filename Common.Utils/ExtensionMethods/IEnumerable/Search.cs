using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public enum SearchStrategy
    {
        linear,
        jump,
        binary,
        exponential
    }

    public static class SearchExtensions
    {
        private static int LinearSearch<T>(this IEnumerable<T> sequence, T key, Func<T, T, int> comparer)
        {
            var index = -1;
            foreach (T item in sequence)
            {
                index++;
                if (comparer(item, key) == 0)
                    return index;
            }

            return -1;
        }

        private static int JumpSearch<T>(this IEnumerable<T> sequence, T key, Func<T, T, int> comparer)
        {
            var length = sequence.Count();
            int jumpStep = (int)Math.Sqrt(length);

            int start = 0, end = jumpStep;
            while (comparer(sequence.ElementAt(Math.Min(end, length) - 1), key) < 0)
            {
                start = end;
                end += jumpStep;
                if (start >= length)
                    return -1;
            }

            return sequence.Skip(start).Take(end - start).LinearSearch(key, comparer);
        }

        private static int BinarySearch<T>(this IEnumerable<T> sequence, T key, Func<T, T, int> comparer)
        {
            int search(IEnumerable<T> arr, int low, int high)
            {
                if (high >= low)
                {
                    int mid = low + (high - low) / 2;
                    if (comparer(arr.ElementAt(mid), key) == 0)
                        return mid;

                    if (comparer(arr.ElementAt(mid), key) > 0)
                        return search(arr, low, mid - 1);

                    return search(arr, mid + 1, high);
                }

                return -1;
            }

            return search(sequence, 0, sequence.Count() - 1);
        }

        private static int ExponentialSearch<T>(this IEnumerable<T> sequence, T key, Func<T, T, int> comparer)
        {
            if (comparer(sequence.ElementAt(0), key) == 0)
                return 0;

            int i = 1, n = sequence.Count();
            while (i < n && comparer(sequence.ElementAt(i), key) <= 0)
                i *= 2;

            return sequence.Skip(i / 2).Take(Math.Min(i, n - 1) - (i / 2)).BinarySearch(key, comparer);
        }

        public static int Search<T>(this IEnumerable<T> sequence, SearchStrategy strategy, T element, Func<T, T, int> comparer)
        {
            return strategy switch
            {
                SearchStrategy.linear => sequence.LinearSearch(element, comparer),
                SearchStrategy.jump => sequence.JumpSearch(element, comparer),
                SearchStrategy.binary => sequence.BinarySearch(element, comparer),
                SearchStrategy.exponential => sequence.ExponentialSearch(element, comparer),
                _ => throw new NotImplementedException()
            };
        }
    }
}
