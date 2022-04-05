using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.ExtensionMethods.String
{
    public static class ReversePolishNotationExtension
    {
        private static IEnumerable<object> Tokenize(string s)
        {
            return s.Split(" ").Select<string, object>(c => 
            {
                if (int.TryParse(c, out int i))
                    return i;

                if (double.TryParse(c, out double d))
                    return d;

                return c;
            });
        }

        private static Func<double, double, double> OperationFactory(object op)
        {
            return op switch
            {
                "+" => (a, b) => a + b,
                "-" => (a, b) => a - b,
                "*" => (a, b) => a * b,
                "/" => (a, b) => a / b,
                _ => null
            };
        }

        public static double SolveExpression(this string str)
        {
            var tokens = Tokenize(str);

            var stack = new Stack<object>();
            foreach (var t in tokens)
            {
                var operation = OperationFactory(t);
                if (operation is null)
                {
                    stack.Push(t);
                    continue;
                }

                var b = (double)stack.Pop();
                var a = (double)stack.Pop();
                stack.Push(operation(a, b));
            }

            return (double)stack.Pop();
        }
    }
}
