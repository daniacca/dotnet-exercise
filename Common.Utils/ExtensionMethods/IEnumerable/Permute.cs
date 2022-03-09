using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class PermuteIEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            static IEnumerable<IEnumerable<T>> permutations(IEnumerable<T> source)
            {
                var c = source.Count();
                if (c == 1)
                    yield return source;
                else
                    for (int i = 0; i < c; i++)
                        foreach (var p in permutations(source.Take(i).Concat(source.Skip(i + 1))))
                            yield return source.Skip(i).Take(1).Concat(p);
            }

            return permutations(sequence);
        }
    }
}
