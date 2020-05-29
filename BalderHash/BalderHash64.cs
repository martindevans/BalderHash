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
            var parts = str.Split('-');
            if (parts.Length != 4)
                return null;

            var ab = parts[0];
            if (ab.Length != 6)
                return null;
            var cd = parts[1];
            if (cd.Length != 6)
                return null;
            var ef = parts[2];
            if (ef.Length != 6)
                return null;
            var gh = parts[3];
            if (gh.Length != 6)
                return null;

            var abcd = BalderHash32.Parse($"{ab}-{cd}");
            var efgh = BalderHash32.Parse($"{ef}-{gh}");

            if (!abcd.HasValue || !efgh.HasValue)
                return null;

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

                return $"{efgh}-{abcd}";
            }
        }
    }
}
