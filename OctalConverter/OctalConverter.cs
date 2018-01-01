using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OctalConverter
{
    public class OctalConverter
    {
        /// <summary>
        /// Convert a number into octal data.
        /// </summary>
        /// <param name="number">Unsigned long number.</param>
        /// <returns>Octal data in string format.</returns>
        public static string ToOctal(ulong number)
        {

            Stack<string> stack = new Stack<string>();
            while (number != 0)
            {
                ulong mod = number % 8;
                stack.Push(mod.ToString());
                number /= 8;
            }
            return stack.Aggregate((i, j) => i + j);
        }
        /// <summary>
        /// Convert one block of octal string data into unsigned long value.
        /// </summary>
        /// <param name="input">A block of octal string data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <exception cref="FormatException">`input` is not octal data.</exception>
        /// <returns>Unsigned long value.</returns>
        public static ulong FromOctal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            ulong result = 0;
            int power = 0;
            int i = input.Length - 1;
            for (; i >= 0; i--)
            {
                if (ulong.TryParse(input[i].ToString(), out ulong val))
                {
                    if (val == 8 || val == 9)
                        throw new FormatException($"{input} is not octal data");
                    result += (ulong)Math.Pow(8, power++) * val;
                }
                else
                    throw new FormatException($"{input} is not octal data");

            }
            return result;
        }

        /// <summary>
        /// Convert string into octal data in string format.
        /// </summary>
        /// <param name="input">String data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Octal data in string format.</returns>
        public static string StringToOctal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            StringBuilder sb = new StringBuilder();
            foreach (var chr in input)
            {
                sb.Append(ToOctal((ulong)chr) + " ");
            }
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Convert octal string data into string value.
        /// </summary>
        /// <param name="input">Octal data in string format.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Real string value.</returns>
        public static string OctalToString(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            string[] array = Regex.Split(input, @"\s+");
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append(((char)FromOctal(item)));
            }
            return sb.ToString();
        }
    }
}
