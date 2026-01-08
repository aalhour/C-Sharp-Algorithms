using System.Linq;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedSparseGraphTest
    {
        [Fact]
        public static void AddVerticesAndEdges_SetsCorrectCounts()
        {
            var graph = CreateTestGraph();
            var allEdges = graph.Edges.ToList();

            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(10, graph.EdgesCount);
            Assert.Equal(10, allEdges.Count);
        }

        [Fact]
        public static void OutgoingEdges_ReturnsCorrectCounts()
        {
            var graph = CreateTestGraph();

            Assert.Equal(2, graph.OutgoingEdges("a").Count());
            Assert.Equal(2, graph.OutgoingEdges("s").Count());
            Assert.Equal(3, graph.OutgoingEdges("x").Count());
            Assert.Equal(3, graph.OutgoingEdges("d").Count());
            Assert.Equal(4, graph.OutgoingEdges("c").Count());
            Assert.Equal(2, graph.OutgoingEdges("v").Count());
            Assert.Equal(3, graph.OutgoingEdges("f").Count());
            Assert.Single(graph.OutgoingEdges("z"));
        }

        [Fact]
        public static void IncomingEdges_EqualsOutgoingEdges_ForUndirectedGraph()
        {
            var graph = CreateTestGraph();

            // In undirected graphs, incoming = outgoing
            Assert.Equal(2, graph.IncomingEdges("a").Count());
            Assert.Equal(2, graph.IncomingEdges("s").Count());
            Assert.Equal(3, graph.IncomingEdges("x").Count());
            Assert.Equal(3, graph.IncomingEdges("d").Count());
            Assert.Equal(4, graph.IncomingEdges("c").Count());
            Assert.Equal(2, graph.IncomingEdges("v").Count());
            Assert.Equal(3, graph.IncomingEdges("f").Count());
            Assert.Single(graph.IncomingEdges("z"));
        }

        [Fact]
        public static void RemoveEdge_UpdatesCounts()
        {
            var graph = CreateTestGraph();

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(7, graph.EdgesCount);
        }

        [Fact]
        public static void RemoveVertex_RemovesConnectedEdges()
        {
            var graph = CreateTestGraph();
            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            graph.RemoveVertex("x");

            Assert.Equal(7, graph.VerticesCount);
            Assert.Equal(4, graph.EdgesCount);
        }

        [Fact]
        public static void BreadthFirstWalk_TraversesInCorrectOrder()
        {
            var graph = CreateTestGraph();
            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            graph.RemoveVertex("x");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");

            var expected = new[] { "s", "a", "x", "z", "d", "c", "f", "v" };
            Assert.True(graph.BreadthFirstWalk("s").SequenceEqual(expected));
        }

        [Fact]
        public static void SelfLoop_IsSupported()
        {
            var graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f" });

            graph.AddEdge("a", "b");
            graph.AddEdge("a", "d");
            graph.AddEdge("b", "e");
            graph.AddEdge("d", "b");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("f", "f"); // self-loop

            Assert.Equal(6, graph.VerticesCount);
            Assert.Equal(8, graph.EdgesCount);
        }

        private static UndirectedSparseGraph<string> CreateTestGraph()
        {
            var graph = new UndirectedSparseGraph<string>();
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
    }
}
