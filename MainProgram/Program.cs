using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using C_Sharp_Algorithms.AlgorithmsTests;
using C_Sharp_Algorithms.DataStructuresTests;

namespace C_Sharp_Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = "2-4+5*6-1";

			var result = input.Split(new char[] { '-', '+', '*' }).ToList()
				.Select<string, int>(item => Convert.ToInt32(item))
				.ToList<int>();

			var ops = new List<char>();

			for(int i = 0; i < input.Length; i++) {
				if(input[i] == '+' || input[i] == '-' || input[i] == '*') {
					ops.Add (input [i]);
				}
			}
        }
    }
}
