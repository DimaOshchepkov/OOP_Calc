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
            for (int i = 0; i < 100; i++)
            {
                int a = r.Next(0, 10);
                int b = r.Next(0, 10);
                int c = r.Next(0, 10);
                int pow = r.Next(0, 4);

                Assert.AreEqual(Calc.DoOperation($"{a} + {b} * {c}"), (a + b * c).ToString());
                Assert.AreEqual(Calc.DoOperation($"{a}*{b}^{pow}"), (a * Math.Pow(b, pow)).ToString());
            }
        }

        [TestMethod]
        public void Brackets()
        {
            for (int i = 0; i < 100; i++)
            {
                int a = r.Next(0, 100);
                int b = r.Next(0, 100);
                int c = r.Next(0, 100);

                Assert.AreEqual(Calc.DoOperation($"({a} + {b}) * {c}"), ((a + b) * c).ToString());
                Assert.AreEqual(Calc.DoOperation($"({a} - {b}) / {c}"), ((double)(a - b) / c).ToString());
            }
        }

        [TestMethod]
        public void Function()
        {
            for (int i = 0; i < 100; i++)
            {
                int a = r.Next(0, 100);
                int b = r.Next(0, 100);
                int c = r.Next(0, 100);
                int d = r.Next(0, 100);

                Assert.AreEqual(Calc.DoOperation($"cos({a}) + sin({b}) + tan({c}) + sqrt({d})"),
                        (Math.Cos(a) + Math.Sin(b) + Math.Tan(c) + Math.Sqrt(d)).ToString());
            }
        }

        [TestMethod]
        public void Unary()
        {
            for (var i = 0; i < 100; i++)
            {
                int a = r.Next(0, 100);
                int b = r.Next(0, 100);
                Assert.AreEqual(Calc.DoOperation($"-{a} + {b}"), (-a + b).ToString());
                Assert.AreEqual(Calc.DoOperation($"+{a} - (-{b})"), (+a - (-b)).ToString());
            }
        }
    }
}