using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Graphs;
using DataStructures.Graphs;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsConnectedComponents
    {
        public static void DoTest()
        {
            var graph = new UndirectedSparseGraph<string>();

            // Add vertices
            var verticesSet1 = new string[] { "a", "b", "c", "d", "e", "f", "s", "v", "x", "y", "z" };
            graph.AddVertices (verticesSet1);

            // Add edges
            // Connected Component #1
            // the vertex "e" won't be connected to any other vertex

            // Connected Component #2
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "d");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");

            // Connected Component #3
            graph.AddEdge("b", "c");
            graph.AddEdge("b", "v");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("f", "b");

            // Connected Component #4
            graph.AddEdge("y", "z");


            // Get connected components
            var connectedComponents = ConnectedComponents.Compute<string>(graph);
            connectedComponents = connectedComponents.OrderBy(item => item.Count).ToList();

            Debug.Assert(connectedComponents.Count == 4);

            // the case of the (e) vertex
            Debug.Assert(connectedComponents[0].Count == 1);
            Debug.Assert(connectedComponents[0][0] == "e");

            // the case of (y) and (z) vertices
            Debug.Assert(connectedComponents[1].Count == 2);
            Debug.Assert(connectedComponents[1].Contains("y"));
            Debug.Assert(connectedComponents[1].Contains("z"));

            // the case of the rest
            Debug.Assert(connectedComponents[2].Count == 4);
            Debug.Assert(connectedComponents[3].Count == 4);
        }
    }
}

