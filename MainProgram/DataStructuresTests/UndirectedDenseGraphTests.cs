using System;
using System.Diagnostics;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms
{
	public static class UndirectedDenseGraphTests
	{
		public static void DoTest()
		{
			var graph = new UndirectedDenseGraph<string>();

			graph.AddVertex("a");
			graph.AddVertex("z");
			graph.AddVertex("s");
			graph.AddVertex("x");
			graph.AddVertex("d");
			graph.AddVertex("c");
			graph.AddVertex("f");
			graph.AddVertex("v");

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

			Debug.Assert (graph.VerticesCount == 8, "Wrong vertices count.");
			Debug.Assert (graph.EdgesCount == 10, "Wrong edges count.");

			Console.WriteLine ("[*] Undirected Dense Graph:");
			Console.WriteLine ("Graph nodes and edges:");
			Console.WriteLine(graph.ToReadable() + "\r\n");

			graph.RemoveEdge("d", "c");
			graph.RemoveEdge("c", "v");
			graph.RemoveEdge("a", "z");
			Debug.Assert (graph.VerticesCount == 8, "Wrong vertices count.");
			Debug.Assert (graph.EdgesCount == 7, "Wrong edges count.");

			Console.WriteLine ("After removing edges (d-c), (c-v), (a-z):");
			Console.WriteLine(graph.ToReadable() + "\r\n");

			graph.RemoveVertex("x");
			Debug.Assert (graph.VerticesCount == 7, "Wrong vertices count.");
			Debug.Assert (graph.EdgesCount == 4, "Wrong edges count.");

			Console.WriteLine ("After removing node (x):");
			Console.WriteLine(graph.ToReadable() + "\r\n");
		}
	}
}

