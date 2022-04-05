using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class PermuteIEnumerableExtensions
    {
        private static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> sequence)
        {
            var c = sequence.Count();
            if (c == 1)
                yield return sequence;
            else
                for (int i = 0; i < c; i++)
                    foreach (var p in Permutations(sequence.Take(i).Concat(sequence.Skip(i + 1))))
                        yield return sequence.Skip(i).Take(1).Concat(p);
        }

        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            return Permutations(sequence);
        }
    }
}
