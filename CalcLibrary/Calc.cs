using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public static class Calc
    {
        static Dictionary<string, int> precedence = new Dictionary<string, int>(){
            { "(", 0 }, {")", 0 },
            { "+", 1 }, {"-", 1 },
            { "*", 2 },{ "/", 2 },
            { "^", 3 }};

        static Dictionary<string, Func<double, double>> functions =
            new Dictionary<string, Func<double, double>>(){
            {"cos", Math.Cos }, {"sin", Math.Sin },
            {"tan", Math.Tan }, {"sqrt", Math.Sqrt } };

        static List<string> ToInfix(string expression)
        {
            var infix = new List<string>();
            string num = "";
            foreach (var ch in expression)
            {
                if (Char.IsDigit(ch) || ch == ',' || ch == '.')
                    num += ch;
                else
                {
                    if (num != "")
                    {
                        infix.Add(num);
                        num = "";
                    }
                    if (ch != ' ')
                        infix.Add(ch.ToString());
                }
            }
            if (num != "")
                infix.Add(num);
            return infix;
        }

        static List<string> ToPostfix(List<string> infix)
        {
            var postfix = new List<string>();
            var operatorStack = new Stack<string>();

            foreach (string token in infix)
            {
                if (double.TryParse(token, out double value))
                    postfix.Add(token);
                else if (token == "(")
                    operatorStack.Push(token);
                else if (token == ")")
                {
                    while (operatorStack.Peek() != "(")
                        postfix.Add(operatorStack.Pop());
                    operatorStack.Pop();
                }
                else if (functions.ContainsKey(token))
                    operatorStack.Push(token);
                else
                {
                    while (operatorStack.Count != 0 &&
                            precedence[operatorStack.Peek()] >= precedence[token])
                        postfix.Add(operatorStack.Pop());
                    operatorStack.Push(token);
                }
            }
            while (operatorStack.Count != 0)
                postfix.Add(operatorStack.Pop());

            return postfix;
        }


        public static string DoOperation(string expression)
        {
            Stack<string> stack = new Stack<string>();
            List<string> postfix = ToPostfix(ToInfix(expression));
            foreach (string token in postfix)
            {
                if (double.TryParse(token, out double value))
                    stack.Push(token);
                else if (functions.ContainsKey(token))
                {
                    double arg = double.Parse(stack.Pop());
                    double result = functions[token](arg);
                    stack.Push(result.ToString());
                }
                else
                {
                    if (token == "^")
                    {
                        double exp = double.Parse(stack.Pop());
                        double b = double.Parse(stack.Pop());
                        stack.Push(Math.Pow(b, exp).ToString());
                    }
                    else
                    {
                        double b = double.Parse(stack.Pop());
                        double a = double.Parse(stack.Pop());
                        if (token == "+")
                            stack.Push((a + b).ToString());
                        else if (token == "-")
                            stack.Push((a - b).ToString());
                        else if (token == "*")
                            stack.Push((a * b).ToString());
                        else if (token == "/")
                            stack.Push((a / b).ToString());
                    }
                }
            }
            return stack.Pop();
        }
    }
}
