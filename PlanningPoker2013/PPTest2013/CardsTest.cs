using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    /// <summary>
    /// Summary description for CardsTest
    /// </summary>
    [TestClass]
    public class CardsTest
    { 
        public CardsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CoffeeCupEqualsCoffeeCup()
        {
            var c1 = Card.CoffeeCup;
            var c2 = Card.CoffeeCup;
            Assert.AreEqual(c1,c2);
        }
        
        [TestMethod]
        public void ConvertValuesEqual()
        {
            var d = 1.2m;
            var c1 = (Card)d;
            var c2 = (Card)d;
            Assert.AreEqual(c1, c2);
        }
        
        [TestMethod]
        public void ValuesEqual()
        {
            var d = 1.2m;
            var c1 = new Card(d);
            var c2 = new Card(d);
            Assert.AreEqual(c1, c2);
        }
        [TestMethod]
        public void AddingValues()
        {
            var d1 = 1.2m;
            var d2 = 1.2m;

            var c1 = new Card(d1);
            var c2 = new Card(d2);
            var c3 = Card.CoffeeCup;
            var d = (decimal) (c1 + c2+c3);
            Assert.AreEqual(d1+d2 ,d);
        }

    }
}
