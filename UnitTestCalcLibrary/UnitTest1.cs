using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CalcLibrary;

namespace UnitTestCalcLibrary
{
    [TestClass]
    public class UnitTestCalcLibrary
    {
        Random r = new Random();
        [TestMethod]
        public void BinaryOperation()
        {
            for (int i = 0; i < 100; i++)
            {
                int a = r.Next(0, 100);
                int b = r.Next(0, 100);

                Assert.AreEqual(Calc.DoOperation($"{a} + {b}"), (a + b).ToString());
                Assert.AreEqual(Calc.DoOperation($"{a} - {b}"), (a - b).ToString());
                Assert.AreEqual(Calc.DoOperation($"{a} * {b}"), (a * b).ToString());
                Assert.AreEqual(Calc.DoOperation($"{a} / {b}"), ((double)a / b).ToString());
            }

        }

        [TestMethod]
        public void OperationPriory()
        {
            Assert.AreEqual(Calc.DoOperation("2 + 2*2"), "6");
            Assert.AreEqual(Calc.DoOperation("4*2^4"), "64");
        }

        [TestMethod]
        public void Brackets()
        {
            Assert.AreEqual(Calc.DoOperation("(20 + 12) * 2"), "64");
            Assert.AreEqual(Calc.DoOperation("(12,5 - 0,5) / 4"), "3");
        }
    }
}