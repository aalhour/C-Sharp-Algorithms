using System;
using System.Linq;
using System.Diagnostics;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class Graphs_DirectedWeightedSparseGraphTest
    {
        public static void DoTest()
        {
            var graph = new DirectedWeightedSparseGraph<string>();

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
            graph.AddEdge("c", "d", 3);
            graph.AddEdge("v", "f", 1);
            graph.AddEdge("f", "c", 2);

            var allEdges = graph.Edges.ToList();

            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 14, "Wrong edges count.");
            Debug.Assert(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Debug.Assert(graph.OutgoingEdges("a").ToList().Count == 2, "Wrong outgoing edges from 'a'.");
            Debug.Assert(graph.OutgoingEdges("s").ToList().Count == 1, "Wrong outgoing edges from 's'.");
            Debug.Assert(graph.OutgoingEdges("d").ToList().Count == 3, "Wrong outgoing edges from 'd'.");
            Debug.Assert(graph.OutgoingEdges("x").ToList().Count == 3, "Wrong outgoing edges from 'x'.");
            Debug.Assert(graph.OutgoingEdges("c").ToList().Count == 3, "Wrong outgoing edges from 'c'.");
            Debug.Assert(graph.OutgoingEdges("v").ToList().Count == 1, "Wrong outgoing edges from 'v'.");
            Debug.Assert(graph.OutgoingEdges("f").ToList().Count == 1, "Wrong outgoing edges from 'f'.");
            Debug.Assert(graph.OutgoingEdges("z").ToList().Count == 0, "Wrong outgoing edges from 'z'.");

            Debug.Assert(graph.IncomingEdges("a").ToList().Count == 1, "Wrong incoming edges from 'a'.");
            Debug.Assert(graph.IncomingEdges("s").ToList().Count == 2, "Wrong incoming edges from 's'.");
            Debug.Assert(graph.IncomingEdges("d").ToList().Count == 2, "Wrong incoming edges from 'd'.");
            Debug.Assert(graph.IncomingEdges("x").ToList().Count == 1, "Wrong incoming edges from 'x'.");
            Debug.Assert(graph.IncomingEdges("c").ToList().Count == 3, "Wrong incoming edges from 'c'.");
            Debug.Assert(graph.IncomingEdges("v").ToList().Count == 1, "Wrong incoming edges from 'v'.");
            Debug.Assert(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Debug.Assert(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            Console.WriteLine("[*] Directed Weighted Sparse Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // ASSERT RANDOMLY SELECTED EDGES
            var f_to_c = graph.HasEdge("f", "c");
            var f_to_c_weight = graph.GetEdgeWeight("f", "c");
            Debug.Assert(f_to_c == true, "Edge f->c doesn't exist.");
            Debug.Assert(f_to_c_weight == 2, "Edge f->c must have a weight of 2.");
            Console.WriteLine("Is there an edge from f to c? " + f_to_c + ". If yes it's weight is: " + f_to_c_weight + ".");

            // ASSERT RANDOMLY SELECTED EDGES
            var d_to_s = graph.HasEdge("d", "s");
            var d_to_s_weight = graph.GetEdgeWeight("d", "s");
            Debug.Assert(d_to_s == true, "Edge d->s doesn't exist.");
            Debug.Assert(d_to_s_weight == 3, "Edge d->s must have a weight of 3.");
            Console.WriteLine("Is there an edge from d to d? " + d_to_s + ". If yes it's weight is: " + d_to_s_weight + ".");

            Console.WriteLine();

            // TRY ADDING DUPLICATE EDGES BUT WITH DIFFERENT WEIGHTS
            var add_d_to_s_status = graph.AddEdge("d", "s", 6);
            Debug.Assert(add_d_to_s_status == false, "Error! Added a duplicate edge.");

            var add_c_to_f_status = graph.AddEdge("c", "f", 12);
            Debug.Assert(add_c_to_f_status == false, "Error! Added a duplicate edge.");

            var add_s_to_x_status = graph.AddEdge("s", "x", 123);
            Debug.Assert(add_s_to_x_status == false, "Error! Added a duplicate edge.");

            var add_x_to_d_status = graph.AddEdge("x", "d", 34);
            Debug.Assert(add_x_to_d_status == false, "Error! Added a duplicate edge.");

            // TEST DELETING EDGES
            graph.RemoveEdge("d", "c");
            Debug.Assert(graph.HasEdge("d", "c") == false, "Error! The edge d->c was deleted.");
            
            graph.RemoveEdge("c", "v");
            Debug.Assert(graph.HasEdge("c", "v") == false, "Error! The edge c->v was deleted.");
            
            graph.RemoveEdge("a", "z");
            Debug.Assert(graph.HasEdge("a", "z") == false, "Error! The edge a->z was deleted.");

            // ASSERT VERTICES AND EDGES COUNT
            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 11, "Wrong edges count.");

            Console.WriteLine("After removing edges (d-c), (c-v), (a-z):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // TEST DELETING VERTICES
            graph.RemoveVertex("x");
            Debug.Assert(graph.HasEdge("x", "a") == false, "Error! The edge x->a was deleted because vertex x was deleted.");

            // ASSERT VERTICES AND EDGES COUNT
            Debug.Assert(graph.VerticesCount == 7, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 7, "Wrong edges count.");

            Console.WriteLine("After removing node (x):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.AddVertex("x");
            graph.AddEdge("s", "x", 3);
            graph.AddEdge("x", "d", 1);
            graph.AddEdge("x", "c", 2);
            graph.AddEdge("x", "a", 3);
            graph.AddEdge("d", "c", 2);
            graph.AddEdge("c", "v", 2);
            graph.AddEdge("a", "z", 2);
            Console.WriteLine("Re-added the deleted vertices and edges to the graph.");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // BFS from A
            Console.WriteLine("Walk the graph using BFS from A:");
            var bfsWalk = graph.BreadthFirstWalk("a");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in bfsWalk) Console.Write(String.Format("({0})", node));
            Console.WriteLine("\r\n");

            // DFS from A
            Console.WriteLine("Walk the graph using DFS from A:");
            var dfsWalk = graph.DepthFirstWalk("a");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));
            Console.WriteLine("\r\n");

            // BFS from F
            Console.WriteLine("Walk the graph using BFS from F:");
            bfsWalk = graph.BreadthFirstWalk("f");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in bfsWalk) Console.Write(String.Format("({0})", node));
            Console.WriteLine("\r\n");

            // DFS from F
            Console.WriteLine("Walk the graph using DFS from F:");
            dfsWalk = graph.DepthFirstWalk("f");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));
            Console.WriteLine("\r\n");

            Console.ReadLine();


            /********************************************************************/


            Console.WriteLine("***************************************************\r\n");

            graph.Clear();
            Console.WriteLine("Cleared the graph from all vertices and edges.\r\n");

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

            Debug.Assert(graph.VerticesCount == 6, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 8, "Wrong edges count.");

            Console.WriteLine("[*] NEW Directed Weighted Sparse Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            Console.WriteLine("Walk the graph using DFS:");
            dfsWalk = graph.DepthFirstWalk();		// output: (a) (b) (e) (d) (c) (f) 
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));

            Console.ReadLine();

        }

    }

}
