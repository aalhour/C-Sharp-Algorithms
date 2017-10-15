using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    class SieveOfEratosthenesTest
    {
        public static void DoTest()
        {
            foreach (int i in Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(100))
            {
                Console.Write(i + " ");
            }

            foreach (int i in Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(-100))
            {
                Console.Write(i + " ");
            }

            foreach (int i in Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(2))
            {
                Console.Write(i + " ");
            }

            foreach (int i in Algorithms.Numeric.SieveOfEratosthenes.GeneratePrimesUpTo(1))
            {
                Console.Write(i + " ");
            }
        }
    }
}
