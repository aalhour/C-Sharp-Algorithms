using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Graphs;
using DataStructures.Graphs;
using DataStructures.Lists;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsTopologicalSorterTest
    {
        public static void DoTest()
        {
            var V01 = new string[] { "A", "B", "C", "D", "E", "X" };
            var DAG01 = new DirectedSparseGraph<string>();

            // Insert new values of V
            DAG01.AddVertices(V01);

            // Insert new value for edges
            DAG01.AddEdge("A", "B");
            DAG01.AddEdge("A", "X");
            DAG01.AddEdge("B", "C");
            DAG01.AddEdge("C", "D");
            DAG01.AddEdge("D", "E");
            DAG01.AddEdge("E", "X");

            // PRINT THE GRAPH
            Console.WriteLine("[*] DAG (Directed Asyclic Graph):");
            Console.WriteLine(DAG01.ToReadable() + "\r\n");

            // CALCULATE THE TOPOLOGICAL SORT
            var topologicalSort01 = TopologicalSorter.Sort<string>(DAG01);
            
            var output01 = string.Empty;
            foreach(var node in topologicalSort01)
                output01 = String.Format("{0}({1}) ", output01, node);

            // PRINT THE TOPOLOGICAL SORT
            Console.WriteLine("Was the previous graph cyclic? " + output01);

            Console.WriteLine("\r\n*********************************************\r\n");


            /**************************************************************************/


            var V02 = new int[] { 7, 5, 3, 11, 8, 2, 9, 10 };
            var DAG02 = new DirectedSparseGraph<int>();

            // Insert new values of V
            DAG02.AddVertices(V02);

            // Insert new value for edges
            DAG02.AddEdge(7, 11);
            DAG02.AddEdge(7, 8);
            DAG02.AddEdge(5, 11);
            DAG02.AddEdge(3, 8);
            DAG02.AddEdge(3, 10);
            DAG02.AddEdge(11, 2);
            DAG02.AddEdge(11, 9);
            DAG02.AddEdge(11, 10);
            DAG02.AddEdge(8, 9);
            
            // PRINT THE GRAPH
            Console.WriteLine("[*] DAG (Directed Asyclic Graph):");
            Console.WriteLine(DAG02.ToReadable() + "\r\n");

            // CALCULATE THE TOPOLOGICAL SORT
            var topologicalSort02 = TopologicalSorter.Sort<int>(DAG02);

            var output02 = string.Empty;
            foreach (var node in topologicalSort02)
                output02 = String.Format("{0}({1}) ", output02, node);

            // PRINT THE TOPOLOGICAL SORT
            Console.WriteLine("Was the previous graph cyclic? " + output02);

            Console.WriteLine("\r\n*********************************************\r\n");

            Console.ReadLine();
        }

    }

}

