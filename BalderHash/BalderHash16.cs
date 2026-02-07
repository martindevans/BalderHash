using System;
using BalderHash.Extensions;
using static BalderHash.Data;

namespace BalderHash
{
    public readonly struct BalderHash16
    {
        private const ushort Offset = 43346;

        private const ushort Multiply = 14305;
        private const ushort MultiplyInverse = 19489;

        public ushort Value { get; }

        public BalderHash16(ushort value)
        {
            Value = value;
        }

        public static BalderHash16? Parse(string str)
        {
            return Parse(str.AsSpan());
        }

        public static BalderHash16? Parse(ReadOnlySpan<char> str)
        {
            if (str.Length != 6 || !str.IsAsciiLowercaseLetters())
                return null;

            var a = str.Slice(0, 3);
            var b = str.Slice(3, 3);

            var an = GetPrefixIndex(a);
            if (an < 0)
                return null;

            var bn = GetSuffixIndex(b);
            if (bn < 0)
                return null;

            ushort number = 0;
            unsafe
            {
                // ReSharper disable once ObjectCreationAsStatement (assigning into the underlying pointer)
                new Span<byte>(&number, sizeof(ushort))
                {
                    [0] = (byte)an,
                    [1] = (byte)bn,
                };
            }

            unchecked
            {
                number *= MultiplyInverse;
                number -= Offset;
            }

            return new BalderHash16(number);
        }

        public override string ToString()
        {
            Span<char> span = stackalloc char[6];
            ToSpan(span);
            return span.ToString();
        }

        public void ToSpan(Span<char> output)
        {
            if (output.Length != 6)
                throw new ArgumentException("Output span must be exactly Length=6", nameof(output));

            var number = Value;
            unchecked
            {
                number += Offset;
                number *= Multiply;
            }

            unsafe
            {
                var bytes = new Span<byte>(&number, sizeof(ushort));

                var a = GetPrefix(bytes[0]);
                var b = GetSuffix(bytes[1]);

                a.AsSpan().CopyTo(output.Slice(0, 3));
                b.AsSpan().CopyTo(output.Slice(3, 3));
            }
        }
    }
}