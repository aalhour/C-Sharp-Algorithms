using DataStructures.Graphs;
using System;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsDirectedDenseGraphTest
    {
        [Fact]
        public static void DoTest()
        {
            var graph = new DirectedDenseGraph<string>();

            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };

            graph.AddVertices(verticesSet1);

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

            var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 14, "Wrong edges count.");
            Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.OutgoingEdges("a").ToList().Count == 2, "Wrong outgoing edges from 'a'.");
            Assert.True(graph.OutgoingEdges("s").ToList().Count == 1, "Wrong outgoing edges from 's'.");
            Assert.True(graph.OutgoingEdges("d").ToList().Count == 3, "Wrong outgoing edges from 'd'.");
            Assert.True(graph.OutgoingEdges("x").ToList().Count == 3, "Wrong outgoing edges from 'x'.");
            Assert.True(graph.OutgoingEdges("c").ToList().Count == 3, "Wrong outgoing edges from 'c'.");
            Assert.True(graph.OutgoingEdges("v").ToList().Count == 1, "Wrong outgoing edges from 'v'.");
            Assert.True(graph.OutgoingEdges("f").ToList().Count == 1, "Wrong outgoing edges from 'f'.");
            Assert.True(graph.OutgoingEdges("z").ToList().Count == 0, "Wrong outgoing edges from 'z'.");

            Assert.True(graph.IncomingEdges("a").ToList().Count == 1, "Wrong incoming edges from 'a'.");
            Assert.True(graph.IncomingEdges("s").ToList().Count == 2, "Wrong incoming edges from 's'.");
            Assert.True(graph.IncomingEdges("d").ToList().Count == 2, "Wrong incoming edges from 'd'.");
            Assert.True(graph.IncomingEdges("x").ToList().Count == 1, "Wrong incoming edges from 'x'.");
            Assert.True(graph.IncomingEdges("c").ToList().Count == 3, "Wrong incoming edges from 'c'.");
            Assert.True(graph.IncomingEdges("v").ToList().Count == 1, "Wrong incoming edges from 'v'.");
            Assert.True(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Assert.True(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 11, "Wrong edges count.");

            graph.RemoveVertex("x");
            Assert.True(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 7, "Wrong edges count.");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("x", "a");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");

            // BFS from A
            // Walk the graph using BFS from A:
            Assert.True(graph.BreadthFirstWalk("a").SequenceEqual(new string[] { "a", "z", "s", "x", "d", "c", "f", "v" }));

            // DFS from A
            // Walk the graph using DFS from A:
            Assert.True(graph.DepthFirstWalk("a").SequenceEqual(new string[] { "a", "s", "x", "c", "v", "f", "d", "z" }));

            // BFS from F
            // Walk the graph using BFS from F:
            Assert.True(graph.BreadthFirstWalk("f").SequenceEqual(new string[] { "f", "c", "d", "v", "s", "x", "a", "z" }));


            // DFS from F
            // Walk the graph using DFS from F:
            Assert.True(graph.DepthFirstWalk("f").SequenceEqual(new string[] { "f", "c", "v", "d", "s", "x", "a", "z" }));

            graph.Clear();
            // Cleared the graph from all vertices and edges

            var verticesSet2 = new string[] { "a", "b", "c", "d", "e", "f" };

            graph.AddVertices(verticesSet2);

            graph.AddEdge("a", "b");
            graph.AddEdge("a", "d");
            graph.AddEdge("b", "e");
            graph.AddEdge("d", "b");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("f", "f");

            Assert.True(graph.VerticesCount == 6, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 8, "Wrong edges count.");

            // Walk the graph using DFS:
            Assert.True(graph.DepthFirstWalk().SequenceEqual(new string[] { "a", "d", "e", "c", "f", "b" }));

        }

    }

}

