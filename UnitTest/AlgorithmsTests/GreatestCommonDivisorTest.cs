using System;

using Algorithms.Numeric;


namespace UnitTest.AlgorithmsTests
{
    public static class GreatestCommonDivisorTest
    {

        public static void DoTest()
        {
            uint[,] testNumbers = new uint[8, 2]
            {
                {0,0 },
                {0,5 },
                {2,4 },
                {3,10 },
                {13,17 },
                {12,20 },
                {22,11 },
                {33,22 }
            };

            for (int i = 0; i < testNumbers.GetLength(0); i++)
            {
                uint gcd = GreatestCommonDivisor.FindGCD(testNumbers[i, 0], testNumbers[i, 1]);

                if (Assert(testNumbers[i,0], testNumbers[i,1], gcd))
                {
                    Console.WriteLine("{0,5}-{1,-5}: with gcd: {2,-5} : Success", testNumbers[i, 0], testNumbers[i, 1], gcd);
                }
                else
                {
                    Console.WriteLine("{0,5}-{1,-5}: with gcd: {2,-5} : Error", testNumbers[i, 0], testNumbers[i, 1], gcd);
                }
            }
        }

        private static bool Assert(uint a, uint b, uint gcdCandidate)
        {
            if (a == 0)
                return b == gcdCandidate;
            else if (b == 0)
                return a == gcdCandidate;

            if (a % gcdCandidate != 0 || b % gcdCandidate != 0)
                return false;

            //Suppose that gcdCandidate is not gcd.
            //With this assumption, if gcdCandidate divides both a and b then it must divide the real gcd
            //Namely, there should be a positive integer
            uint q;
            //such that gcd = q*gcdCandidate

            q = 2;

            uint _gcd = gcdCandidate * q;

            while(_gcd < a && _gcd < b)
            {
                if (a % _gcd == 0 && b % _gcd == 0)
                    return false;
                
                _gcd *= ++q;
            }

            return a > b ? 
                ((a % b == 0) && (b == gcdCandidate)) || ((a % b != 0) && (b != gcdCandidate)) : 
                ((b % a == 0) && (a == gcdCandidate)) || ((b % a != 0) && (a != gcdCandidate));
        }

    }
}
