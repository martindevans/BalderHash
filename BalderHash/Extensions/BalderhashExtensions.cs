using System;

namespace BalderHash.Extensions
{
    public static class BalderhashExtensions
    {
        extension(byte number)
        {
            public string BalderHash()
            {
                return new BalderHash8(number).ToString();
            }

            public void BalderHash(Span<char> output)
            {
                new BalderHash8(number).ToSpan(output);
            }
        }

        extension(ushort number)
        {
            public string BalderHash()
            {
                return new BalderHash16(number).ToString();
            }

            public void BalderHash(Span<char> output)
            {
                new BalderHash16(number).ToSpan(output);
            }
        }

        extension(uint number)
        {
            public string BalderHash()
            {
                return new BalderHash32(number).ToString();
            }

            public void BalderHash(Span<char> output)
            {
                new BalderHash32(number).ToSpan(output);
            }
        }

        extension(ulong number)
        {
            public string BalderHash()
            {
                return new BalderHash64(number).ToString();
            }

            public void BalderHash(Span<char> output)
            {
                new BalderHash64(number).ToSpan(output);
            }
        }
    }
}
