using DataStructures.Graphs;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsDirectedSparseGraphTest
    {
        [Fact]
        public static void AddVerticesAndEdges_SetsCorrectCounts()
        {
            var graph = CreateTestGraph();
            var allEdges = graph.Edges.ToList();

            Assert.Equal(8, graph.VerticesCount);
            Assert.Equal(14, graph.EdgesCount);
            Assert.Equal(14, allEdges.Count);
        }

        [Fact]
        public static void OutgoingEdges_ReturnsCorrectCounts()
        {
            var graph = CreateTestGraph();

            Assert.Equal(2, graph.OutgoingEdges("a").Count());
            Assert.Single(graph.OutgoingEdges("s"));
            Assert.Equal(3, graph.OutgoingEdges("d").Count());
            Assert.Equal(3, graph.OutgoingEdges("x").Count());
            Assert.Equal(3, graph.OutgoingEdges("c").Count());
            Assert.Single(graph.OutgoingEdges("v"));
            Assert.Single(graph.OutgoingEdges("f"));
            Assert.Empty(graph.OutgoingEdges("z"));
        }

        [Fact]
        public static void IncomingEdges_ReturnsCorrectCounts()
        {
            var graph = CreateTestGraph();

            Assert.Single(graph.IncomingEdges("a"));
            Assert.Equal(2, graph.IncomingEdges("s").Count());
            Assert.Equal(2, graph.IncomingEdges("d").Count());
            Assert.Single(graph.IncomingEdges("x"));
            Assert.Equal(3, graph.IncomingEdges("c").Count());
            Assert.Single(graph.IncomingEdges("v"));
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
            Assert.Equal(11, graph.EdgesCount);
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
            Assert.Equal(7, graph.EdgesCount);
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
            var graph = new DirectedSparseGraph<string>();
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

        private static DirectedSparseGraph<string> CreateTestGraph()
        {
            var graph = new DirectedSparseGraph<string>();
            var vertices = new[] { "a", "z", "s", "x", "d", "c", "f", "v" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s");
            graph.AddEdge("a", "z");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("x", "a");
            graph.AddEdge("d", "f");
            graph.AddEdge("d", "c");
            graph.AddEdge("d", "s");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("c", "d");
            graph.AddEdge("v", "f");
            graph.AddEdge("f", "c");

            return graph;
        }
    }
}
