/***
 * Euclidean Algorithm to find the greatest common divisor of two numbers.
 * 
 */


namespace Algorithms.Numeric
{
    public static class GreatestCommonDivisor
    {
        /// <summary>
        /// Finds and returns the greatest common divisor of two numbers
        /// </summary>
        public static uint FindGCD(uint a, uint b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            uint _a = a, _b = b;
            
            //Bitwise operator '&' works on individual bits of each value
            //result is 0 or 1
            //it works like a modulus operator '%' but is more efficient
            uint r = _a & _b;

            while(r != 0)
            {
                _a = _b;
                _b = r;
                r = _a & _b;
            }

            return _b;
        }

        /// <summary>
        /// Determines given two numbers are relatively prime
        /// </summary>
        public static bool IsRelativelyPrime(uint a, uint b)
        {
            return FindGCD(a, b) == 1;
        }
    }
}
