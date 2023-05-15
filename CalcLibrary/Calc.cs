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
        static int GetPrecedence(string op)
        {
            if ("+" == op || "-" == op)
                return 1;
            else if ("*" == op || "/" == op)
                return 2;
            else if ("cos" == op || "sin" == op || "tan" == op || "sqrt" == op ||
                    "#-" == op || "#+" == op)
                return 3;
            else if ("^" == op)
                return 4;
            else
                return 0;
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" ||
                token == "/" || token == "^" || token == "#+" || token == "#-";
        }

        static bool IsUnaryOperator(string op)
        {
            return op == "+" || op == "-";
        }

        static Dictionary<string, Func<double, double>> functions =
            new Dictionary<string, Func<double, double>>(){
            {"cos", Math.Cos }, {"sin", Math.Sin },
            {"tan", Math.Tan }, {"sqrt", Math.Sqrt } };

        static string pattern = @"(\d+(\.|\,)\d+|\d+|cos|sin|tan|sqrt|\S)";

        static List<string> ToInfix(string expression)
        {
            
            List<string> tokens = new List<string>();
            foreach (Match match in Regex.Matches(expression, pattern))
            {
                tokens.Add(match.Value);
            }
            return tokens;
        }

        static List<string> ToPostfix(List<string> infix)
        {
            var postfix = new List<string>();
            var operatorStack = new Stack<string>();

            for (int i = 0; i < infix.Count; i++)
            {
                string token = infix[i];
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
                    if (IsUnaryOperator(token))
                    {
                        // Проверяем, является ли предыдущий токен оператором или открывающей скобкой
                        // Если да, то текущий оператор является унарным оператором
                        bool isUnary = (i == 0 || IsOperator(infix[i - 1]) || infix[i - 1] == "(");

                        if (isUnary)
                        {
                            // Преобразуем унарный оператор в бинарный для удобства обработки
                            string unaryOperator = (token == "+") ? "#+" : "#-";
                            token = unaryOperator;
                        }
                    }
                    while (operatorStack.Count != 0 &&
                            GetPrecedence(operatorStack.Peek()) >= GetPrecedence(token))
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
            List<string> postfix = ToPostfix(ToInfix(expression));
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
                        case "^":
                            double exponent = operandStack.Pop();
                            operandStack.Push(Math.Pow(operandStack.Pop(), exponent));
                            break;
                        case "sin":
                            operandStack.Push(Math.Sin(operandStack.Pop()));
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
                    }
                }
            }

            return operandStack.Pop().ToString();
        }
    }
}
