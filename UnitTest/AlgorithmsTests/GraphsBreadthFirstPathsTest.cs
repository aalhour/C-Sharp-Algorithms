using System;
using System.Collections.Generic;
using DataStructures.Graphs;
using Algorithms.Graphs;
using Xunit;
using System.Diagnostics;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBreadthFirstPathsTest
    {
        private static string PrintPath(IEnumerable<string> path)
        {
            string output = string.Empty;

            foreach (var node in path)
                output = String.Format("{0}({1}) -> ", output, node);

            output += "/TERMINATE/";

            return output;
        }

        [Fact]
        public static void DoTest()
        {
            IGraph<string> graph = new UndirectedSparseGraph<string>();

            // Add vertices
            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v", "w", "m" };
            graph.AddVertices(verticesSet1);

            // Add edges
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "z");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("x", "w");
            graph.AddEdge("x", "m");
            graph.AddEdge("d", "f");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");
            graph.AddEdge("w", "m");

            var sourceNode = "f";
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, sourceNode);

            // TODO: 
            // - Assert distances
            // - Assert ShortestPathTo a node

            var distance = bfsPaths.DistanceTo("a");

            Trace.WriteLine("Distance from '" + sourceNode + "' to 'a' is: " + bfsPaths.DistanceTo("a"));
            Trace.WriteLine("Path from '" + sourceNode + "' to 'a' is : " + PrintPath(bfsPaths.ShortestPathTo("a")));

            Trace.WriteLine(string.Empty);

            Trace.WriteLine("Distance from '" + sourceNode + "' to 'w' is: " + bfsPaths.DistanceTo("w"));
            Trace.WriteLine("Path from '" + sourceNode + "' to 'w' is : " + PrintPath(bfsPaths.ShortestPathTo("w")));

        }

    }

}

