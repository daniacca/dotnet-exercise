using System.Collections.Generic;

namespace Utils.ExtensionMethods.String
{
    public static class GetSubstrings
    {
        public static IEnumerable<string> Substrings(this string str)
        {
            int strLength = str.Length;
            for (int start = 0; start < strLength; start++)
                for (int length = strLength - start; length > 0; length--)
                    yield return str.Substring(start, length);
        }
    }
}
