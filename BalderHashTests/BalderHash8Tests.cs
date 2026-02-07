using System;
using System.Collections.Generic;
using System.Linq;
using BalderHash;
using BalderHash.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalderHashTests
{
    [TestClass]
    public class BalderHash8Tests
    {
        [TestMethod]
        public void ParseInvalid()
        {
            Assert.IsNull(BalderHash8.Parse("aaaryc"));
            Assert.IsNull(BalderHash8.Parse("molaaa"));
            Assert.IsNull(BalderHash8.Parse("molryc-zodzod"));
            Assert.IsNull(BalderHash8.Parse("molrycracten"));
            Assert.IsNull(BalderHash8.Parse("hello"));
            Assert.IsNull(BalderHash8.Parse("hello"));
            Assert.IsNull(BalderHash8.Parse("world"));
            Assert.IsNull(BalderHash8.Parse("zad"));
        }

        [TestMethod]
        public void ToSpanInvalid()
        {
#pragma warning disable MSTEST0051
            Assert.Throws<ArgumentException>(() =>
#pragma warning restore MSTEST0051
            {
                Span<char> span = stackalloc char[2];
                new BalderHash8(134).ToSpan(span);
            });
        }

        [TestMethod]
        public void KnownValues()
        {
            // A set of known values. It's critical that these **DO NOT CHANGE** as the library is developed.

            var values = new[]
            {
                "zod", "nec", "bud", "wes", "sev", "per", "sut", "let",
                "ful", "pen", "syt", "dur", "wep", "ser", "wyl", "sun",
                "ryp", "syx", "dyr", "nup", "heb", "peg", "lup", "dep",
                "dys", "put", "lug", "hec", "ryt", "tyv", "syd", "nex",
                "lun", "mep", "lut", "sep", "pes", "del", "sul", "ped",
                "tem", "led", "tul", "met", "wen", "byn", "hex", "feb",
                "pyl", "dul", "het", "mev", "rut", "tyl", "wyd", "tep",
                "bes", "dex", "sef", "wyc", "bur", "der", "nep", "pur",
                "rys", "reb", "den", "nut", "sub", "pet", "rul", "syn",
                "reg", "tyd", "sup", "sem", "wyn", "rec", "meg", "net",
                "sec", "mul", "nym", "tev", "web", "sum", "mut", "nyx",
                "rex", "teb", "fus", "hep", "ben", "mus", "wyx", "sym",
                "sel", "ruc", "dec", "wex", "syr", "wet", "dyl", "myn",
                "mes", "det", "bet", "bel", "tux", "tug", "myr", "pel",
                "syp", "ter", "meb", "set", "dut", "deg", "tex", "sur",
                "fel", "tud", "nux", "rux", "ren", "wyt", "nub", "med",
                "lyt", "dus", "neb", "rum", "tyn", "seg", "lyx", "pun",
                "res", "red", "fun", "rev", "ref", "mec", "ted", "rus",
                "bex", "leb", "dux", "ryn", "num", "pyx", "ryg", "ryx",
                "fep", "tyr", "tus", "tyc", "leg", "nem", "fer", "mer",
                "ten", "lus", "nus", "syl", "tec", "mex", "pub", "rym",
                "tuc", "fyl", "lep", "deb", "ber", "mug", "hut", "tun",
                "byl", "sud", "pem", "dev", "lur", "def", "bus", "bep",
                "run", "mel", "pex", "dyt", "byt", "typ", "lev", "myl",
                "wed", "duc", "fur", "fex", "nul", "luc", "len", "ner",
                "lex", "rup", "ned", "lec", "ryd", "lyd", "fen", "wel",
                "nyd", "hus", "rel", "rud", "nes", "hes", "fet", "des",
                "ret", "dun", "ler", "nyr", "seb", "hul", "ryl", "lud",
                "rem", "lys", "fyn", "wer", "ryc", "sug", "nys", "nyl",
                "lyn", "dyn", "dem", "lux", "fed", "sed", "bec", "mun",
                "lyr", "tes", "mud", "nyt", "byr", "sen", "weg", "fyr",
                "mur", "tel", "rep", "teg", "pec", "nel", "nev", "fes",
            };

            var hashes = new List<string>();

            for (var i = 0; i <= 255; i++)
            {
                var v = new BalderHash8(checked((byte)i));
                hashes.Add(v.ToString());

                Assert.AreEqual(values[i], v.ToString());
            }

            Console.WriteLine(string.Join(", ", hashes.Select(a => $"\"{a}\"")));
        }

        [TestMethod]
        public void Roundtrip()
        {
            for (var i = 0; i <= byte.MaxValue; i++)
            {
                var str = ((byte)i).BalderHash();
                Console.WriteLine(str);
                var b = BalderHash8.Parse(str);

                Assert.IsNotNull(b);
                Assert.AreEqual(i, b.Value.Value);
            }
        }

        [TestMethod]
        public void AllZero()
        {
            const byte a = 0;
            var str = a.BalderHash();

            Console.WriteLine(str);

            var b = BalderHash8.Parse(str);

            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void Parse()
        {
            var str = "ryc";
            var fid = BalderHash8.Parse(str);

            Assert.IsTrue(fid.HasValue);
            Assert.AreEqual(str, fid.ToString());

            Console.WriteLine(str);
            Console.WriteLine(fid.Value.Value);
            Console.WriteLine(fid.ToString());
        }

        [TestMethod]
        public void First128()
        {
            for (byte i = 0; i < 128; i++)
            {
                Console.Write($"\"{new BalderHash8(i)}\", ");
            }
        }
    }
}
