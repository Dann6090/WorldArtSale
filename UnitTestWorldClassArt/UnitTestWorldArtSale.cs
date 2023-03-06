using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Repository;
using BIZ;
using IO;



namespace UnitTestWorldClassArt
{
    [TestClass]
    public class UnitTestWorldArtSale
    {
        //Initionalize classes for unit test
        ClassBidCalck classBidCalck = new ClassBidCalck();
        ClassBIZ BIZ = new ClassBIZ();
        ClassRates cr = new ClassRates();

        [TestMethod]
        public void TestMethod1()
        {

            //Arrange. arrange svar til test
            cr.rates.Add("DKK", 7.50D);
            cr.rates.Add("EUR", 3.85D);
            cr.rates.Add("GBP", 8.45D);
            cr.rates.Add("INR", 0.085D);
            cr.rates.Add("CNY", 1.02D);

            


            //Act. hvor den tester 
            BIZ.CBC.valutaRates = cr;


            //Assert. testen for at finde fejl eller ej.
            Assert.AreEqual(1.02D, cr.ratesFull["CNY"].dubRate);

        }        

    }
}
