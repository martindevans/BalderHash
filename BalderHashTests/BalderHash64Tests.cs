using System;
using BalderHash;
using BalderHash.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalderHashTests
{
    [TestClass]
    public class BalderHash64Tests
    {
        [TestMethod]
        public void ParseInvalid()
        {
            Assert.IsNull(BalderHash64.Parse("aaaryc-racnes"));
            Assert.IsNull(BalderHash64.Parse("molaaa-racnes"));
            Assert.IsNull(BalderHash64.Parse("molryc-aaanes"));
            Assert.IsNull(BalderHash64.Parse("molryc-racaaa"));
            Assert.IsNull(BalderHash64.Parse("molrycracten"));
            Assert.IsNull(BalderHash64.Parse("hello"));
            Assert.IsNull(BalderHash64.Parse("hello-world"));
            Assert.IsNull(BalderHash64.Parse("molryc-world"));
        }

        [TestMethod]
        public void Roundtrip()
        {
            const ulong a = 232342389035467834;
            var str = a.BalderHash();
            Console.WriteLine(str);
            var b = BalderHash64.Parse(str);
            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void AllZero()
        {
            const ulong a = 0;
            var str = a.BalderHash();
            Console.WriteLine(str);
            var b = BalderHash64.Parse(str);
            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void Parse()
        {
            var str = "solser-datwed-widmut-dabten";
            var fid = BalderHash64.Parse(str);

            Assert.IsTrue(fid.HasValue);
            Assert.AreEqual(str, fid.ToString());

            Console.WriteLine(str);
            Console.WriteLine(fid.Value.Value);
            Console.WriteLine(fid.ToString());
        }

        [TestMethod]
        public void First100()
        {
            for (uint i = 0; i < 100; i++)
            {
                Console.WriteLine(new BalderHash64(i).ToString());
            }
        }
    }
}
