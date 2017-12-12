using System;
using System.Linq;
using System.Diagnostics;

using DataStructures.Graphs;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedSparseGraphTest
    {
        [Fact]
        public static void DoTest()
        {
            var graph = new UndirectedSparseGraph<string>();

            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };

            graph.AddVertices(verticesSet1);

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

            var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 10, "Wrong edges count.");
            Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.OutgoingEdges("a").ToList().Count == 2, "Wrong outgoing edges from 'a'.");
            Assert.True(graph.OutgoingEdges("s").ToList().Count == 2, "Wrong outgoing edges from 's'.");
            Assert.True(graph.OutgoingEdges("x").ToList().Count == 3, "Wrong outgoing edges from 'x'.");
            Assert.True(graph.OutgoingEdges("d").ToList().Count == 3, "Wrong outgoing edges from 'd'.");
            Assert.True(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Assert.True(graph.OutgoingEdges("v").ToList().Count == 2, "Wrong outgoing edges from 'v'.");
            Assert.True(graph.OutgoingEdges("f").ToList().Count == 3, "Wrong outgoing edges from 'f'.");
            Assert.True(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Assert.True(graph.IncomingEdges("a").ToList().Count == 2, "Wrong incoming edges from 'a'.");
            Assert.True(graph.IncomingEdges("s").ToList().Count == 2, "Wrong incoming edges from 's'.");
            Assert.True(graph.IncomingEdges("x").ToList().Count == 3, "Wrong incoming edges from 'x'.");
            Assert.True(graph.IncomingEdges("d").ToList().Count == 3, "Wrong incoming edges from 'd'.");
            Assert.True(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Assert.True(graph.IncomingEdges("v").ToList().Count == 2, "Wrong incoming edges from 'v'.");
            Assert.True(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Assert.True(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 7, "Wrong edges count.");


            graph.RemoveVertex("x");
            Assert.True(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 4, "Wrong edges count.");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");

            Assert.True(graph.BreadthFirstWalk("s").SequenceEqual(new string[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            graph.AddVertices(new string[] { "a", "b", "c", "d", "e", "f" });

            graph.AddEdge("a", "b");
            graph.AddEdge("a", "d");
            graph.AddEdge("b", "e");
            graph.AddEdge("d", "b");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("f", "f");

            Assert.True(graph.VerticesCount == 10, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 17, "Wrong edges count.");

            Assert.True(graph.DepthFirstWalk().SequenceEqual(new string[] { "a", "d", "e", "c", "v", "f", "x", "s", "b", "z" }));
        }
    }
}

