namespace EuclidGCD
{
    public class EuclidGCD
    {
        public static uint FindGCD(uint number1, uint number2)
        {
            if(number2>number1)
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
}
