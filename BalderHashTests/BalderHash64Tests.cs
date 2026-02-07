using BalderHash;
using BalderHash.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Assert.IsNull(BalderHash64.Parse("midpen-ladbep-namweg-p1lnus"));
            Assert.IsNull(BalderHash64.Parse("m1dpen-ladbep-namweg-pilnus"));
        }

        [TestMethod]
        public void ToSpanInvalid()
        {
#pragma warning disable MSTEST0051
            Assert.Throws<ArgumentException>(() =>
#pragma warning restore MSTEST0051
            {
                Span<char> span = stackalloc char[123];
                new BalderHash64(1234).ToSpan(span);
            });
        }

        [TestMethod]
        public void Roundtrip()
        {
            var rng = new Random(568746);

            Span<char> buffer = stackalloc char[27];

            for (var i = 0; i < 1_000_000; i++)
            {
                var a = unchecked((ulong)rng.NextInt64());

                a.BalderHash(buffer);
                var b = BalderHash64.Parse(buffer);

                Assert.IsTrue(b.HasValue);
                Assert.AreEqual(a, b.Value.Value);
            }
        }

        [TestMethod]
        public void KnownValues()
        {
            // A set of known values. It's critical that these **DO NOT CHANGE** as the library is developed.

            var values = new[]
            {
                "hilluc-ravsyl-ribbel-maldes", "midpen-ladbep-namweg-pilnus", "fadsef-rablyd-boltud-dinmul", "lossyl-lavbet-wolsef-darsun",
                "nilber-bispub-finbyn-dabdet", "fonrym-mismun-bicsyt-lonhul", "rilner-litfur-radrep-rigruc", "mothep-tadsug-magdel-moghus",
                "rigwel-wicwet-motrud-rovnyl", "pocdex-micnut-ridlun-habmep", "doswet-tocdul-narsef-fitmeg", "ponhex-hattyr-rivmev-wiswyc",
                "solrul-taslen-mitryp-pinlux", "nisbep-bidned-pidtyv-dosbyt", "hodmug-tocbus-parrux-baltun", "hanref-hatnul-radwep-hadryt",
                "silsup-magfer-labbyl-nispes", "dorlup-lopwex-hacryc-pildeg", "nalsem-sitmus-dorwyx-lisden", "sapnut-bicnub-paclet-fiplex",
                "ribrux-dibryc-losnyx-lanmev", "wanryp-dopsep-dismep-ridpyx", "lablyd-finmel-hobwyd-ritnyx", "tognyr-ligmet-dalsup-haslug",
                "dilder-tolpyx-hilnyd-tadhut", "ribmun-ragnyr-londux-tobfen", "ransyt-ralbus-hidsyd-haddyt", "tassub-fassen-hosfur-havmel",
                "lapluc-topsem-namwen-lavsec", "dopfel-digrup-tammer-folbur", "milrut-malrec-sitmeb-natpub", "dotfus-passef-mitnyt-ladmun",
                "figdyr-livseb-nimtes-talneb", "nocsyr-fiptul-tobnes-figrem", "lavrud-dibteg-midbet-difser", "torfes-dolres-lopfyl-hanhus",
                "savdyt-normur-ramdec-tarmep", "botwet-socper-bandyt-donwen", "todryp-bartus-sigbur-mirrum", "nomnyl-fipten-macrud-forlux",
                "torhex-molbet-tobdyl-raclec", "nacmus-barhep-donweb-fotmep", "holden-batsyn-batwyc-ragbyl", "nilper-parmed-toltyd-micruc",
                "pacheb-loclut-bicpun-wacmex", "dosbyl-simnep-doprel-figtyd", "sigrec-dozryl-barres-datlyt", "davset-lacres-sartyn-datber",
                "ransed-lavwyt-dopmep-taglut", "fopler-moppec-partev-dosfex", "bolryx-mipbur-rossul-raptyl", "sorlun-dabden-mogfur-hallev",
                "posnep-nomlys-lavpem-dirbud", "wanlun-fillud-samdex-patlet", "fadlug-hosteb-difnyr-mosnul", "litsyr-sidsyr-midmed-dosdun",
                "fotfus-sabret-dossyp-sattun", "hobsum-palder-dapsep-lanlyn", "hosdeg-salryn-lantes-richex", "nidtyp-namrev-ridweb-hopwyt",
                "pagseg-hactud-sabwyd-poddec", "wanfex-barsug-fitsup-hapder", "wittyd-togfun-lidner-falned", "tansev-nopnem-havnyd-hiddus",
                "dattus-tardut-lopmes-larfep", "boldev-donreg-sopleg-simhus", "todteg-hidtev-datheb-tocnel", "sabmel-palrel-wicseb-nisfel",
                "roplux-picryd-locwyc-satnup", "davdel-dibmev-nidwyt-namren", "togmel-racrus-botdev-ribrut", "moshep-dilsel-winpex-fidfus",
                "ticbyn-dablyn-fogner-hosmug", "nimbyn-watbyl-rocmus-hatden", "tammes-nistyp-rablyd-malnex", "ragpet-tirmun-fabdux-lisfeb",
                "sipber-figlyt-harwep-lagrys", "hashec-toldyr-tidhep-lintem", "toprud-nosnev-holret-tamlen", "socnel-sicpub-harfus-foplud",
                "lorhes-ravmeg-naclyx-libfur", "sabmus-molsyt-falfun-nappem", "tanrys-divbyn-picteg-podlux", "latpes-fonref-dabweb-tamtyn",
                "hadsel-hasfed-podmug-bildux", "dovsum-digrul-sicmep-nosfen", "daprum-mopbus-hasmyn-bolsyd", "macfex-landut-sanluc-libryn",
                "ravsec-torsup-magwyd-dartyl", "riptev-lopsen-ribmyn-rismul", "hadlys-battus-mitzod-midrut", "rindyl-darwyc-molmus-sivnup",
                "pannep-timwyn-tabtug-ropsyp", "havnyr-dalhul-bidres-racfyr", "hocheb-savlen-molbud-ragdur", "firsym-parpyl-livsup-sardun",
                "samnul-digsud-waclen-rosfyl", "mogrum-larsul-hobrex-filtug", "widful-middyt-tanfyr-macmeg", "dolfyl-ravlyt-fignut-bonseg",
                "rovdep-rivset-fonlyn-tortyp", "banler-rovsyt-fanleb-lavrym", "lantus-pasdeb-fondyr-mirrux", "filbyn-sivnub-lonfep-sornut",
                "socwes-lopten-ravteg-lislev", "dapnul-radtyc-tortex-linsud", "mirweb-danhus-livryp-sibsel", "pagler-paswes-fitneb-napneb",
                "firful-paldyl-batseb-divpec", "samnel-lonmyr-rabmev-dilrun", "hintyv-lanbep-hatrys-ticsun", "winter-rapryc-simmer-dacsun",
                "somtul-savmur-faldur-sogsep", "lornel-tadbes-digmed-nilryp", "macsyp-sitbet-dabrel-darsup", "sigryn-tiltug-radhec-tillyt",
                "rigrel-faddyr-nimpes-tarfet", "micrun-pallec-tomber-podsup", "padnul-padmev-lopdem-sivhus", "nomzod-dinmun-mactug-rislep",
                "tilsur-lislev-samryx-dorfyn", "ricsed-polpun-falfel-diswet", "disled-lardul-watpem-modhul", "rivbus-socmeb-timmer-tarlyd",
                "talhul-baltuc-fogted-pidhec", "savwyn-natsef-racdep-nallyn", "fotwyt-bidhep-harlug-rovdus", "mosryx-palrep-magser-racryn"
            };

            var rng = new Random(67835);

            var vs = new List<string>();

            for (var i = 0; i < 128; i++)
            {
                var v = new BalderHash64(NextUInt64());
                vs.Add(v.ToString());

                Assert.AreEqual(values[i], v.ToString());
            }

            Console.WriteLine(string.Join(", ", vs.Select(a => $"\"{a}\"")));

            ulong NextUInt64()
            {
                var buffer = new byte[8];
                rng.NextBytes(buffer);
                return BitConverter.ToUInt64(buffer, 0);
            }
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
