﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CalcLibrary
{
    /// <summary>
    /// Класс для парсинга выражения на токены
    /// </summary>
    class Infix
    {
        static string pattern = @"(\d+(\.|\,)\d+|\d+|cos|sin|tan|sqrt|!|%|exp|xor|or|bin|oct|hex|\S)";

        /// <summary>
        /// Перевод строки в список токенов
        /// </summary>
        /// <param name="expression">выражение, которое нужно распарсить</param>
        /// <returns>Распарщенное выражение в инфиксной форме</returns>
        public List<string> ToInfix(string expression)
        {
            List<string> tokens = new List<string>();
            foreach (Match match in Regex.Matches(expression, pattern))
            {
                tokens.Add(match.Value);
            }
            return tokens;
        }
    }
}
