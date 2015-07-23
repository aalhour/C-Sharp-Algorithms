using System;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Graphs
{
	/// <summary>
	/// The Dense Graph Data Structure.
	/// 
	/// Definition:
	/// A dense graph is a graph G = (V, E) in which |E| = O(|V|^2).
	/// 
	/// This class represents the graph as an adjacency matrix.
	/// </summary>
	public class UndirectedDenseGraph<T> : IUndirectedGraph<T> where T : IComparable<T>
	{
		/// <summary>
		/// INSTANCE VARIABLES
		/// </summary>
		protected virtual int edgesCount { get; set; }
		protected virtual ArrayList<ArrayList<T>> adjacencyMatrix { get; set; }


		/// <summary>
		/// CONSTRUCTORS
		/// </summary>
		public UndirectedDenseGraph()
		{
			edgesCount = 0;
			adjacencyMatrix = new ArrayList<ArrayList<T>> ();
		}


		/// <summary>
		/// Gets the count of vetices.
		/// </summary>
		public int VeticesCount
		{
			get { throw new NotImplementedException (); }
		}

		/// <summary>
		/// Gets the count of edges.
		/// </summary>
		public int EdgesCount
		{
			get { throw new NotImplementedException (); }
		}

		/// <summary>
		/// Returns the list of Vertices.
		/// </summary>
		public DataStructures.Lists.DLinkedList<T> Vertices
		{
			get { throw new NotImplementedException (); }
		}

		/// <summary>
		/// Connects two vertices together.
		/// </summary>
		public bool AddEdge (T firstVertex, T secondVertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Deletes an edge, if exists, between two vertices.
		/// </summary>
		public bool DeleteEdge (T firstVertex, T secondVertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Adds a new vertex to graph.
		/// </summary>
		public bool AddVertex (T vertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Removes the specified vertex from graph.
		/// </summary>
		public bool RemoveVertex (T vertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
		/// </summary>
		public bool AreConnected (T firstVertex, T secondVertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines whether this graph has the specified vertex.
		/// </summary>
		public bool HasVertex (T vertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns the neighbours doubly-linked list for the specified vertex.
		/// </summary>
		public DataStructures.Lists.DLinkedList<T> Neighbours (T vertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns the degree of the specified vertex.
		/// </summary>
		public int Degree (T vertex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns a human-readable string of the graph.
		/// </summary>
		public string ToReadable ()
		{
			throw new NotImplementedException ();
		}

	}

}

