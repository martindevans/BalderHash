using System;
using System.Collections.Generic;
using System.Linq;
using BalderHash;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalderHashTests
{
    [TestClass]
    public class BalderHash128Tests
    {
        [TestMethod]
        public void ParseInvalid()
        {
            Assert.IsNull(BalderHash128.Parse("aaaryc-racnes-midpen-ladbep-molryc-racnes-midpen-ladbep"));
            Assert.IsNull(BalderHash128.Parse("molaaa-racnes-midpen-ladbep-molryc-racnes-midpen-ladbep"));
            Assert.IsNull(BalderHash128.Parse("molryxracnes-midpen-ladbep-molryc-racnes-midpen-ladbep"));
            Assert.IsNull(BalderHash128.Parse("hello"));
            Assert.IsNull(BalderHash128.Parse("hello-world"));
            Assert.IsNull(BalderHash128.Parse("molryc-racnes-midpen-ladbep"));
            Assert.IsNull(BalderHash128.Parse("m1dpen-racnes-midpen-ladbep-molryc-racnes-midpen-ladbep"));
            Assert.IsNull(BalderHash128.Parse("molryc-racnes-midpen-ladbeplmolryc-racnes-midpen-ladbep"));
        }

        [TestMethod]
        public void ToSpanInvalid()
        {
#pragma warning disable MSTEST0051
            Assert.Throws<ArgumentException>(() =>
#pragma warning restore MSTEST0051
            {
                Span<char> span = stackalloc char[123];
                new BalderHash128(1234, 5678).ToSpan(span);
            });
        }

        [TestMethod]
        public void Roundtrip()
        {
            var rng = new Random(568746);

            Span<char> buffer = stackalloc char[55];

            for (var i = 0; i < 1_000_000; i++)
            {
                var lo = unchecked((ulong)rng.NextInt64());
                var hi = unchecked((ulong)rng.NextInt64());

                new BalderHash128(lo, hi).ToSpan(buffer);
                var b = BalderHash128.Parse(buffer);

                Assert.IsTrue(b.HasValue);
                Assert.AreEqual(lo, b.Value.ValueLo);
                Assert.AreEqual(hi, b.Value.ValueHi);
            }
        }

        [TestMethod]
        public void KnownValues()
        {
            // A set of known values. It's critical that these **DO NOT CHANGE** as the library is developed.

            var values = new[]
            {
                "rontug-tabnyl-biddeb-ligled-soclep-sivrul-ransef-lasret", "lavheb-dardys-botdyt-borrep-niddun-finbur-rapdeb-hadhul",
                "tirlut-dovrun-nolrys-dossyn-rontyc-banres-patlun-nompun", "timlud-lomteg-holmes-balweg-padtel-fabnys-widsug-mitben",
                "sigseb-mallud-natdun-ripbur-dirnup-fabsyn-disleb-figdyl", "dinlux-havsul-divnet-hablux-sapmur-haptex-lasmeg-naclur",
                "fidnyl-larsun-wacnyd-radlys-dabnys-hosser-nattev-foglyn", "ripteb-lacwyx-nornys-sivwyt-motsyt-mitref-dospex-laswyd",
                "tasrut-hodlyx-linlec-bonryc-ragrul-sogmut-tomteb-banhul", "londeg-darnul-polbex-davneb-folpun-walpyx-migfeb-fadmer",
                "fipmed-ralnup-savtux-novten-hocsyt-tadbur-tobtun-namtuc", "torbex-lommul-hocnes-bidlyn-bannup-hadmun-ranfeb-rilfyl",
                "larnep-firdem-worrus-winbyl-nispex-fosmel-lispun-bitler", "tashul-ligwyl-pagrux-hopled-rinnyr-tocryg-dilsed-foldun",
                "hilmun-digmyr-bantun-bisber-tolhep-podmed-laches-larnux", "davlyt-novryp-docnet-padren-faldyn-tintep-panfex-picren",
                "labsut-motmed-parryt-sarpex-foltec-davlut-rovwed-bitpes", "nilwyt-parbyl-natref-dopdyn-lartes-danref-midned-fanweg",
                "tamheb-lasfen-laddus-tirdun-silwer-batwer-namrem-tardur", "macbel-dilryc-migfex-radweb-riprun-rigwyl-mochut-lishes",
                "fothet-wordut-fotlyt-tocbec-rinsyp-wactun-sivtex-linlex", "labmyn-sitsug-lacres-sogdet-rilbus-dabbec-losbud-davsyd",
                "ladmec-satsun-tirteb-tanryn-dalnut-nibped-locnyd-tirfed", "fodlyx-mogsyr-savsun-witrup-donser-lopfen-dovtun-simlyn",
                "rittel-witlep-hobpem-hoslut-mogtyc-tambep-polryp-ropdul", "natsup-tasres-hilpel-morrys-napteb-danwyl-savpun-bisseg",
                "fopsup-mopnev-biclet-bidluc-dotwyl-sidwes-maphul-fopnyd", "dinref-hosrys-nallyr-poclur-rignem-lagrel-ponfex-fabdur",
                "savrut-narnem-ragteb-ribmug-rilseg-dirsub-firbec-wormep", "mignus-wannes-tapbyl-maslex-lapdef-danrup-riblug-lavbel",
                "ridpet-filnem-tidlec-silfes-mosleb-latsum-richec-martyc", "hinpes-bilnux-tadlet-filnyx-ticpyx-nibleb-billur-rilhus",
                "widpen-lastes-fitreb-doprel-mipden-dinsed-ropdef-sopleg", "mosnym-dapheb-namnes-faspub-hilrup-pitbep-lossyl-sarmut",
                "sivbyn-nosduc-todsyx-nornyt-namlup-fampyx-foslun-hactug", "dinlex-bilner-soldux-hostud-matdut-satpem-dozpeg-widtyl",
                "lanlun-ronrus-modped-dosmyl-daclec-faswes-libnes-laglec", "winlud-lagfur-tabnub-misbyr-todnel-bidbep-diltel-donbyt",
                "moldef-nolnyx-fitbud-ribsur-nilnet-ravmyn-pacmyl-simfun", "widsum-hadtel-bonmyl-pochut-postug-sorteb-rontug-topnyd",
                "hacwes-witmyl-tilmul-sartun-patdex-bidpun-natbes-pinheb", "mapdus-locmel-masmec-ravwyl-hoblec-handul-pacten-navnep",
                "tintel-hoprel-tadnys-fosdyn-bitmep-modteb-rabfeb-hoptel", "sonrec-wortyp-tipfed-rabfet-dirpem-davnex-fitlun-marmer",
                "sovmev-sogfur-libmec-mardus-monfel-dolnys-figbes-tinwyl", "pacnes-fosder-tobsed-rislec-hinden-ridwex-wolfur-ponhet",
                "silnys-dovsec-nardur-fottyr-sovnec-nidsef-magter-rapsul", "padfex-samduc-moldem-podlet-ligbur-motsef-hinput-migdyl",
                "tolwyl-holbur-modwel-hocfep-hosrus-namrut-witruc-tadwes", "ladner-laprys-linmer-wicsum-tinmyl-macmur-pidmur-palnec",
                "ranben-nocfyr-sogwed-hobryl-hattul-lisset-mosdeb-moslen", "borryn-ricnus-dorrut-wicmug-mirhut-mocsym-widrex-davmud",
                "hatwer-davnyr-havryx-dopdux-havdux-polner-hosrem-formec", "hasrec-faddec-dachex-dosted-parlys-noclep-magfus-pocfex",
                "rigweb-togsen-soldur-todtud-rollys-siplud-litber-ligres", "tippem-podbet-dosler-ribmel-padpub-tinleg-losrel-dosfyl",
                "fittel-wactul-tapfyn-ligsel-datfel-lomrym-mapwep-habref", "datsyn-mossup-ronryl-havnus-nammun-tocsyd-silteb-wolwyn",
                "batsyp-sitlur-misfus-tapten-dalryx-tinfel-fodbus-tidryp", "modnul-borden-tilsut-mipfeb-sammes-fidnup-divdyr-bisrum",
            };

            var rng = new Random(67835);

            var vs = new List<string>();

            for (var i = 0; i < values.Length; i++)
            {
                var v = new BalderHash128(NextUInt64(rng), NextUInt64(rng));
                vs.Add(v.ToString());

                Assert.AreEqual(values[i], v.ToString());
            }

            Console.WriteLine(string.Join(", ", vs.Select(a => $"\"{a}\"")));
        }

        [TestMethod]
        public void AllZero()
        {
            const ulong a = 0;
            var str = new BalderHash128(a, a).ToString();
            Console.WriteLine(str);
            var b = BalderHash128.Parse(str);
            Assert.IsNotNull(b);
            Assert.AreEqual(a, b.Value.ValueLo);
            Assert.AreEqual(a, b.Value.ValueHi);
        }

        [TestMethod]
        public void Parse()
        {
            var str = new BalderHash128(0x0123456789abcdefUL, 0xfedcba9876543210UL).ToString();
            var fid = BalderHash128.Parse(str);

            Assert.IsTrue(fid.HasValue);
            Assert.AreEqual(str, fid.ToString());

            Console.WriteLine(str);
            Console.WriteLine(fid.Value.ValueLo);
            Console.WriteLine(fid.Value.ValueHi);
            Console.WriteLine(fid.ToString());
        }

        [TestMethod]
        public void First100()
        {
            for (uint i = 0; i < 100; i++)
            {
                Console.WriteLine(new BalderHash128(i, i).ToString());
            }
        }

        private static ulong NextUInt64(Random rng)
        {
            return unchecked((ulong)rng.NextInt64());
        }
    }
}
