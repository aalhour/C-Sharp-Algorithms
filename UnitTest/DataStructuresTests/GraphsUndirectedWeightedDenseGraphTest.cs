using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Graphs;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedWeightedDenseGraphTest
    {
        [Fact]
        public static void DoTest()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();

            var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };

            graph.AddVertices(verticesSet1);

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

            var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 11, "Wrong edges count.");
            Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.OutgoingEdges("a").ToList().Count == 3, "Wrong outgoing edges from 'a'.");
            Assert.True(graph.OutgoingEdges("s").ToList().Count == 3, "Wrong outgoing edges from 's'.");
            Assert.True(graph.OutgoingEdges("x").ToList().Count == 4, "Wrong outgoing edges from 'x'.");
            Assert.True(graph.OutgoingEdges("d").ToList().Count == 4, "Wrong outgoing edges from 'd'.");
            Assert.True(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Assert.True(graph.OutgoingEdges("v").ToList().Count == 1, "Wrong outgoing edges from 'v'.");
            Assert.True(graph.OutgoingEdges("f").ToList().Count == 2, "Wrong outgoing edges from 'f'.");
            Assert.True(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Assert.True(graph.IncomingEdges("a").ToList().Count == 3, "Wrong incoming edges from 'a'.");
            Assert.True(graph.IncomingEdges("s").ToList().Count == 3, "Wrong incoming edges from 's'.");
            Assert.True(graph.IncomingEdges("x").ToList().Count == 4, "Wrong incoming edges from 'x'.");
            Assert.True(graph.IncomingEdges("d").ToList().Count == 4, "Wrong incoming edges from 'd'.");
            Assert.True(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Assert.True(graph.IncomingEdges("v").ToList().Count == 1, "Wrong incoming edges from 'v'.");
            Assert.True(graph.IncomingEdges("f").ToList().Count == 2, "Wrong incoming edges from 'f'.");
            Assert.True(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            var f_to_c = graph.HasEdge("f", "c");
            var f_to_c_weight = graph.GetEdgeWeight("f", "c");
            Assert.True(f_to_c == true, "Edge f->c doesn't exist.");
            Assert.True(f_to_c_weight == 1, "Edge f->c must have a weight of 1.");

            var d_to_s = graph.HasEdge("d", "s");
            var d_to_s_weight = graph.GetEdgeWeight("d", "s");
            Assert.True(d_to_s == true, "Edge d->s doesn't exist.");
            Assert.True(d_to_s_weight == 3, "Edge d->s must have a weight of 3.");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 8, "Wrong edges count.");

            graph.RemoveVertex("x");
            Assert.True(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 4, "Wrong edges count.");

            graph.AddVertex("x");
            graph.AddEdge("s", "x", 3);
            graph.AddEdge("x", "d", 1);
            graph.AddEdge("x", "c", 2);
            graph.AddEdge("x", "a", 3);
            graph.AddEdge("d", "c", 2);
            graph.AddEdge("c", "v", 2);
            graph.AddEdge("a", "z", 2);

            // BFS from A
            // Walk the graph using BFS from A:
            var bfsWalk = graph.BreadthFirstWalk("a");

            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.True(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // DFS from A
            // Walk the graph using DFS from A:
            var dfsWalk = graph.DepthFirstWalk("a");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.True(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // BFS from F
            // Walk the graph using BFS from F:
            bfsWalk = graph.BreadthFirstWalk("f");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.True(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));

            // DFS from F
            // Walk the graph using DFS from F:
            dfsWalk = graph.DepthFirstWalk("f");
            // output: (s) (a) (x) (z) (d) (c) (f) (v)
            Assert.True(bfsWalk.SequenceEqual(new[] { "s", "a", "x", "z", "d", "c", "f", "v" }));


            /********************************************************************/

            graph.Clear();

            var verticesSet2 = new string[] { "a", "b", "c", "d", "e", "f" };

            graph.AddVertices(verticesSet2);

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("a", "d", 2);
            graph.AddEdge("b", "e", 3);
            graph.AddEdge("d", "b", 1);
            graph.AddEdge("d", "e", 2);
            graph.AddEdge("e", "c", 3);
            graph.AddEdge("c", "f", 1);
            graph.AddEdge("f", "f", 1);

            Assert.True(graph.VerticesCount == 6, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 8, "Wrong edges count.");

            // Walk the graph using DFS:
            dfsWalk = graph.DepthFirstWalk();
            // output: (a) (b) (e) (d) (c) (f) 
            Assert.True(bfsWalk.SequenceEqual(new[] { "a", "b", "e", "d", "c", "f" }));

            Console.ReadLine();
        }

    }

}
