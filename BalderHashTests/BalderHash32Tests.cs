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
        public void ToSpanInvalid()
        {
#pragma warning disable MSTEST0051
            Assert.Throws<ArgumentException>(() =>
#pragma warning restore MSTEST0051
            {
                Span<char> span = stackalloc char[11];
                new BalderHash32(1234).ToSpan(span);
            });
        }

        [TestMethod]
        public void KnownValues()
        {
            // A set of known values. It's critical that these **DO NOT CHANGE** as the library is developed.

            var values = new[]
            {
                "pagmyr-hapnex", "dacbyr-satbex", "sabned-boldex", "pagmet-novwes", "pagpyx-pagnub", "minlud-mignec", "motpun-foltyc", "mattyr-famtyc",
                "somwyn-dalryc", "torseg-bolmed", "ropdus-nibter", "satnul-filwes", "tipteg-larpub", "socmep-finhes", "fodneb-morwyn", "tipweb-sitfep",
                "magluc-dovbep", "tomsyt-sitreg", "raprud-tocdem", "lonreg-somret", "tadlyn-borled", "tidret-narrus", "sonlys-dasdyr", "socrun-sabpub",
                "nillyd-simlux", "naphep-parlyr", "ricmer-sollus", "palrut-sidwyc", "ponhep-rablex", "fosfet-nibfep", "sogset-namned", "doldyr-tarryd",
                "novsut-libpub", "racmep-lopdel", "nalnep-lodrud", "lisnys-ricluc", "hilpyl-labsyr", "wicbyn-morfep", "falweg-fotder", "sitlyn-mochec",
                "dosmev-mosryx", "firlec-lomnum", "balrut-wicber", "libwes-hatnys", "wacmyl-mirsyx", "palsed-sonmep", "sicdex-divpex", "nodbyt-palmec",
                "mosrep-noches", "binfer-migrex", "maldec-dolrun", "labluc-bidryp", "mattyv-lodrud", "raddyl-dindex", "habryg-masbel", "nodwet-dilryn",
                "sonsyp-nidped", "tamlys-tonryg", "sopmel-marduc", "lisryg-pilbes", "motsun-pagwex", "lombyl-wanpen", "tidser-ticfet", "foprul-lignet",
                "rosmyr-todwyx", "ralnep-dantuc", "diblug-sorsut", "soctep-bintul", "podpur-fipdeb", "falbel-nibrev", "tirbyr-nistep", "datbyr-baclex",
                "sorsyp-hinlur", "sitdyt-fiprud", "tansug-nidsub", "bicfed-dovfep", "bacset-dabnys", "libmug-rocpyl", "binrup-mittyr", "satder-wordut",
                "barmud-foldex", "pitpur-taltyp", "dappen-tamdet", "radlug-follug", "ridnyd-rolred", "modnub-siptyp", "bismul-silnus", "libsyp-livsyp",
                "hadfus-roptyp", "fidfyl-sipsub", "bicrud-hilsyn", "middeb-lisfel", "tapnyd-lodlyt", "botbyt-listun", "pattuc-ribdyt", "lanrem-daptep",
                "laglug-rismyn", "rillev-massel", "litmud-patdul", "rithul-mocdyr", "patfeb-lisnul", "parsym-habdex", "sogdun-hocref", "sovsym-locrec",
                "tacsut-hasned", "bidned-famtyr", "tarsud-marper", "navlev-lorlep", "matsec-magmur", "timsut-daswyt", "sanduc-motbyl", "libdux-rapteg",
                "livdut-hadrym", "dopwyn-monmul", "lavdut-bacnec", "pilwen-sornet", "wistex-lontec", "parsec-malwet", "lidrel-nidsyp", "nibmyl-rigmex",
                "sattyd-pidbyl", "wintud-poctus", "wolber-pagsyp", "namdyl-tanbet", "sogmes-sonreg", "masmex-nilmes", "dinbyr-sogsem", "timryp-siltun"

            };

            var r = new Random(346234);

            for (var i = 0; i < 128; i++)
                Assert.AreEqual(values[i], new BalderHash32(unchecked((uint)r.Next())).ToString());
        }

        [TestMethod]
        public void Roundtrip()
        {
            var rng = new Random(2358972);

            Span<char> buffer = stackalloc char[13];

            for (var i = 0; i < 10_000_000; i++)
            {
                var a = unchecked((uint)rng.Next());

                a.BalderHash(buffer);
                var b = BalderHash32.Parse(buffer);

                Assert.IsTrue(b.HasValue);
                Assert.AreEqual(a, b.Value.Value);
            }
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
