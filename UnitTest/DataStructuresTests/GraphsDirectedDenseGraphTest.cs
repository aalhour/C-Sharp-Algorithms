using System;
using System.Linq;
using System.Diagnostics;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class GraphsDirectedDenseGraphTest
    {
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

            Console.WriteLine("[*] Directed Dense Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 11, "Wrong edges count.");

            Console.WriteLine("After removing edges (d-c), (c-v), (a-z):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.RemoveVertex("x");
            Debug.Assert(graph.VerticesCount == 7, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 7, "Wrong edges count.");

            Console.WriteLine("After removing node (x):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("x", "a");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");
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


            graph.Clear();
            Console.WriteLine("Cleared the graph from all vertices and edges.\r\n");

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

            Debug.Assert(graph.VerticesCount == 6, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 8, "Wrong edges count.");

            Console.WriteLine("[*] NEW Directed Dense Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            Console.WriteLine("Walk the graph using DFS:");
            dfsWalk = graph.DepthFirstWalk();		// output: (a) (b) (e) (d) (c) (f) 
            foreach (var node in dfsWalk) Console.Write(String.Format("({0})", node));

            Console.ReadLine();
        }

    }

}

