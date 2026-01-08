using DataStructures.Graphs;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedDenseGraphTest
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
        public static void RemoveVertex_RemovesConnectedEdges()
        {
            var graph = CreateTestGraph();

            graph.RemoveVertex("v");
            graph.RemoveVertex("f");

            Assert.Equal(6, graph.VerticesCount);
            Assert.Equal(6, graph.EdgesCount);
            Assert.False(graph.HasVertex("v"));
            Assert.False(graph.HasVertex("f"));
        }

        [Fact]
        public static void AddVertex_AfterRemove_Works()
        {
            var graph = CreateTestGraph();
            graph.RemoveVertex("v");
            graph.RemoveVertex("f");

            graph.AddVertex("v");
            graph.AddVertex("f");
            graph.AddEdge("d", "f");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");

            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(10, graph.EdgesCount);
            Assert.True(graph.HasVertex("v"));
            Assert.True(graph.HasVertex("f"));
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
        public static void BreadthFirstWalk_TraversesInCorrectOrder()
        {
            var graph = CreateTestGraph();

            var expected = new[] { "s", "a", "x", "z", "d", "c", "f", "v" };
            Assert.True(graph.BreadthFirstWalk("s").SequenceEqual(expected));
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
        public static void DepthFirstWalk_TraversesGraph()
        {
            var graph = new UndirectedDenseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f" });
            graph.AddEdge("a", "b");
            graph.AddEdge("a", "d");
            graph.AddEdge("b", "e");
            graph.AddEdge("d", "b");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("f", "f");

            Assert.Equal(6, graph.VerticesCount);
            Assert.Equal(8, graph.EdgesCount);

            var expected = new[] { "a", "d", "e", "c", "f", "b" };
            Assert.True(graph.DepthFirstWalk().SequenceEqual(expected));
        }

        private static UndirectedDenseGraph<string> CreateTestGraph()
        {
            var graph = new UndirectedDenseGraph<string>();
            graph.AddVertices(new[] { "a", "z", "s", "x", "d", "c", "f", "v" });

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
