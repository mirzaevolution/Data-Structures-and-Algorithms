using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryConverter
{
    public class BinaryConverter
    {
        /// <summary>
        /// Convert a number into binary data.
        /// </summary>
        /// <param name="number">Unsigned long number.</param>
        /// <returns>Binary data in string format.</returns>
        public static string ToBinary(ulong number)
        {
            Stack<string> stack = new Stack<string>();
            while (number != 0)
            {
                ulong mod = number % 2;
                stack.Push(mod.ToString());
                number /= 2;
            }
            return stack.Aggregate((i, j) => i + j);
        }
        /// <summary>
        /// Convert one block of binary string data into unsigned long value.
        /// </summary>
        /// <param name="input">A block of binary string data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <exception cref="FormatException">`input` is not binary data.</exception>
        /// <returns>Unsigned long value.</returns>
        public static ulong FromBinary(string input)
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
                    if (val != 1 && val != 0)
                        throw new FormatException($"{input} is not binary data");
                    result += (ulong)Math.Pow(2, power++) * val;
                }
                else
                    throw new FormatException($"{input} is not binary data");

            }
            return result;
        }
        /// <summary>
        /// Convert string into binary data in string format.
        /// </summary>
        /// <param name="input">String data.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Binary data in string format.</returns>
        public static string StringToBinary(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            StringBuilder sb = new StringBuilder();
            foreach (var chr in input)
            {
                sb.Append(ToBinary((ulong)chr) + " ");
            }
            return sb.ToString().TrimEnd();
        }
        /// <summary>
        /// Convert binary string data into string value.
        /// </summary>
        /// <param name="input">Binary data in string format.</param>
        /// <exception cref="ArgumentNullException">`input` cannot be null.</exception>
        /// <returns>Real string value.</returns>
        public static string BinaryToString(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException("`input` cannot be null");
            string[] array = Regex.Split(input, @"\s+");
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append(((char)FromBinary(item)));
            }
            return sb.ToString();
        }
    }
}
