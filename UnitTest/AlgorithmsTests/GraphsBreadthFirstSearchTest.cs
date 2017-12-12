using System;
using System.Diagnostics;

using DataStructures.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBreadthFirstSearchTest
    {
        [Fact]
        public static void DoTest()
        {
            IGraph<string> graph = new UndirectedSparseGraph<string>();

            // Add vertices
            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };
            graph.AddVertices(verticesSet1);

            // Add edges
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "z");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "f");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");

            // Print the nodes in graph
            //  [*] BFS PrintAll:
            BreadthFirstSearcher.PrintAll(graph, "d");
            string searchResult = null;
            string startFromNode = "d";
            Action<string> writeToConsole = (node) => Trace.Write(String.Format("({0}) ", node));
            Predicate<string> searchPredicate = (node) => node == "f" || node == "c";

            Trace.WriteLine("[*] BFS Visit All Nodes:");
            Trace.WriteLine("Graph traversal started at node: '" + startFromNode + "'");

            BreadthFirstSearcher.VisitAll(ref graph, startFromNode, writeToConsole);

            try
            {
                searchResult = BreadthFirstSearcher.FindFirstMatch(graph, startFromNode, searchPredicate);

                Assert.True(searchResult == "c" || searchResult == "f");

                Trace.WriteLine("[*] BFS Find First Match:");
                Trace.WriteLine(
                    String.Format(
                        "Search result: '{0}'. The search started from node: '{1}'."
                        , searchResult
                        , startFromNode));
            }
            catch (Exception)
            {
                Trace.WriteLine("Search predicate was not matched by any node in the graph.");
            }

            Console.WriteLine("\r\n");
        }

    }

}

