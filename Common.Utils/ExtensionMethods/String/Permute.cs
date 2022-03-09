using System.Collections.Generic;
using Utils.ExtensionMethods.IEnumerable;

namespace Utils.ExtensionMethods.String
{
    public static class PermuteStringExtension
    {
        public static IEnumerable<string> Permute(this string str)
        {
            IEnumerable<string> permutations(IEnumerable<char> source)
            {
                foreach (var perm in source.Permute())
                    yield return string.Join("", perm);
            }

            return permutations(str.ToCharArray());
        }
    }
}
