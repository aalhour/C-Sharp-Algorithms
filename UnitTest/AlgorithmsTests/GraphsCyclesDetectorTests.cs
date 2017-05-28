using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Graphs;
using DataStructures.Graphs;
using DataStructures.Lists;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsCyclesDetectorTests
    {
        public static void DoTest()
        {
            string[] V;
            DirectedSparseGraph<string> DAG;
            UndirectedSparseGraph<string> CyclicGraph;
            DirectedSparseGraph<string> DigraphWithCycles;

            // Init graph object
            DigraphWithCycles = new DirectedSparseGraph<string>();

            // Init V
            V = new string[6] { "r", "s", "t", "x", "y", "z" };

            // Insert V
            DigraphWithCycles.AddVertices(V);

            // Insert E
            DigraphWithCycles.AddEdge("r", "s");
            DigraphWithCycles.AddEdge("r", "t");
            DigraphWithCycles.AddEdge("s", "t");
            DigraphWithCycles.AddEdge("s", "x");
            DigraphWithCycles.AddEdge("t", "x");
            DigraphWithCycles.AddEdge("t", "y");
            DigraphWithCycles.AddEdge("t", "z");
            DigraphWithCycles.AddEdge("x", "y");
            DigraphWithCycles.AddEdge("x", "z");
            DigraphWithCycles.AddEdge("y", "z");
            DigraphWithCycles.AddEdge("z", "r");
            DigraphWithCycles.AddEdge("z", "s");
            
            var isCyclic = CyclesDetector.IsCyclic<string>(DigraphWithCycles);
            Debug.Assert(isCyclic == true, "Wrong status! The graph has cycles.");

            // PRINT THE GRAPH
            Console.WriteLine("[*] Directed Graph:");
            Console.WriteLine(DigraphWithCycles.ToReadable() + "\r\n");
            Console.WriteLine("Was the previous graph cyclic? " + isCyclic);

            Console.WriteLine("\r\n*********************************************\r\n");


            /***************************************************************************************/


            CyclicGraph = new UndirectedSparseGraph<string>();

            V = new string[] { "A", "B", "C", "D", "E" };

            // Insert new values of V
            CyclicGraph.AddVertices(V);
            
            // Insert new value for edges
            CyclicGraph.AddEdge("A", "C");
            CyclicGraph.AddEdge("B", "A");
            CyclicGraph.AddEdge("B", "C");
            CyclicGraph.AddEdge("C", "E");
            CyclicGraph.AddEdge("C", "D");
            CyclicGraph.AddEdge("D", "B");
            CyclicGraph.AddEdge("E", "D");

            isCyclic = CyclesDetector.IsCyclic<string>(CyclicGraph);
            Debug.Assert(isCyclic == true, "Wrong status! The graph has cycles.");

            // PRINT THE GRAPH
            Console.WriteLine("[*] Undirected Graph:");
            Console.WriteLine(CyclicGraph.ToReadable() + "\r\n");
            Console.WriteLine("Was the previous graph cyclic? " + isCyclic);

            Console.WriteLine("\r\n*********************************************\r\n");


            /***************************************************************************************/


            DAG = new DirectedSparseGraph<string>();

            V = new string[] { "A", "B", "C", "D", "E", "X" };

            // Insert new values of V
            DAG.AddVertices(V);

            // Insert new value for edges
            DAG.AddEdge("A", "B");
            DAG.AddEdge("A", "X");
            DAG.AddEdge("B", "C");
            DAG.AddEdge("C", "D");
            DAG.AddEdge("D", "E");
            DAG.AddEdge("E", "X");

            isCyclic = CyclesDetector.IsCyclic<string>(DAG);
            Debug.Assert(isCyclic == false, "Wrong status! The graph has no cycles.");

            // PRINT THE GRAPH
            Console.WriteLine("[*] DAG (Directed Asyclic Graph):");
            Console.WriteLine(DAG.ToReadable() + "\r\n");
            Console.WriteLine("Was the previous graph cyclic? " + isCyclic);

            Console.ReadLine();
        }

    }

}
