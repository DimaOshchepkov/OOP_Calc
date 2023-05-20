using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConvert
{
    using System;

    class Program
    {
        static void Main()
        {
            string number = "1aa.625";
            int sourceBase = 16;
            int targetBase = 2;
            string convertedNumber = ConvertBaseWithFraction(number, sourceBase, targetBase);
            Console.WriteLine(convertedNumber);

            Console.ReadLine();
        }

        static string ConvertBaseWithFraction(string number, int sourceBase, int targetBase)
        {
            string[] parts = number.Split('.');
            string integerPart = parts[0];
            string fractionalPart = parts[1];

            // Convert the integer part to the target base
            string convertedIntegerPart = ConvertBase(integerPart, sourceBase, targetBase);

            // Convert the fractional part to the target base
            string convertedFractionalPart = ConvertFractionalPart(fractionalPart, sourceBase, targetBase);

            // Combine the converted parts
            string convertedNumber = convertedIntegerPart + "." + convertedFractionalPart;

            return convertedNumber;
        }

        static string ConvertBase(string number, int sourceBase, int targetBase)
        {
            // Convert the number to decimal
            int decimalNumber = ConvertToDecimal(number, sourceBase);

            // Convert the decimal number to the target base
            string convertedNumber = ConvertFromDecimal(decimalNumber, targetBase);

            return convertedNumber;
        }

        static int ConvertToDecimal(string number, int sourceBase)
        {
            int decimalNumber = 0;
            int power = 0;

            // Iterate through each digit of the number from right to left
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int digit = GetDigitValue(number[i]);

                // Multiply the digit by the corresponding power of the source base and add it to the decimal number
                decimalNumber += digit * (int)Math.Pow(sourceBase, power);
                power++;
            }

            return decimalNumber;
        }

        static string ConvertFromDecimal(int decimalNumber, int targetBase)
        {
            string convertedNumber = "";

            // Special case for 0
            if (decimalNumber == 0)
            {
                return "0";
            }

            // Convert the decimal number to the target base
            while (decimalNumber > 0)
            {
                int remainder = decimalNumber % targetBase;
                char digit = GetDigitChar(remainder);
                convertedNumber = digit + convertedNumber;
                decimalNumber /= targetBase;
            }

            return convertedNumber;
        }

        static string ConvertFractionalPart(string fractionalPart, int sourceBase, int targetBase)
        {
            double fraction = double.Parse("0," + fractionalPart);
            string convertedFractionalPart = "";

            int maxDigits = 15; // Maximum number of digits to avoid infinite fraction representation

            for (int i = 0; i < maxDigits; i++)
            {
                fraction *= targetBase;
                int digit = (int)fraction;
                char digitChar = GetDigitChar(digit);
                convertedFractionalPart += digitChar;
                fraction -= digit;

                if (fraction == 0)
                {
                    break;
                }
            }

            return convertedFractionalPart;
        }

        static int GetDigitValue(char digit)
        {
            if (char.IsDigit(digit))
            {
                return digit - '0';
            }
            else
            {
                return char.ToUpper(digit) - 'A' + 10;
            }
        }

        static char GetDigitChar(int value)
        {
            if (value >= 0 && value <= 9)
            {
                return (char)(value + '0');
            }
            else
            {
                return (char)(value - 10 + 'A');
            }
        }
    }
}
