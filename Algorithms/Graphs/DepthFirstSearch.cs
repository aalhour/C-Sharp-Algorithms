using System;
using System.Collections.Generic;

using DataStructures.Graphs;

namespace Algorithms
{
	public static class DepthFirstSearch
	{
		private static void DFSRecursive<T>(IUndirectedGraph<T> Graph, T fromVertex) where T : IComparable<T>
		{
			
		}

		public static string VisitAll<T> (IUndirectedGraph<T> Graph, T startVertex) where T : IComparable<T>
		{
			string output = string.Empty;
			Dictionary<T, T> parents = new Dictionary<T, T> ();

			foreach (var vertex in Graph.Vertices)
			{
				if (!parents.ContainsKey (vertex))
				{
					parents.Add (vertex, default(T));
					output = String.Format ("{0}({1}) -> ", output, vertex);
					DFSRecursive (Graph, vertex);
				}
			}

			return output;
		}

		public static T Find<T> (IUndirectedGraph<T> Graph, T startVertex) where T : IComparable<T>
		{
			return default(T);
		}
	}
}

