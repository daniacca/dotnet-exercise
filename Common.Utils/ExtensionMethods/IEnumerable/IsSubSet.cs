using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class IsSubSetExtensions
    {
        private static bool? SequenceCheck<T>(IEnumerable<T> sequence, IEnumerable<T> superSet)
        {
            if (!sequence.Any() && !superSet.Any())
                return true;

            if (sequence.Count() > superSet.Count())
                return false;

            return null;
        }

        /// <summary>
        /// Returns true if sequence is a subset of superSet param, false otherwise
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="superSet"></param>
        /// <returns></returns>
        public static bool IsSubset<T>(this IEnumerable<T> sequence, IEnumerable<T> superSet)
        {
            var check = SequenceCheck(sequence, superSet);
            if (check is not null)
                return check.Value;

            return new HashSet<T>(sequence).IsSubsetOf(new HashSet<T>(superSet));
        }

        /// <summary>
        /// Returns true if sequence is a subset of superSet param, false otherwise
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="superSet"></param>
        /// <returns></returns>
        public static bool IsProperSubset<T>(this IEnumerable<T> sequence, IEnumerable<T> superSet)
        {
            var check = SequenceCheck(sequence, superSet);
            if (check is not null)
                return check.Value;

            return new HashSet<T>(sequence).IsProperSubsetOf(new HashSet<T>(superSet));
        }
    }
}
