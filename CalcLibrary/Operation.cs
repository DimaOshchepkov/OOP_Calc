using CalcLibrary.AdapterDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalcLibrary
{
    /// <summary>
    /// Класс для высчитывания выражения в постфикной форме
    /// </summary>
    class Operation
    {
        /// <summary>
        /// Адаптер для библиотеки BinCalc.dll
        /// </summary>
        TargetBinOperation BinOp = new TargetBinOperation();

        /// <summary>
        /// Функция для подсчета факториала
        /// </summary>
        /// <param name="n"> Число</param>
        /// <returns>Факториал числа</returns>
        static double Factorial(double n)
        {
            if (n < 0)
                throw new ArgumentException();

            if ((int)n != n)
            {
                double log = Math.Log(Factorial((int)n)) +
                              (n - (int)n) * Math.Log((int)n + 1);
                return Math.Exp(log);
            }

            if (n <= 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        /// <summary>
        /// Произовит вычисление с постфиксной записью выражения
        /// </summary>
        /// <param name="postfix">постфиксная запись выражения</param>
        /// <returns>Значение выражения</returns>
        public string DoCalc(List<string> postfix)
        {
            Stack<double> operandStack = new Stack<double>();

            foreach (string token in postfix)
            {
                if (double.TryParse(token, out double operand))
                    operandStack.Push(operand);
                else
                {
                    switch (token)
                    {
                        case "+":
                            operandStack.Push(operandStack.Pop() + operandStack.Pop());
                            break;
                        case "-":
                            double subtrahend = operandStack.Pop();
                            operandStack.Push(operandStack.Pop() - subtrahend);
                            break;
                        case "*":
                            operandStack.Push(operandStack.Pop() * operandStack.Pop());
                            break;
                        case "/":
                            double divisor = operandStack.Pop();
                            operandStack.Push(operandStack.Pop() / divisor);
                            break;
                        case "%":
                            double div = operandStack.Pop();
                            double res = operandStack.Pop() % div;
                            if (res < 0)
                                res += div;
                            operandStack.Push(res);
                            break;
                        case "^":
                            double exponent = operandStack.Pop();
                            operandStack.Push(Math.Pow(operandStack.Pop(), exponent));
                            break;
                        case "xor":
                            operandStack.Push(BinOp.Calc(operandStack.Pop(), operandStack.Pop(), token));
                            break;
                        case "or":
                            operandStack.Push(BinOp.Calc(operandStack.Pop(), operandStack.Pop(), token));
                            break;
                        case "sin":
                            operandStack.Push(Math.Sin(operandStack.Pop()));
                            break;
                        case "exp":
                            operandStack.Push(Math.Exp(operandStack.Pop()));
                            break;
                        case "cos":
                            operandStack.Push(Math.Cos(operandStack.Pop()));
                            break;
                        case "tan":
                            operandStack.Push(Math.Tan(operandStack.Pop()));
                            break;
                        case "sqrt":
                            operandStack.Push(Math.Sqrt(operandStack.Pop()));
                            break;
                        case "#-":
                            operandStack.Push(-operandStack.Pop());
                            break;
                        case "#+":
                            operandStack.Push(operandStack.Pop());
                            break;
                        case "!":
                            operandStack.Push(Factorial(operandStack.Pop()));
                            break;
                    }
                }
            }

            return operandStack.Pop().ToString();
        }
    }
}
