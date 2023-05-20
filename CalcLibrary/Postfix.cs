using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    /// <summary>
    /// Класс предназначен для перевода инфиксной записи в постфиксную
    /// </summary>
    class Postfix
    {
        /// <summary>
        /// Вспомогательная функция для  определения приоритета операции op
        /// </summary>
        /// <param name="op">операция или функция</param>
        /// <returns> возвращает приоритет операции</returns>
        static int GetPrecedence(string op)
        {
            if ("+" == op || "-" == op)
                return 1;
            else if ("*" == op || "/" == op || op == "%" || op == "xor" || op == "or")
                return 2;
            else if ("cos" == op || "sin" == op || "tan" == op || "sqrt" == op ||
                    "#-" == op || "#+" == op || op == "exp" || op == "bin" || op == "oct"
                    || op == "hex")
                return 3;
            else if ("^" == op || "!" == op)
                return 4;
            else
                return 0;
        }

        /// <summary>
        /// Определяет является ли токен операцией
        /// </summary>
        /// <param name="token"> предполагаемая операция</param>
        /// <returns>true, если операция, иначе false</returns>
        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "xor" || token == "or" ||
                token == "/" || token == "^" || token == "#+" || token == "#-" || token == "%";
        }

        /// <summary>
        /// Опеределяет есть ли возможность у данного оператора быть унарным
        /// </summary>
        /// <param name="op">оператор</param>
        /// <returns>true, если есть возможность быть унарным оператором, иначе false</returns>
        static bool IsUnaryOperator(string op)
        {
            return op == "+" || op == "-";
        }

        /// <summary>
        /// список функций
        /// </summary>
        /// <TODO>Это поле избыточно, нужно реструкторизовать программу</TODO>
        static Dictionary<string, Func<double, double>> functions =
            new Dictionary<string, Func<double, double>>(){
            {"cos", Math.Cos }, {"sin", Math.Sin },
            {"tan", Math.Tan }, {"sqrt", Math.Sqrt },
            {"exp", Math.Exp } }; 

        /// <summary>
        /// Переводит постфиксную запись в инфиксную
        /// </summary>
        /// <param name="infix"> инфиксное представление выражения</param>
        /// <returns>Постфиксное представление</returns>
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
