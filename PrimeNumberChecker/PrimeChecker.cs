using System;
namespace PrimeNumberChecker
{
    public class PrimeChecker
    {
        /// <summary>
        /// Check a prime number status from a given number.
        /// </summary>
        /// <param name="value">Integer value.</param>
        /// <returns>True/False indicates the status of prime number.</returns>
        public static bool IsPrime(int value)
        {
            if (value <= 1)
                return false;
            if (value == 2)
                return true;
            int bound = Convert.ToInt32(Math.Ceiling(Math.Sqrt(value)));
            for (int number = 2; number <= bound; number++)
            {
                if (value % number == 0) return false;
            }
            return true;
        }
    }
}
