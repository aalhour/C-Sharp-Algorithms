using System;

using DataStructures.Graphs;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class UndirectedSparseGraphTests
	{
		public static void DoTest()
		{
			var graph = new UndirectedSparseGraph<string>();

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

			Console.WriteLine(graph.ToReadable());
		}
	}
}

