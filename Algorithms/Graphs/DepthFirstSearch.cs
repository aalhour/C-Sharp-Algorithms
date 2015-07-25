using System;
using System.Collections.Generic;

using DataStructures.Graphs;

namespace Algorithms.Graphs
{
	public static class DepthFirstSearch
	{
		/// <summary>
		/// DFS Recursive Helper function. 
		/// Visits the neighbors of a given vertex recusively, and applies the given Action<T> to each one of them.
		/// </summary>
		private static void _visitNeighbors<T>(T Vertex, ref IUndirectedGraph<T> Graph, ref Dictionary<T, object> Parents, Action<T> Action) where T : IComparable<T>
		{
			foreach (var adjacent in Graph.Neighbours(Vertex))
			{
				if (!Parents.ContainsKey (adjacent))
				{
					// DFS VISIT NODE
					Action (adjacent);

					// Save adjacents parent into dictionary
					Parents.Add (adjacent, Vertex);

					// Recusively visit adjacent nodes
					_visitNeighbors (adjacent, ref Graph, ref Parents, Action);
				}
			}
		}

		/// <summary>
		/// Recursive DFS Implementation with helper.
		/// Traverses all the nodes in a graph starting from a specific node, applying the passed action to every node.
		/// </summary>
		public static void VisitAll<T> (ref IUndirectedGraph<T> Graph, T StartVertex, Action<T> Action) where T : IComparable<T>
		{
			if (Graph.VerticesCount == 0)
				return;

			var parents = new Dictionary<T, object> (Graph.VerticesCount);	// keeps track of visited nodes and tree-edges

			foreach (var vertex in Graph.Neighbours(StartVertex))
			{
				if (!parents.ContainsKey (vertex))
				{
					// DFS VISIT NODE
					Action (vertex);

					// Add to parents dictionary
					parents.Add (vertex, null);

					// Visit neighbors using recusrive helper
					_visitNeighbors (vertex, ref Graph, ref parents, Action);
				}
			}
		}

		/// <summary>
		/// Iterative DFS Implementation.
		/// Given a predicate function and a starting node, this function searches the nodes of the graph for a first match.
		/// </summary>
		public static T FindFirstMatch<T> (IUndirectedGraph<T> Graph, T StartVertex, Predicate<T> Match) where T : IComparable<T>
		{
			if (Graph.VerticesCount == 0)
				throw new Exception ("Graph is empty!");

			var stack = new Stack<T> ();
			var parents = new Dictionary<T, object> (Graph.VerticesCount);	// keeps track of visited nodes and tree-edges

			object currentParent = null;
			stack.Push (StartVertex);

			while (stack.Count > 0)
			{
				var current = stack.Pop ();

				// Skip loop if node was already visited
				if (parents.ContainsKey (current))
					continue;

				// Save its parent into the dictionary
				// Mark it as visited
				parents.Add (current, currentParent);

				// DFS VISIT NODE STEP
				if (Match (current))
					return current;

				// Get currents adjacent nodes
				foreach (var adjacent in Graph.Neighbours(current))
					stack.Push (adjacent);

				// Mark current as the father of its adjacents. This helps keep track of tree-nodes.
				currentParent = current;
			}//end-while

			throw new Exception ("Item was not found!");
		}

	}

}

