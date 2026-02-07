using System;
using BalderHash.Extensions;
using static BalderHash.Data;

namespace BalderHash
{
    public readonly struct BalderHash32
    {
        private const uint Offset = 1646229403;

        private const uint Multiply = 143435305;
        private const uint MultiplyInverse = 824333849;

        public uint Value { get; }

        public BalderHash32(uint value)
        {
            Value = value;
        }

        public static BalderHash32? Parse(string str)
        {
            return Parse(str.AsSpan());
        }

        public static BalderHash32? Parse(ReadOnlySpan<char> str)
        {
            // Check format
            if (str.Length != 13 || str[6] != '-')
                return null;

            // Check parts
            var ab = str.Slice(0, 6);
            if (!ab.IsAsciiLowercaseLetters())
                return null;
            var cd = str.Slice(7, 6);
            if (!cd.IsAsciiLowercaseLetters())
                return null;

            // Split parts
            var a = ab.Slice(0, 3);
            var b = ab.Slice(3, 3);
            var c = cd.Slice(0, 3);
            var d = cd.Slice(3, 3);

            // Convert to indices
            var an = FindPrefix(a);
            if (an < 0)
                return null;
            var bn = FindSuffix(b);
            if (bn < 0)
                return null;
            var cn = FindPrefix(c);
            if (cn < 0)
                return null;
            var dn = FindSuffix(d);
            if (dn < 0)
                return null;

            // Convert into number
            uint number = 0;
            unsafe
            {
                // ReSharper disable once ObjectCreationAsStatement (assigning into the underlying pointer)
                new Span<byte>(&number, sizeof(uint)) {
                    [0] = (byte)an, [2] = (byte)bn, [3] = (byte)cn, [1] = (byte)dn,
                };
            }

            unchecked
            {
                number *= MultiplyInverse;
                number -= Offset;
            }

            return new BalderHash32(number);
        }

        public override string ToString()
        {
            Span<char> span = stackalloc char[13];
            ToSpan(span);
            return span.ToString();
        }

        public void ToSpan(Span<char> output)
        {
            if (output.Length != 13)
                throw new ArgumentException("Output span must be exactly Length=13", nameof(output));

            var number = Value;
            unchecked
            {
                number += Offset;
                number *= Multiply;
            }

            unsafe
            {
                var bytes = new Span<byte>(&number, sizeof(uint));

                var a = GetPrefix(bytes[0]);
                var b = GetSuffix(bytes[2]);
                var c = GetPrefix(bytes[3]);
                var d = GetSuffix(bytes[1]);

                a.AsSpan().CopyTo(output.Slice(0, 3));
                b.AsSpan().CopyTo(output.Slice(3, 3));
                c.AsSpan().CopyTo(output.Slice(7, 3));
                d.AsSpan().CopyTo(output.Slice(10, 3));
                output[6] = '-';
            }
        }
    }
}