using System;
using System.Collections.Generic;

namespace Utils.ExtensionMethods.IEnumerable
{
    public static class FlattenExtension
    {
        private static bool IsIterable(Type type) => type.IsArray || typeof(IEnumerable<>).IsAssignableTo(type);

        private static void Flatten<T>(IEnumerable<object> toBeFlatten, List<T> accumulator)
        {
            foreach (var obj in toBeFlatten)
            {
                if (obj is T[] objArray) accumulator.AddRange(objArray);
                else if (obj is T item) accumulator.Add(item);
                else if (IsIterable(obj.GetType()))
                    Flatten((object[])obj, accumulator);
            }
        }

        /// <summary>
        /// Flatten a generic IEnumerable of object to a single array of element
        /// </summary>
        /// <typeparam name="TOut">the type of element to be flatted</typeparam>
        /// <param name="sequence">the nested array to be flatted</param>
        /// <returns>a flatted array of type <typeparamref name="TOut"/></returns>
        public static IEnumerable<TOut> Flatten<TOut>(this IEnumerable<object> sequence)
        {
            var flatted = new List<TOut>();
            Flatten(sequence, flatted);
            return flatted;
        }
    }
}
