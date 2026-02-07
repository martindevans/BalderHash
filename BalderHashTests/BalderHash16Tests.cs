using System;
using System.Collections.Generic;
using System.Linq;
using BalderHash;
using BalderHash.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalderHashTests
{
    [TestClass]
    public class BalderHash16Tests
    {
        [TestMethod]
        public void ParseInvalid()
        {
            Assert.IsNull(BalderHash16.Parse("aaaryc"));
            Assert.IsNull(BalderHash16.Parse("molaaa"));
            Assert.IsNull(BalderHash16.Parse("molryc-zodzod"));
            Assert.IsNull(BalderHash16.Parse("molrycracten"));
            Assert.IsNull(BalderHash16.Parse("hello"));
            Assert.IsNull(BalderHash16.Parse("hello"));
            Assert.IsNull(BalderHash16.Parse("world"));
            Assert.IsNull(BalderHash16.Parse("t4mwex"));
        }

        [TestMethod]
        public void ToSpanInvalid()
        {
#pragma warning disable MSTEST0051
            Assert.Throws<ArgumentException>(() =>
#pragma warning restore MSTEST0051
            {
                Span<char> span = stackalloc char[11];
                new BalderHash16(1234).ToSpan(span);
            });
        }

        [TestMethod]
        public void KnownValues()
        {
            // A set of known values. It's critical that these **DO NOT CHANGE** as the library is developed.

            var values = new[]
            {
                "mogfun", "tamwex", "dopmep", "tinlex", "lagdyr", "todfer", "dolsyp", "ritput",
                "samryc", "ribter", "sicsub", "witbet", "dopfeb", "maltyr", "loplen", "lorsep",
                "sivdel", "sicfyn", "sarwep", "dosnyd", "macpun", "bintuc", "bantug", "ranseb",
                "tocwes", "walteb", "fotsyx", "bonbes", "taplud", "ramrel", "rintex", "dorsul",
                "happer", "patnex", "poddyn", "doltyn", "macryp", "difset", "ravneb", "possef",
                "roltex", "bidwyx", "ligryn", "nibpes", "windyl", "riptep", "holfep", "pastyn",
                "borsed", "happeg", "lomryn", "waldeb", "nostul", "billen", "pacbyl", "habput",
                "liswet", "hinrux", "sibpec", "lavped", "wacref", "filwyd", "dinsyp", "fitlyt",
                "namhet", "maptyv", "filrud", "sonrul", "dotnex", "sogtud", "timdus", "picdyn",
                "samsym", "tompen", "nidrup", "latsel", "mitsem", "marlyt", "pacdeg", "napwes",
                "bonmur", "fadpec", "nodsup", "banrup", "macdeg", "navrup", "tanhet", "dastex",
                "rinryl", "mopfen", "ligted", "sitnum", "fonhut", "batrup", "tilmug", "modpur",
                "filpes", "patdus", "bossel", "samreg", "mosrel", "pocres", "dapsyr", "waldeb",
                "dapwes", "liglys", "taldyt", "wiclun", "tarrut", "rilpun", "midper", "nopryc",
                "socder", "fidmur", "nophex", "ritnev", "balsep", "hacryl", "salsyt", "lodmes",
                "daslut", "bitnum", "parmer", "bilpun", "ragmep", "pospes", "midmeb", "bidsev"
            };

            var r = new Random(56346);

            var hashes = new List<string>();

            for (var i = 0; i < 128; i++)
            {
                var v = new BalderHash16(unchecked((ushort)r.Next(0, ushort.MaxValue)));
                hashes.Add(v.ToString());

                Assert.AreEqual(values[i], v.ToString());
            }

            Console.WriteLine(string.Join(", ", hashes.Select(a => $"\"{a}\"")));
        }

        [TestMethod]
        public void Roundtrip()
        {
            Span<char> buffer = stackalloc char[6];

            for (var i = 0; i <= ushort.MaxValue; i++)
            {
                ((ushort)i).BalderHash(buffer);
                var str = buffer.ToString();

                Console.WriteLine(str);
                var b = BalderHash16.Parse(buffer);

                Assert.IsNotNull(b);
                Assert.AreEqual(i, b.Value.Value);
            }
        }

        [TestMethod]
        public void AllZero()
        {
            const ushort a = 0;
            var str = a.BalderHash();

            Console.WriteLine(str);

            var b = BalderHash16.Parse(str);

            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void Parse()
        {
            var str = "lomryc";
            var fid = BalderHash16.Parse(str);

            Assert.IsTrue(fid.HasValue);
            Assert.AreEqual(str, fid.ToString());

            Console.WriteLine(str);
            Console.WriteLine(fid.Value.Value);
            Console.WriteLine(fid.ToString());
        }

        [TestMethod]
        public void First128()
        {
            for (ushort i = 0; i < 128; i++)
            {
                Console.Write($"\"{new BalderHash16(i)}\", ");
            }
        }
    }
}
