using System;
using System.Collections.Generic;

using DataStructures.Lists;

namespace DataStructures.Graphs
{
	/// <summary>
	/// The Sparse Graph Data Structure.
	/// 
	/// Definition:
	/// A sparse graph is a graph G = (V, E) in which |E| = O(|V|).
	/// 
	/// This class represents the graph as an adjacency list (dictionary).
	/// </summary>
	public class UndirectedSparseGraph<T> : IUndirectedGraph<T> where T : IComparable<T>
	{
		/// <summary>
		/// INSTANCE VARIABLES
		/// </summary>
		protected virtual int edgesCount { get; set; }
		protected virtual Dictionary<T, DLinkedList<T>> adjacencyList { get; set; }


		/// <summary>
		/// CONSTRUCTORS
		/// </summary>
		public UndirectedSparseGraph ()
		{
			edgesCount = 0;
			adjacencyList = new Dictionary<T, DLinkedList<T>> ();
		}

		public UndirectedSparseGraph(int size)
		{
			edgesCount = 0;
			adjacencyList = new Dictionary<T, DLinkedList<T>> (size);
		}


		/// <summary>
		/// Gets the count of vetices.
		/// </summary>
		public virtual int VerticesCount
		{
			get { return adjacencyList.Count; }
		}

		/// <summary>
		/// Gets the count of edges.
		/// </summary>
		public virtual int EdgesCount
		{
			get { return edgesCount; }
		}

		/// <summary>
		/// Returns the list of Vertices.
		/// </summary>
		public virtual ArrayList<T> Vertices
		{
			get
			{
				var list = new ArrayList<T> ();
				foreach (var vertex in adjacencyList.Keys)
					list.Add (vertex);
				
				return list;
			}
		}

		/// <summary>
		/// Connects two vertices together.
		/// </summary>
		public virtual bool AddEdge(T firstVertex, T secondVertex)
		{
			bool status = false;

			// Check existence of vertices
			if (adjacencyList.ContainsKey (firstVertex) && adjacencyList.ContainsKey (secondVertex))
			{
				var neighbours = adjacencyList [firstVertex];

				// Check existence of an edge in one list.
				// If edge doesn't exist, add the connecting edge to both vertices' neighbours lists.
				if (!neighbours.Contains (secondVertex))
				{
					adjacencyList [firstVertex].Append(secondVertex);
					adjacencyList [secondVertex].Append (firstVertex);

					// Increment the edges count
					++edgesCount;

					status = true;
				}
			}

			return status;
		}

		/// <summary>
		/// Deletes an edge, if exists, between two vertices.
		/// </summary>
		public virtual bool RemoveEdge(T firstVertex, T secondVertex)
		{
			bool status = false;

			// Check existence of vertices
			if (adjacencyList.ContainsKey (firstVertex) && adjacencyList.ContainsKey (secondVertex))
			{
				var neighbours = adjacencyList [firstVertex];

				// Check existence of an edge in one list.
				// If edge doesn't exist, add the connecting edge to both vertices' neighbours lists.
				if (neighbours.Contains (secondVertex))
				{
					adjacencyList [firstVertex].Remove(secondVertex);
					adjacencyList [secondVertex].Remove(firstVertex);

					// Decrement the edges count
					--edgesCount;

					status = true;
				}
			}

			return status;
		}

		/// <summary>
		/// Adds a new vertex to graph.
		/// </summary>
		public virtual bool AddVertex(T vertex)
		{
			bool status = false;

			// Check existence of vertex
			if (!adjacencyList.ContainsKey (vertex))
			{
				adjacencyList.Add (vertex, new DLinkedList<T> ());
				status = true;
			}

			return status;
		}

		/// <summary>
		/// Removes the specified vertex from graph.
		/// </summary>
		public virtual bool RemoveVertex(T vertex)
		{
			bool status = false;

			// Check existence
			if (adjacencyList.ContainsKey (vertex))
			{
				adjacencyList.Remove (vertex);

				foreach(var adjacent in adjacencyList)
				{
					if (adjacent.Value.Contains (vertex))
					{
						adjacent.Value.Remove (vertex);

						// Decrement the edges count.
						--edgesCount;
					}
				}

				status = true;
			}

			return status;
		}

		/// <summary>
		/// Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
		/// </summary>
		public virtual bool AreConnected(T firstVertex, T secondVertex)
		{
			bool status = false;

			// Check existence of vertices
			if (adjacencyList.ContainsKey (firstVertex) && adjacencyList.ContainsKey (secondVertex))
			{
				// Check the neighbours of only one, because the edge is always added to the two vertices.
				// If the edge is there, it returns true, else it returns false.
				status |= adjacencyList [firstVertex].Contains (secondVertex);	// binary or
			}

			return status;
		}

		/// <summary>
		/// Determines whether this graph has the specified vertex.
		/// </summary>
		public virtual bool HasVertex(T vertex)
		{
			return adjacencyList.ContainsKey (vertex);
		}

		/// <summary>
		/// Returns the neighbours doubly-linked list for the specified vertex.
		/// </summary>
		public virtual DLinkedList<T> Neighbours(T vertex)
		{
			if (adjacencyList.ContainsKey (vertex))
				return adjacencyList [vertex];

			return null;
		}

		/// <summary>
		/// Returns the degree of the specified vertex.
		/// </summary>
		public virtual int Degree(T vertex)
		{
			if (!adjacencyList.ContainsKey (vertex))
				throw new KeyNotFoundException ();

			return adjacencyList [vertex].Count;
		}

		/// <summary>
		/// Returns a human-readable string of the graph.
		/// </summary>
		public virtual string ToReadable()
		{
			string output = string.Empty;

			foreach (var node in adjacencyList)
			{
				var adjacents = string.Empty;

				output = String.Format (
					"{0}\r\n{1}: ["
					, output
					, node.Key
				);

				foreach (var adjacentNode in node.Value)
				{
					adjacents = String.Format ("{0}{1},", adjacents, adjacentNode);
				}

				if(adjacents.Length > 0)
					adjacents.Remove (adjacents.Length - 1);

				output = String.Format ("{0}{1}]", output, adjacents);
			}

			return output;
		}

	}

}

