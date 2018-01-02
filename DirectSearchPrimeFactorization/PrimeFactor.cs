using System;
using System.Collections.Generic;

namespace DirectSearchPrimeFactorization
{
    public class PrimeFactor
    {
        private static bool IsPrime(uint number)
        {
            if (number <= 1)
                return false;
            if (number == 2)
                return true;
            uint bound = (uint)Math.Ceiling(Math.Sqrt(number));
            for (int j = 2; j < bound; j++)
            {
                if (number % j == 0) return false;
            }
            return true;
        }
        public static IEnumerable<uint> FindPrimeFactors(uint number)
        {
            List<uint> numbers = new List<uint>();
            List<uint> primes = new List<uint>();
            for (uint i = 2; i <= number; i++)
            {
                if (IsPrime(2))
                {
                    primes.Add(i);
                }
            }
            int index = 0;
            while (number != 1)
            {
                if (number % primes[index] == 0)
                {
                    numbers.Add(primes[index]);
                    number /= primes[index];
                }
                else
                    index++;
            }
            return numbers;
        }
    }
}
