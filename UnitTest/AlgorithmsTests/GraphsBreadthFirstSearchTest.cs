using System;
using System.Diagnostics;

using DataStructures.Graphs;
using Algorithms.Graphs;

namespace UnitTest.AlgorithmsTests
{
	public static class GraphsBreadthFirstSearchTest
	{
		public static void DoTest ()
		{
			IGraph<string> graph = new UndirectedSparseGraph<string>();

			// Add vertices
			var verticesSet1 = new string[] { "a", "z", "s", "x", "d", "c", "f", "v" };
			graph.AddVertices (verticesSet1);

			// Add edges
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

            // Print the nodes in graph
            Console.WriteLine(" [*] BFS PrintAll: ");
            BreadthFirstSearcher.PrintAll(graph, "d");
            Console.WriteLine ("\r\n");

			string searchResult = null;
			string startFromNode = "d";
			Action<string> writeToConsole = (node) => Console.Write (String.Format ("({0}) ", node));
			Predicate<string> searchPredicate = (node) => node == "f" || node == "c";

			Console.WriteLine ("[*] BFS Visit All Nodes:");
			Console.WriteLine ("Graph traversal started at node: '" + startFromNode + "'");

			BreadthFirstSearcher.VisitAll (ref graph, startFromNode, writeToConsole);

			Console.WriteLine ("\r\n");

			try 
			{
				searchResult = BreadthFirstSearcher.FindFirstMatch(graph, startFromNode, searchPredicate);

				Debug.Assert(searchResult == "c" || searchResult == "f");

				Console.WriteLine("[*] BFS Find First Match:");
				Console.WriteLine(
					String.Format(
						"Search result: '{0}'. The search started from node: '{1}'."
						, searchResult
						, startFromNode));
			}
			catch(Exception) 
			{
				Console.WriteLine ("Search predicate was not matched by any node in the graph.");
			}

			Console.WriteLine ("\r\n");
		}

	}

}

