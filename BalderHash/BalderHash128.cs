using System;

namespace BalderHash
{
    public readonly struct BalderHash128
    {
        public ulong ValueLo { get; }
        public ulong ValueHi { get; }

        public BalderHash128(ulong lo, ulong hi)
        {
            ValueLo = lo;
            ValueHi = hi;
        }

        public static BalderHash128? Parse(string str)
        {
            return Parse(str.AsSpan());
        }

        public static BalderHash128? Parse(ReadOnlySpan<char> str)
        {
            // Check format
            if (str.Length != 55 || str[27] != '-')
                return null;

            // Parse the 2 halves
            var hi = BalderHash64.Parse(str.Slice(0, 27));
            var lo = BalderHash64.Parse(str.Slice(28));

            // Check that they were valid
            if (!lo.HasValue || !hi.HasValue)
                return null;

            return new BalderHash128(lo.Value.Value, hi.Value.Value);
        }

        public override string ToString()
        {
            Span<char> span = stackalloc char[55];
            ToSpan(span);
            return span.ToString();
        }

        public void ToSpan(Span<char> output)
        {
            if (output.Length != 55)
                throw new ArgumentException("Output span must be exactly Length=55", nameof(output));

            new BalderHash64(ValueHi).ToSpan(output.Slice(0, 27));
            output[27] = '-';
            new BalderHash64(ValueLo).ToSpan(output.Slice(28, 27));
        }
    }
}
