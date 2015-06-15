using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Hashing;
using C_Sharp_Algorithms.AlgorithmsTests;
using C_Sharp_Algorithms.DataStructuresTests;

namespace C_Sharp_Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var primes = PrimesList.Instance;

            int[] array = new int[20000];
            primes.CopyTo(array, 15000);
        }
    }
}
