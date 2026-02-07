using System;
using BalderHash.Extensions;
using static BalderHash.Data;

namespace BalderHash
{
    public readonly struct BalderHash8
    {
        public byte Value { get; }

        public BalderHash8(byte value)
        {
            Value = value;
        }

        public static BalderHash8? Parse(string str)
        {
            return Parse(str.AsSpan());
        }

        public static BalderHash8? Parse(ReadOnlySpan<char> str)
        {
            if (str.Length != 3 || !str.IsAsciiLowercaseLetters())
                return null;

            var a = str.Slice(0, 3);

            var an = GetSuffixIndex(a);
            if (an < 0)
                return null;

            return new BalderHash8(checked((byte)an));
        }

        public override string ToString()
        {
            Span<char> span = stackalloc char[3];
            ToSpan(span);
            return span.ToString();
        }

        public void ToSpan(Span<char> output)
        {
            if (output.Length != 3)
                throw new ArgumentException("Output span must be exactly Length=3", nameof(output));

            GetSuffix(Value).AsSpan().CopyTo(output);
        }
    }
}