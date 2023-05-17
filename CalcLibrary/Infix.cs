using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CalcLibrary
{
    class Infix
    {
        static string pattern = @"(\d+(\.|\,)\d+|\d+|cos|sin|tan|sqrt|!|%|exp|\S)";

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
