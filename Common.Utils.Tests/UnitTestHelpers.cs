using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.Tests
{
    public static class UnitTestHelpers
    {
        private static object[] GenerateNestedRandomArray(int element, int deep, List<int> generatedElement)
        {
            if (deep == 0)
            {
                var numbers = Enumerable.Repeat<object>(new Random().Next(500), element).ToArray();
                generatedElement.AddRange(numbers.Select(n => (int)n));
                return numbers;
            }

            var nested = new List<object>();
            nested.AddRange(GenerateNestedRandomArray(element, --deep, generatedElement));
            return nested.ToArray();
        }

        /// <summary>
        /// Generate an array of object with a random number of nested array
        /// inside (and random "deep")
        /// </summary>
        /// <param name="lenght">the max number of element present in the array</param>
        /// <param name="maxNestedLenght">the max number of element present in each nested array</param>
        /// <param name="maxDeep">the max number of deep of nested element</param>
        /// <returns></returns>
        public static (object[] toBeFlatted, int[] expected) GenerateRandomNestedInput(int lenght, int maxNestedLenght, int maxDeep)
        {
            var random = new Random();
            var list = new List<object>();
            var generatedElement = new List<int>(lenght);

            int addedItem;
            for (int i = 0; i < lenght; i += addedItem)
            {
                addedItem = random.Next(1, Math.Min(maxNestedLenght, lenght - i));
                if (addedItem <= 1)
                {
                    var number = random.Next(500);
                    list.Add(number);
                    generatedElement.Add(number);
                    continue;
                }

                int addedDeep = random.Next(0, maxDeep);
                if (addedDeep <= 1)
                {
                    var numbers = Enumerable.Repeat<object>(random.Next(500), addedItem);
                    list.AddRange(numbers);
                    generatedElement.AddRange(numbers.Select(n => (int)n));
                    continue;
                }

                list.Add(GenerateNestedRandomArray(addedItem, addedDeep, generatedElement));
            }

            return (list.ToArray(), generatedElement.ToArray());
        }
    }
}
