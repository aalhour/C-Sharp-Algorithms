using System;
using DataStructures.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBreadthFirstSearchTest
    {
        private static IGraph<string> CreateTestGraph()
        {
            IGraph<string> graph = new UndirectedSparseGraph<string>();

            var vertices = new[] { "a", "z", "s", "x", "d", "c", "f", "v" };
            graph.AddVertices(vertices);

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

            return graph;
        }

        [Fact]
        public static void PrintAll_DoesNotThrow()
        {
            var graph = CreateTestGraph();

            var exception = Record.Exception(() => BreadthFirstSearcher.PrintAll(graph, "d"));

            Assert.Null(exception);
        }

        [Fact]
        public static void VisitAll_VisitsAllConnectedVertices()
        {
            var graph = CreateTestGraph();
            var visitedNodes = new System.Collections.Generic.List<string>();

            BreadthFirstSearcher.VisitAll(ref graph, "d", node => visitedNodes.Add(node));

            Assert.Equal(8, visitedNodes.Count);
            Assert.Contains("a", visitedNodes);
            Assert.Contains("z", visitedNodes);
            Assert.Contains("s", visitedNodes);
            Assert.Contains("x", visitedNodes);
            Assert.Contains("d", visitedNodes);
            Assert.Contains("c", visitedNodes);
            Assert.Contains("f", visitedNodes);
            Assert.Contains("v", visitedNodes);
        }

        [Fact]
        public static void FindFirstMatch_ReturnsMatchingNode()
        {
            var graph = CreateTestGraph();

            var result = BreadthFirstSearcher.FindFirstMatch(graph, "d", node => node == "f" || node == "c");

            Assert.True(result == "c" || result == "f");
        }

        /// <summary>
        /// Note: BreadthFirstSearcher.FindFirstMatch throws an Exception when no match is found.
        /// This is by design - use try-catch to handle the "not found" case.
        /// </summary>
        [Fact]
        public static void FindFirstMatch_ThrowsException_WhenNoMatch()
        {
            var graph = CreateTestGraph();

            Assert.Throws<Exception>(() => 
                BreadthFirstSearcher.FindFirstMatch(graph, "d", node => node == "nonexistent"));
        }
    }
}
