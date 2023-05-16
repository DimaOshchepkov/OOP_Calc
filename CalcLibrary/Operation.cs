﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    class Operation
    {
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