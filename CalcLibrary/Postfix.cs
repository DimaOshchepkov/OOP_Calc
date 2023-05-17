using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    class Postfix
    {
        static int GetPrecedence(string op)
        {
            if ("+" == op || "-" == op)
                return 1;
            else if ("*" == op || "/" == op || op == "%")
                return 2;
            else if ("cos" == op || "sin" == op || "tan" == op || "sqrt" == op ||
                    "#-" == op || "#+" == op )
                return 3;
            else if ("^" == op || "!" == op)
                return 4;
            else
                return 0;
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" ||
                token == "/" || token == "^" || token == "#+" || token == "#-" || token == "%";
        }

        static bool IsUnaryOperator(string op)
        {
            return op == "+" || op == "-";
        }

        static Dictionary<string, Func<double, double>> functions =
            new Dictionary<string, Func<double, double>>(){
            {"cos", Math.Cos }, {"sin", Math.Sin },
            {"tan", Math.Tan }, {"sqrt", Math.Sqrt } };

        public List<string> ToPostfix(List<string> infix)
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
    }
}
