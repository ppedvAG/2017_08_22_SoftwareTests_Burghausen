using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDDBank.Tests
{
    [TestClass]
    public class KontoTests
    {
        [TestMethod]
        public void Konto_New_Kontostand_0()
        {
            var konto = new Konto();

            decimal result = konto.Kontostand;

            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void Konto_Einzahlen_3_Kontostand_3()
        {
            var konto = new Konto();

            konto.Einzahlen(3m);

            Assert.AreEqual(3m, konto.Kontostand);
        }

        [TestMethod]
        public void Konto_Einzahlen_3_und_3_Kontostand_6()
        {
            var konto = new Konto();

            konto.Einzahlen(3m);
            konto.Einzahlen(3m);

            Assert.AreEqual(6m, konto.Kontostand);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Konto_Einzahlen_M3_ArgumentException()
        {
            var konto = new Konto();

            konto.Einzahlen(-3m); // expected: ArgumentException
        }

        [TestMethod]
        public void Konto_Einzahlen_7_Auszahlen_4_Kontostand_3()
        {
            var konto = new Konto();
            konto.Einzahlen(7m);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 14);
                konto.Auszahlen(4m);
            }

            Assert.AreEqual(3m, konto.Kontostand);
        }

        [TestMethod]
        public void Konto_Einzahlen_7_Auszahlen_2_und2__Kontostand_3()
        {
            var konto = new Konto();
            konto.Einzahlen(7m);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 14);
                konto.Auszahlen(2m);
                konto.Auszahlen(2m);
            }

            Assert.AreEqual(3m, konto.Kontostand);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Konto_Einzahlen_10_Auszahlen_13_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10m);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 14);
                konto.Auszahlen(13m);   // expected: InvalidOperationException
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Konto_Auszahlen_M3_ArgumentException()
        {
            var konto = new Konto();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 14);
                konto.Auszahlen(-3m); // expected: ArgumentException
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Konto_Auszahlen_Am_Samstag_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 19);
                konto.Auszahlen(3m); // expected: InvalidOperationException
            }
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Konto_Auszahlen_Am_Sonntag_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 20);
                konto.Auszahlen(3m); // expected: InvalidOperationException
            }
        }
        [TestMethod]
        public void Konto_Auszahlen_UnterDerWoche_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 14);
                konto.Auszahlen(1m);

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 15);
                konto.Auszahlen(1m);

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 16);
                konto.Auszahlen(1m);

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 17);
                konto.Auszahlen(1m);

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 08, 18);
                konto.Auszahlen(1m);
            }
        }
    }
}
