using System;
using System.Collections.Generic;

namespace BalderHash.Extensions
{
    internal static class StringArrayExtensions
    {
        public static int IndexOf(this IReadOnlyList<string> haystack, ReadOnlySpan<char> needle)
        {
            for (var i = 0; i < haystack.Count; i++)
                if (haystack[i].AsSpan().SequenceEqual(needle))
                    return i;

            return -1;
        }
    }
}