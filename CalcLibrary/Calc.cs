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
    /// <summary>
    /// Класс для вычисления выражения
    /// </summary>
    public static class Calc
    {
        static Postfix post = new Postfix();
        static Infix inf = new Infix();
        static Operation op = new Operation();
        /// <summary>
        /// Функция, производящая вычисление
        /// </summary>
        /// <param name="expression">string или List&ltstring&gt инфиксное выражение</param>
        /// <returns>Значение выражения</returns>
        public static string DoOperation(object expression)
        {
            List<string> postfix;
            if (expression is string)
                postfix = post.ToPostfix(inf.ToInfix(expression as string));
            else
                postfix = post.ToPostfix(expression as List<string>);

            return op.DoCalc(postfix);
        }
    }
}
