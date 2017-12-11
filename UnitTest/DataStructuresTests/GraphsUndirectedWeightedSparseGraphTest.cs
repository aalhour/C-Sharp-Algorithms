using DataStructures.Graphs;
using System;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class GraphsUndirectedWeightedSparseGraphTest
    {
        [Fact]
        public static void DoTest()
        {
            var graph = new UndirectedWeightedSparseGraph<string>();

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
            graph.AddEdge("v", "f", 1);

            var allEdges = graph.Edges.ToList();

            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 12, "Wrong edges count.");
            Assert.True(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Assert.True(graph.OutgoingEdges("a").ToList().Count == 3, "Wrong outgoing edges from 'a'.");
            Assert.True(graph.OutgoingEdges("s").ToList().Count == 3, "Wrong outgoing edges from 's'.");
            Assert.True(graph.OutgoingEdges("x").ToList().Count == 4, "Wrong outgoing edges from 'x'.");
            Assert.True(graph.OutgoingEdges("d").ToList().Count == 4, "Wrong outgoing edges from 'd'.");
            Assert.True(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Assert.True(graph.OutgoingEdges("v").ToList().Count == 2, "Wrong outgoing edges from 'v'.");
            Assert.True(graph.OutgoingEdges("f").ToList().Count == 3, "Wrong outgoing edges from 'f'.");
            Assert.True(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Assert.True(graph.IncomingEdges("a").ToList().Count == 3, "Wrong incoming edges from 'a'.");
            Assert.True(graph.IncomingEdges("s").ToList().Count == 3, "Wrong incoming edges from 's'.");
            Assert.True(graph.IncomingEdges("x").ToList().Count == 4, "Wrong incoming edges from 'x'.");
            Assert.True(graph.IncomingEdges("d").ToList().Count == 4, "Wrong incoming edges from 'd'.");
            Assert.True(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Assert.True(graph.IncomingEdges("v").ToList().Count == 2, "Wrong incoming edges from 'v'.");
            Assert.True(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Assert.True(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            // ASSERT RANDOMLY SELECTED EDGES
            var f_to_c = graph.HasEdge("f", "c");
            var f_to_c_weight = graph.GetEdgeWeight("f", "c");
            Assert.True(f_to_c == true, "Edge f->c doesn't exist.");
            Assert.True(f_to_c_weight == 1, "Edge f->c must have a weight of 2.");
            Console.WriteLine("Is there an edge from f to c? " + f_to_c + ". If yes it's weight is: " + f_to_c_weight + ".");

            // ASSERT RANDOMLY SELECTED EDGES
            var d_to_s = graph.HasEdge("d", "s");
            var d_to_s_weight = graph.GetEdgeWeight("d", "s");
            Assert.True(d_to_s == true, "Edge d->s doesn't exist.");
            Assert.True(d_to_s_weight == 3, "Edge d->s must have a weight of 3.");

            // TRY ADDING DUPLICATE EDGES BUT WITH DIFFERENT WEIGHTS
            var add_d_to_s_status = graph.AddEdge("d", "s", 6);
            Assert.True(add_d_to_s_status == false, "Error! Added a duplicate edge.");

            var add_c_to_f_status = graph.AddEdge("c", "f", 12);
            Assert.True(add_c_to_f_status == false, "Error! Added a duplicate edge.");

            var add_s_to_x_status = graph.AddEdge("s", "x", 123);
            Assert.True(add_s_to_x_status == false, "Error! Added a duplicate edge.");

            var add_x_to_d_status = graph.AddEdge("x", "d", 34);
            Assert.True(add_x_to_d_status == false, "Error! Added a duplicate edge.");

            // TEST DELETING EDGES
            graph.RemoveEdge("d", "c");
            Assert.True(graph.HasEdge("d", "c") == false, "Error! The edge d->c was deleted.");

            graph.RemoveEdge("c", "v");
            Assert.True(graph.HasEdge("c", "v") == false, "Error! The edge c->v was deleted.");

            graph.RemoveEdge("a", "z");
            Assert.True(graph.HasEdge("a", "z") == false, "Error! The edge a->z was deleted.");

            // ASSERT VERTICES AND EDGES COUNT
            Assert.True(graph.VerticesCount == 8, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 9, "Wrong edges count.");

            Console.WriteLine("After removing edges (d-c), (c-v), (a-z):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // TEST DELETING VERTICES
            graph.RemoveVertex("x");
            Assert.True(graph.HasEdge("x", "a") == false, "Error! The edge x->a was deleted because vertex x was deleted.");

            // ASSERT VERTICES AND EDGES COUNT
            Assert.True(graph.VerticesCount == 7, "Wrong vertices count.");
            Assert.True(graph.EdgesCount == 5, "Wrong edges count.");

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
            var bfsWalk = graph.BreadthFirstWalk("a");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in bfsWalk) Console.Write(String.Format("({0})", node));

            // DFS from A
            // Walk the graph using DFS from A:
            var dfsWalk = graph.DepthFirstWalk("a");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));

            // BFS from F
            // Walk the graph using BFS from F:
            bfsWalk = graph.BreadthFirstWalk("f");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in bfsWalk) Console.Write(String.Format("({0})", node));
            Console.WriteLine("\r\n");

            // DFS from F
            // Walk the graph using DFS from F:
            dfsWalk = graph.DepthFirstWalk("f");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));

            /********************************************************************/

            graph.Clear();
            // "Cleared the graph from all vertices and edges.\r\n"

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
            Assert.True(graph.DepthFirstWalk().SequenceEqual(new[] { "a", "d", "e", "c", "f", "b" }));
        }

    }

}
