using System;
using DataStructures.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsDepthFirstSearchTest
    {
        private static IGraph<string> CreateTestGraph()
        {
            //
            // Graph structure:
            //
            //     a --- s --- x --- d --- f
            //     |         / \   / |   /
            //     z        /   \ /  |  /
            //             +-----c---+-+
            //                   |
            //                   v
            //
            var graph = new UndirectedSparseGraph<string>();

            var vertices = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };
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
        public static void VisitAll_VisitsAllConnectedVertices()
        {
            var graph = CreateTestGraph();
            var visitedNodes = new System.Collections.Generic.List<string>();

            DepthFirstSearcher.VisitAll(ref graph, "d", node => visitedNodes.Add(node));

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

            var result = DepthFirstSearcher.FindFirstMatch(graph, "d", node => node == "f" || node == "c");

            Assert.True(result == "c" || result == "f");
        }

        /// <summary>
        /// Note: DepthFirstSearcher.FindFirstMatch throws an Exception when no match is found.
        /// This is by design - use try-catch to handle the "not found" case.
        /// </summary>
        [Fact]
        public static void FindFirstMatch_ThrowsException_WhenNoMatch()
        {
            var graph = CreateTestGraph();

            Assert.Throws<Exception>(() => 
                DepthFirstSearcher.FindFirstMatch(graph, "d", node => node == "nonexistent"));
        }

        [Fact]
        public static void PrintAll_DoesNotThrow()
        {
            var graph = CreateTestGraph();

            // Should not throw
            var exception = Record.Exception(() => DepthFirstSearcher.PrintAll(graph, "d"));
            
            Assert.Null(exception);
        }
    }
}
