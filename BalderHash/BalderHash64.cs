using System;

namespace BalderHash
{
    public readonly struct BalderHash64
    {
        private const ulong Offset = 0xe106c179ac47eead;

        private const ulong Multiply = 3481248731582150605;
        private const ulong MultiplyInverse = 3333;

        public ulong Value { get; }

        public BalderHash64(ulong value)
        {
            Value = value;
        }

        public static BalderHash64? Parse(string str)
        {
            return Parse(str.AsSpan());
        }

        public static BalderHash64? Parse(ReadOnlySpan<char> str)
        {
            // Check format
            if (str.Length != 27 || str[6] != '-' || str[13] != '-' || str[20] != '-')
                return null;

            // Parse the 2 halves
            var abcd = BalderHash32.Parse(str.Slice(0, 13));
            var efgh = BalderHash32.Parse(str.Slice(14, 13));

            // Check that they were valid
            if (!abcd.HasValue || !efgh.HasValue)
                return null;

            // Convert to value
            var value = ((ulong)abcd.Value.Value << 32) | efgh.Value.Value;
            unchecked
            {
                value *= MultiplyInverse;
                value -= Offset;
            }

            return new BalderHash64(value);
        }

        public override string ToString()
        {
            Span<char> span = stackalloc char[27];
            ToSpan(span);
            return span.ToString();
        }

        public void ToSpan(Span<char> output)
        {
            if (output.Length != 27)
                throw new ArgumentException("Output span must be exactly Length=6", nameof(output));

            var number = Value;
            unchecked
            {
                number += Offset;
                number *= Multiply;
            }

            unsafe
            {
                var ints = new Span<uint>(&number, 2);

                var n1 = ints[0];
                var n2 = ints[1];

                var abcd = new BalderHash32(n1);
                var efgh = new BalderHash32(n2);

                efgh.ToSpan(output.Slice(0, 13));
                output[13] = '-';
                abcd.ToSpan(output.Slice(14, 13));
            }
        }
    }
}
