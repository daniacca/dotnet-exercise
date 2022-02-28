using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExtensionMethods
{
    public static class GetSubArraysExtension
    {
        private static IEnumerable<IEnumerable<T>> GetSubArrays<T>(List<T> list, int currIndex)
        {
            if (currIndex == list.Count)
                yield return Enumerable.Empty<T>();

            var result = new List<T>(list.Count - currIndex);
            for (int i = currIndex; i < list.Count; i++)
            {
                result.Add(list[i]);
                yield return new List<T>(result);
            }
        }

        /// <summary>
        /// Returns an iterator over every possibily combinations of
        /// subarrays obtained from 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SubArrays<T>(this IEnumerable<T> sequence)
        {
            var list = sequence.ToList();
            for (int index = 0; index < list.Count; index++)
                foreach (var subArray in GetSubArrays(list, index))
                    yield return subArray;
        }
    }
}
