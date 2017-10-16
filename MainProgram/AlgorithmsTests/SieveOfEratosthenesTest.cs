using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    class SieveOfEratosthenesTest
    {

        //Test lists
        static List<int> testList = new List<int>();
        static List<int> primesUpTo100List = new List<int>{2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
        static List<int> primesUpTo2List = new List<int> { 2 };
        static List<int> emptyList = new List<int>();

        public static void DoTest()
        {
            //Check if algorithm returns expected lists
            testList = Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(100);
            Debug.Assert(testList.SequenceEqual(primesUpTo100List));

            testList = Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(2);
            Debug.Assert(testList.SequenceEqual(primesUpTo2List));

            testList = Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(1);
            Debug.Assert(testList.SequenceEqual(emptyList));

            testList = Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(-100);
            Debug.Assert(testList.SequenceEqual(emptyList));

        }
    }
}
