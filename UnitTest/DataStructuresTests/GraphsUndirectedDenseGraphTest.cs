using System;
using System.Linq;
using System.Diagnostics;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms
{
    public static class GraphsUndirectedDenseGraphTests
    {
        public static void DoTest()
        {
            var graph = new UndirectedDenseGraph<string>();

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

            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 10, "Wrong edges count.");
            Debug.Assert(graph.EdgesCount == allEdges.Count, "Wrong edges count.");

            Debug.Assert(graph.OutgoingEdges("a").ToList().Count == 2, "Wrong outgoing edges from 'a'.");
            Debug.Assert(graph.OutgoingEdges("s").ToList().Count == 2, "Wrong outgoing edges from 's'.");
            Debug.Assert(graph.OutgoingEdges("x").ToList().Count == 3, "Wrong outgoing edges from 'x'.");
            Debug.Assert(graph.OutgoingEdges("d").ToList().Count == 3, "Wrong outgoing edges from 'd'.");
            Debug.Assert(graph.OutgoingEdges("c").ToList().Count == 4, "Wrong outgoing edges from 'c'.");
            Debug.Assert(graph.OutgoingEdges("v").ToList().Count == 2, "Wrong outgoing edges from 'v'.");
            Debug.Assert(graph.OutgoingEdges("f").ToList().Count == 3, "Wrong outgoing edges from 'f'.");
            Debug.Assert(graph.OutgoingEdges("z").ToList().Count == 1, "Wrong outgoing edges from 'z'.");

            Debug.Assert(graph.IncomingEdges("a").ToList().Count == 2, "Wrong incoming edges from 'a'.");
            Debug.Assert(graph.IncomingEdges("s").ToList().Count == 2, "Wrong incoming edges from 's'.");
            Debug.Assert(graph.IncomingEdges("x").ToList().Count == 3, "Wrong incoming edges from 'x'.");
            Debug.Assert(graph.IncomingEdges("d").ToList().Count == 3, "Wrong incoming edges from 'd'.");
            Debug.Assert(graph.IncomingEdges("c").ToList().Count == 4, "Wrong incoming edges from 'c'.");
            Debug.Assert(graph.IncomingEdges("v").ToList().Count == 2, "Wrong incoming edges from 'v'.");
            Debug.Assert(graph.IncomingEdges("f").ToList().Count == 3, "Wrong incoming edges from 'f'.");
            Debug.Assert(graph.IncomingEdges("z").ToList().Count == 1, "Wrong incoming edges from 'z'.");

            // TEST REMOVE nodes v and f
            graph.RemoveVertex("v");
            graph.RemoveVertex("f");
            Debug.Assert(graph.VerticesCount == 6, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 6, "Wrong edges count.");
            Debug.Assert(graph.HasVertex("v") == false, "Vertex v must have been deleted.");
            Debug.Assert(graph.HasVertex("f") == false, "Vertex f must have been deleted.");

            // TEST RE-ADD REMOVED NODES AND EDGES
            graph.AddVertex("v");
            graph.AddVertex("f");
            graph.AddEdge("d", "f");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");

            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 10, "Wrong edges count.");
            Debug.Assert(graph.HasVertex("v") == true, "Vertex v does not exist.");
            Debug.Assert(graph.HasVertex("f") == true, "Vertex f does not exist.");

            Console.WriteLine("[*] Undirected Dense Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // RE-TEST REMOVE AND ADD NODES AND EDGES
            graph.RemoveEdge("d", "c");
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("a", "z");
            Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 7, "Wrong edges count.");

            Console.WriteLine("After removing edges (d-c), (c-v), (a-z):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.RemoveVertex("x");
            Debug.Assert(graph.VerticesCount == 7, "Wrong vertices count.");
            Debug.Assert(graph.EdgesCount == 4, "Wrong edges count.");

            Console.WriteLine("After removing node (x):");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            graph.AddVertex("x");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "v");
            graph.AddEdge("a", "z");
            Console.WriteLine("Re-added the deleted vertices and edges to the graph.");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // BFS
            Console.WriteLine("Walk the graph using BFS:");
            graph.BreadthFirstWalk("s");		// output: (s) (a) (x) (z) (d) (c) (f) (v)
            Console.WriteLine("\r\n");


            /********************************************************************/


            Console.WriteLine("***************************************************\r\n");

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

            Console.WriteLine("[*] NEW Undirected Dense Graph:");
            Console.WriteLine("Graph nodes and edges:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            Console.WriteLine("Walk the graph using DFS:");
            graph.DepthFirstWalk();		// output: (a) (b) (e) (d) (c) (f) 

        }

    }

}

