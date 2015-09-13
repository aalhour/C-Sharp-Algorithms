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
            //
            // Data Structures Tests
            // Test all graphs after introducing the Edges, OutgoingEdges and IncomingEdges methods
            Graphs_DirectedDenseGraphTest.DoTest();
            Graphs_DirectedSparseGraphTest.DoTest();
            Graphs_DirectedWeightedDenseGraphTest.DoTest();
            Graphs_DirectedWeightedSparseGraphTest.DoTest();

            Graphs_UndirectedDenseGraphTests.DoTest();
            Graphs_UndirectedSparseGraphTest.DoTest();
            Graphs_UndirectedWeightedDenseGraphTest.DoTest();
            Graphs_UndirectedWeightedSparseGraphTest.DoTest();
        }
    }
}
