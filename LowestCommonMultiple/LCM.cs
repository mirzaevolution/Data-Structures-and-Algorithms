namespace LowestCommonMultiple
{
    public class EuclidGCD
    {
        public static uint FindGCD(uint number1, uint number2)
        {
            if (number2 > number1)
            {
                uint temp = number1;
                number1 = number2;
                number2 = temp;
            }
            uint mod = 0;
            do
            {
                mod = number1 % number2;
                number1 = number2;
                number2 = mod;
            } while (mod != 0);
            return number1;
        }
    }
    public class LCM
    {
        //formula: number1 * number2 / GCD(number1,number2);
        public static uint FindLCM(uint number1, uint number2)
        {
            return (number1 * number2) / EuclidGCD.FindGCD(number1, number2);
        }
    }
}
