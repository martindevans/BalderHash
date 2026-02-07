using System;

namespace BalderHash.Extensions
{
    internal static class SpanCharExtensions
    {
        public static bool IsAsciiLowercaseLetters(this ReadOnlySpan<char> chars)
        {
            foreach (var c in chars)
                if (c < 'a' || c > 'z')
                    return false;

            return true;
        }
    }
}