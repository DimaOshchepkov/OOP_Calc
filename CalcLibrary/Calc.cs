using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public static class Calc
    {
        static Postfix post = new Postfix();
        static Infix inf = new Infix();
        static Operation op = new Operation();

        public static string DoOperation(object expression)
        {
            List<string> postfix;
            if (expression is string)
                postfix = post.ToPostfix(inf.ToInfix(expression as string));
            else
                postfix = post.ToPostfix(expression as List<string>);

            return op.DoCalc(postfix);
        }
        static double ToAnother(double doubleNumber, int toBase)
        {
            long doubleBits = BitConverter.DoubleToInt64Bits(doubleNumber);
            string binaryString = Convert.ToString(doubleBits, toBase);

            // Insert the decimal point at the appropriate position
            int decimalIndex = binaryString.Length - 52; // Assuming a double precision number
            binaryString = binaryString.Insert(decimalIndex, ",");

            return double.Parse(binaryString);
        }
    }
}
