using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HexadecimalConverter
{
    public class HexadecimalConverter
    {    /// <summary>
         /// Convert a number into hexadecimal data.
         /// </summary>
         /// <param name="number">Unsigned long number.</param>
         /// <returns>Hexadecimal data in string format.</returns>
        public static string ToHexadecimal(ulong number)
        {
            Stack<string> stack = new Stack<string>();
            while (number != 0)
            {
                ulong mod = number % 16;
                switch (mod)
                {
                    case 10:
                        stack.Push("A");
                        break;
                    case 11:
                        stack.Push("B");
                        break;
                    case 12:
                        stack.Push("C");
                        break;
                    case 13:
                        stack.Push("D");
                        break;
                    case 14:
                        stack.Push("E");
                        break;
                    case 15:
                        stack.Push("F");
                        break;
                    default:
                        stack.Push(mod.ToString());
                        break;
                }

                number /= 16;
            }
            return stack.Aggregate((i, j) => i + j);
        }

        /// <summary>
        /// Convert one block of hexadecimal string data into unsigned long value.
        /// </summary>
        /// <param name="input">A block of hexadecimal string data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <exception cref="FormatException">`input` is not hexadecimal data.</exception>
        /// <returns>Unsigned long value.</returns>
        public static ulong FromHexadecimal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            ulong result = 0;
            int power = 0;
            int i = input.Length - 1;
            string[] data = new string[]
                {
                "0","1","2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f"
                };
            for (; i >= 0; i--)
            {
                string rawString = input[i].ToString().ToLower();
                if (!data.Contains(rawString))
                    throw new FormatException($"{input} is not valid hexadecimal data");
                string strValue = string.Empty;
                switch (rawString)
                {
                    case "a":
                        strValue = "10";
                        break;
                    case "b":
                        strValue = "11";
                        break;
                    case "c":
                        strValue = "12";
                        break;
                    case "d":
                        strValue = "13";
                        break;
                    case "e":
                        strValue = "14";
                        break;
                    case "f":
                        strValue = "15";
                        break;
                    default:
                        strValue = rawString;
                        break;
                }
                if (ulong.TryParse(strValue, out ulong val))
                {
                    result += (ulong)Math.Pow(16, power++) * val;
                }
                else
                    throw new FormatException($"{input} is not valid hexadecimal data");

            }
            return result;
        }
        /// <summary>
        /// Convert string into hexadecimal data in string format.
        /// </summary>
        /// <param name="input">String data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Hexadecimal data in string format.</returns>
        public static string StringToHexadecimal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            StringBuilder sb = new StringBuilder();
            foreach (var chr in input)
            {
                sb.Append(ToHexadecimal((ulong)chr) + " ");
            }
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Convert hexadecimal string data into string value.
        /// </summary>
        /// <param name="input">Hexadecimal data in string format.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Real string value.</returns>
        public static string HexadecimalToString(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            string[] array = Regex.Split(input, @"\s+");
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append(((char)FromHexadecimal(item)));
            }
            return sb.ToString();
        }
    }
}
