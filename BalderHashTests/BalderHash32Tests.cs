using System;
using BalderHash;
using BalderHash.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalderHashTests
{
    [TestClass]
    public class BalderHash32Tests
    {
        [TestMethod]
        public void ParseInvalid()
        {
            Assert.IsNull(BalderHash32.Parse("aaaryc-racnes"));
            Assert.IsNull(BalderHash32.Parse("molaaa-racnes"));
            Assert.IsNull(BalderHash32.Parse("molryc-aaanes"));
            Assert.IsNull(BalderHash32.Parse("molryc-racaaa"));
            Assert.IsNull(BalderHash32.Parse("molrycracten"));
            Assert.IsNull(BalderHash32.Parse("hello"));
            Assert.IsNull(BalderHash32.Parse("hello-world"));
            Assert.IsNull(BalderHash32.Parse("molryc-world"));
        }

        [TestMethod]
        public void Roundtrip()
        {
            const uint a = 2323423;
            var str = a.BalderHash();
            Console.WriteLine(str);
            var b = BalderHash32.Parse(str);
            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void AllZero()
        {
            const uint a = 0;
            var str = a.BalderHash();
            Console.WriteLine(str);
            var b = BalderHash32.Parse(str);
            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.Value);
        }

        [TestMethod]
        public void Parse()
        {
            var str = "lomryc-racnes";
            var fid = BalderHash32.Parse(str);

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
                Console.WriteLine(new BalderHash32(i).ToString());
            }
        }
    }
}
