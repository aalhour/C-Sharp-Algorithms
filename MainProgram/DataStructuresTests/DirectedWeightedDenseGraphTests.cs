using System;
using System.Diagnostics;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class DirectedWeightedDenseGraphTests
	{
		public static void DoTest()
		{
			var graph = new DirectedWeightedDenseGraph<string>();

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

			Debug.Assert(graph.VerticesCount == 8, "Wrong vertices count.");
			Debug.Assert(graph.EdgesCount == 14, "Wrong edges count.");

			Console.WriteLine("[*] Directed Dense Graph:");
			Console.WriteLine("Graph nodes and edges:");
			Console.WriteLine(graph.ToReadable() + "\r\n");

			var f_to_c = graph.HasEdge ("f", "c");
			var f_to_c_weight = graph.GetEdgeWeight ("f", "c");
			Debug.Assert (f_to_c == true, "Edge f->c doesn't exist.");
			Debug.Assert (f_to_c_weight == 2, "Edge f->c must have a weight of 2.");
			Console.WriteLine ("Is there an edge from f to c? " + f_to_c + ". If yes it's weight is: " + f_to_c_weight + ".");

			var d_to_s = graph.HasEdge ("d", "s");
			var d_to_s_weight = graph.GetEdgeWeight ("d", "s");
			Debug.Assert (d_to_s == true, "Edge d->s doesn't exist.");
			Debug.Assert (d_to_s_weight == 3, "Edge d->s must have a weight of 3.");
			Console.WriteLine ("Is there an edge from d to d? " + d_to_s + ". If yes it's weight is: " + d_to_s_weight + ".");

			Console.WriteLine ();

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

