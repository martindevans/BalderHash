using System;

namespace BalderHash.Extensions
{
    public static class UInt32Extensions
    {
        public static string BalderHash(this uint number)
        {
            return new BalderHash32(number).ToString();
        }

        public static void BalderHash(this uint number, Span<char> output)
        {
            new BalderHash32(number).ToSpan(output);
        }
    }
}
