using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class IsSubSetExtensions
    {
        private static bool IsSub<T>(HashSet<T> sub, HashSet<T> sup) => sub.IsSubsetOf(sup);

        /// <summary>
        /// Returns true if sequence is a subset of superSet param, false otherwise
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="superSet"></param>
        /// <returns></returns>
        public static bool IsSubSet<T>(this IEnumerable<T> sequence, IEnumerable<T> superSet)
        {
            if (!sequence.Any() && !superSet.Any())
                return true;

            if (sequence.Count() > superSet.Count())
                return false;

            if (sequence.Count() == superSet.Count())
                return sequence.SequenceEqual(superSet);

            return IsSub(new HashSet<T>(sequence), new HashSet<T>(superSet));
        }
    }
}
