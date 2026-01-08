using DataStructures.Graphs;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedWeightedSparseGraphTest
    {
        [Fact]
        public static void AddVerticesAndEdges_SetsCorrectCounts()
        {
            var graph = CreateTestGraph();
            var allEdges = graph.Edges.ToList();

            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(12, graph.EdgesCount);
            Assert.Equal(12, allEdges.Count);
        }

        [Fact]
        public static void OutgoingEdges_ReturnsCorrectCounts()
        {
            var graph = CreateTestGraph();

            Assert.Equal(3, graph.OutgoingEdges("a").Count());
            Assert.Equal(3, graph.OutgoingEdges("s").Count());
            Assert.Equal(4, graph.OutgoingEdges("x").Count());
            Assert.Equal(4, graph.OutgoingEdges("d").Count());
            Assert.Equal(4, graph.OutgoingEdges("c").Count());
            Assert.Equal(2, graph.OutgoingEdges("v").Count());
            Assert.Equal(3, graph.OutgoingEdges("f").Count());
            Assert.Equal(1, graph.OutgoingEdges("z").Count());
        }

        [Fact]
        public static void IncomingEdges_EqualsOutgoingEdges_ForUndirectedGraph()
        {
            var graph = CreateTestGraph();

            Assert.Equal(3, graph.IncomingEdges("a").Count());
            Assert.Equal(3, graph.IncomingEdges("s").Count());
            Assert.Equal(4, graph.IncomingEdges("x").Count());
            Assert.Equal(4, graph.IncomingEdges("d").Count());
            Assert.Equal(4, graph.IncomingEdges("c").Count());
            Assert.Equal(2, graph.IncomingEdges("v").Count());
            Assert.Equal(3, graph.IncomingEdges("f").Count());
            Assert.Equal(1, graph.IncomingEdges("z").Count());
        }

        [Fact]
        public static void HasEdge_ExistingEdge_ReturnsTrue()
        {
            var graph = CreateTestGraph();

            Assert.True(graph.HasEdge("f", "c"));
            Assert.True(graph.HasEdge("c", "f")); // undirected - both directions
            Assert.True(graph.HasEdge("d", "s"));
        }

        [Fact]
        public static void GetEdgeWeight_ReturnsCorrectWeight()
        {
            var graph = CreateTestGraph();

            Assert.Equal(1, graph.GetEdgeWeight("f", "c"));
            Assert.Equal(3, graph.GetEdgeWeight("d", "s"));
        }

        [Fact]
        public static void AddEdge_Duplicate_ReturnsFalse()
        {
            var graph = CreateTestGraph();

            Assert.False(graph.AddEdge("d", "s", 6));
            Assert.False(graph.AddEdge("c", "f", 12));
            Assert.False(graph.AddEdge("s", "x", 123));
            Assert.False(graph.AddEdge("x", "d", 34));
        }

        [Fact]
        public static void RemoveEdge_RemovesEdge()
        {
            var graph = CreateTestGraph();

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            Assert.False(graph.HasEdge("d", "c"));
            Assert.False(graph.HasEdge("c", "v"));
            Assert.False(graph.HasEdge("a", "z"));
            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(9, graph.EdgesCount);
        }

        [Fact]
        public static void RemoveVertex_RemovesConnectedEdges()
        {
            var graph = CreateTestGraph();
            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            graph.RemoveVertex("x");

            Assert.False(graph.HasEdge("x", "a"));
            Assert.Equal(7, graph.VerticesCount);
            Assert.Equal(5, graph.EdgesCount);
        }

        [Fact]
        public static void BreadthFirstWalk_TraversesGraph()
        {
            var graph = CreateTestGraph();

            var bfsWalk = graph.BreadthFirstWalk("a").ToList();

            Assert.NotEmpty(bfsWalk);
            Assert.Equal("a", bfsWalk[0]);
        }

        [Fact]
        public static void DepthFirstWalk_TraversesGraph()
        {
            var graph = CreateTestGraph();

            var dfsWalk = graph.DepthFirstWalk("a").ToList();

            Assert.NotEmpty(dfsWalk);
            Assert.Equal("a", dfsWalk[0]);
        }

        [Fact]
        public static void Clear_RemovesAllVerticesAndEdges()
        {
            var graph = CreateTestGraph();

            graph.Clear();

            Assert.Equal(0, graph.VerticesCount);
            Assert.Equal(0, graph.EdgesCount);
        }

        [Fact]
        public static void SelfLoop_IsSupported()
        {
            var graph = new UndirectedWeightedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f" });

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("a", "d", 2);
            graph.AddEdge("b", "e", 3);
            graph.AddEdge("d", "b", 1);
            graph.AddEdge("d", "e", 2);
            graph.AddEdge("e", "c", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("f", "f", 1); // self-loop

            Assert.Equal(6, graph.VerticesCount);
            Assert.Equal(8, graph.EdgesCount);
        }

        [Fact]
        public static void DepthFirstWalk_TraversesInCorrectOrder()
        {
            var graph = new UndirectedWeightedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f" });

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("a", "d", 2);
            graph.AddEdge("b", "e", 3);
            graph.AddEdge("d", "b", 1);
            graph.AddEdge("d", "e", 2);
            graph.AddEdge("e", "c", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("f", "f", 1);

            var expected = new[] { "a", "d", "e", "c", "f", "b" };
            Assert.True(graph.DepthFirstWalk().SequenceEqual(expected));
        }

        private static UndirectedWeightedSparseGraph<string> CreateTestGraph()
        {
            var graph = new UndirectedWeightedSparseGraph<string>();
            var vertices = new[] { "a", "z", "s", "x", "d", "c", "f", "v" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);
            graph.AddEdge("s", "x", 3);
            graph.AddEdge("x", "d", 1);
            graph.AddEdge("x", "c", 2);
            graph.AddEdge("x", "a", 3);
            graph.AddEdge("d", "f", 1);
            graph.AddEdge("d", "c", 2);
            graph.AddEdge("d", "s", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("c", "v", 2);
            graph.AddEdge("v", "f", 1);

            return graph;
        }
    }
}
