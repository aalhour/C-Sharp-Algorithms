using System;
using System.Collections.Generic;

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
		protected virtual int _edgesCount { get; set; }
		protected virtual int _verticesCapacity { get; set; }
		protected virtual ArrayList<T> _vertices { get; set; }
		protected virtual T _firstInsertedNode { get; set; }
		protected virtual bool[,] _adjacencyMatrix { get; set; }


		/// <summary>
		/// CONSTRUCTORS
		/// </summary>
		public UndirectedDenseGraph(uint verticesCount = 10)
		{
			_edgesCount = 0;
			_verticesCapacity = (int)verticesCount;
			_vertices = new ArrayList<T> (_verticesCapacity);
			_adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
			_adjacencyMatrix.Populate (rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
		}


		/// <summary>
		/// Helper function that looks up if an edge exists.
		/// </summary>
		private bool _doesEdgeExist(int index1, int index2)
		{
			return (_adjacencyMatrix [index1, index2] || _adjacencyMatrix [index2, index1]);
		}

		/// <summary>
		/// A helper function used in graph traversal algorithsm. Prints the node.
		/// </summary>
		private void _visitNode(T vertex)
		{
			Console.Write (String.Format("({0}) ", vertex));
		}

		/// <summary>
		/// Gets the count of vetices.
		/// </summary>
		public int VerticesCount
		{
			get { return _vertices.Count; }
		}

		/// <summary>
		/// Gets the count of edges.
		/// </summary>
		public int EdgesCount
		{
			get { return _edgesCount; }
		}

		/// <summary>
		/// Returns the list of Vertices.
		/// </summary>
		public DataStructures.Lists.ArrayList<T> Vertices
		{
			get { return _vertices; }
		}

		/// <summary>
		/// Connects two vertices together.
		/// </summary>
		public bool AddEdge (T firstVertex, T secondVertex)
		{
			int indexOfFirst = _vertices.IndexOf (firstVertex);
			int indexOfSecond = _vertices.IndexOf (secondVertex);

			if (indexOfFirst == -1 || indexOfSecond == -1)
				return false;
			else if (_doesEdgeExist (indexOfFirst, indexOfSecond))
				return false;
			
			_adjacencyMatrix [indexOfFirst, indexOfSecond] = true;
			_adjacencyMatrix [indexOfSecond, indexOfFirst] = true;

			// Increment the edges count.
			++_edgesCount;

			return true;
		}

		/// <summary>
		/// Deletes an edge, if exists, between two vertices.
		/// </summary>
		public bool RemoveEdge (T firstVertex, T secondVertex)
		{
			int indexOfFirst = _vertices.IndexOf (firstVertex);
			int indexOfSecond = _vertices.IndexOf (secondVertex);

			if (indexOfFirst == -1 || indexOfSecond == -1)
				return false;
			else if (!_doesEdgeExist (indexOfFirst, indexOfSecond))
				return false;
			
			_adjacencyMatrix [indexOfFirst, indexOfSecond] = false;
			_adjacencyMatrix [indexOfSecond, indexOfFirst] = false;

			// Decrement the edges count.
			--_edgesCount;

			return true;
		}

		/// <summary>
		/// Adds a list of vertices to the graph.
		/// </summary>
		public virtual void AddVertices(IList<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException ();

			foreach (var item in collection)
			{
				this.AddVertex (item);
			}
		}

		/// <summary>
		/// Adds a new vertex to graph.
		/// </summary>
		public bool AddVertex (T vertex)
		{
			// Return if graph reached it's maximum capacity
			if (_vertices.Count == _verticesCapacity)
				return false;
			
			// Return if vertex exists
			if (_vertices.Contains (vertex))
				return false;

			if (_vertices.Count == 0)
				_firstInsertedNode = vertex;
			
			_vertices.Add (vertex);

			return true;
		}

		/// <summary>
		/// Removes the specified vertex from graph.
		/// </summary>
		public bool RemoveVertex (T vertex)
		{
			int index = _vertices.IndexOf (vertex);

			// Return if vertex doesn't exists
			if (index == -1)
				return false;

			_vertices.Remove (vertex);

			for (int i = 0; i < _verticesCapacity; ++i)
			{
				if (_doesEdgeExist (index, i))
				{
					_adjacencyMatrix [index, i] = false;
					_adjacencyMatrix [i, index] = false;

					// Decrement the edges count
					--_edgesCount;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
		/// </summary>
		public bool AreConnected (T firstVertex, T secondVertex)
		{
			int indexOfFirst = _vertices.IndexOf (firstVertex);
			int indexOfSecond = _vertices.IndexOf (secondVertex);

			// Check for existence
			if (indexOfFirst == -1 || indexOfSecond == -1)
				return false;
			
			return _doesEdgeExist (indexOfFirst, indexOfSecond);
		}

		/// <summary>
		/// Determines whether this graph has the specified vertex.
		/// </summary>
		public bool HasVertex (T vertex)
		{
			return _vertices.Contains (vertex);
		}

		/// <summary>
		/// Returns the neighbours doubly-linked list for the specified vertex.
		/// </summary>
		public DataStructures.Lists.DLinkedList<T> Neighbours (T vertex)
		{
			var neighbours = new DLinkedList<T> ();
			int index = _vertices.IndexOf (vertex);

			if (index == -1)
				return neighbours;

			for (int i = 0; i < _vertices.Count; ++i)
			{
				if (_doesEdgeExist(index, i))
					neighbours.Append (_vertices [i]);
			}

			return neighbours;
		}

		/// <summary>
		/// Returns the degree of the specified vertex.
		/// </summary>
		public int Degree (T vertex)
		{
			return Neighbours (vertex).Count;
		}

		/// <summary>
		/// Returns a human-readable string of the graph.
		/// </summary>
		public string ToReadable ()
		{
			string output = string.Empty;

			for (int i = 0; i < _vertices.Count; ++i)
			{
				var node = _vertices [i];
				var adjacents = string.Empty;

				output = String.Format (
					"{0}\r\n{1}: ["
					, output
					, node
				);

				foreach (var adjacentNode in Neighbours(node))
				{
					adjacents = String.Format ("{0}{1},", adjacents, adjacentNode);
				}

				if(adjacents.Length > 0)
					adjacents.Remove (adjacents.Length - 1);

				output = String.Format ("{0}{1}]", output, adjacents);
			}

			return output;
		}

		/// <summary>
		/// Recursive DFS Helper function. Visits the neighbors of a given vertex.
		/// </summary>
		protected virtual void _DFSVisitNeighbors(T fromVertex, ref Dictionary<T, object> parents)
		{
			foreach (var adjacent in Neighbours(fromVertex))
			{
				if (!parents.ContainsKey (adjacent))
				{
					_visitNode (adjacent);
					parents.Add (adjacent, fromVertex);
					_DFSVisitNeighbors (adjacent, ref parents);
				}
			}
		}

		/// <summary>
		/// A depth first search traversal of the graph. Prints nodes as they get visited.
		/// </summary>
		public virtual void DepthFirstSearchWalk()
		{
			if (VerticesCount == 0)
			{
				Console.Write ("Graph is empty!");
				return;
			}

			var parents = new Dictionary<T, object> (VerticesCount);	// keeps track of tree-edges and visited nodes

			foreach (var node in _vertices)
			{
				if (!parents.ContainsKey (node))
				{
					_visitNode(node);
					parents.Add (node, null);
					_DFSVisitNeighbors (node, ref parents);
				}
			}
		}

		/// <summary>
		/// A depth first search traversal of the graph, starting from a specified vertex. Prints nodes as they get visited.
		/// </summary>
		public virtual void DepthFirstSearchWalk(T startingVertex)
		{
			if (VerticesCount == 0)
			{
				Console.Write ("Graph is empty!");
				return;
			}
			else if (!HasVertex (startingVertex))
			{
				Console.Write ("Vertex doesn't exist!");
				return;
			}

			var parents = new Dictionary<T, object> (VerticesCount);
			_DFSVisitNeighbors (startingVertex, ref parents);
		}

		/// <summary>
		/// A breadth first search traversal of the graph. Prints nodes as they get visited.
		/// </summary>
		public virtual void BreadthFirstSearchWalk()
		{
			if (VerticesCount == 0)
			{
				Console.Write ("Graph is empty!");
				return;
			}

			BreadthFirstSearchWalk (_firstInsertedNode);
		}

		/// <summary>
		/// A breadth first search traversal of the graph, starting from a specified vertex. Prints nodes as they get visited.
		/// </summary>
		public virtual void BreadthFirstSearchWalk(T startingVertex)
		{
			if (VerticesCount == 0)
			{
				Console.Write ("Graph is empty!");
				return;
			}
			else if (!HasVertex (startingVertex))
			{
				Console.Write ("Vertex doesn't exist!");
				return;
			}

			int i = 0;
			List<T> frontiers = new List<T> ();
			Dictionary<T, int> levels = new Dictionary<T, int> ();
			Dictionary<T, object> parents = new Dictionary<T, object> ();

			frontiers.Add (startingVertex);		// previous level: i - 1
			levels.Add (startingVertex, 0);		// keeps track of visited nodes
			parents.Add(startingVertex, null);	// keeps track of tree-edges

			// Visit the startingVertex
			_visitNode(startingVertex);

			while (frontiers.Count > 0) 
			{
				var next = new List<T> (); // next-level, i

				foreach (var node in frontiers)
				{
					foreach (var adjacent in Neighbours(node))
					{
						if(!levels.ContainsKey(adjacent))	// not yet visited
						{
							_visitNode (adjacent);
							levels.Add (adjacent, i);		// level[node] + 1
							parents.Add (adjacent, node);	// add it's parent
							next.Add(adjacent);
						}
					}
				}

				frontiers = next;
				i = i + 1;
			}
		}

		/// <summary>
		/// Clear this graph.
		/// </summary>
		public virtual void Clear()
		{
			_edgesCount = 0;
			_vertices.Clear();
			_adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
			_adjacencyMatrix.Populate (rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
		}

	}

}

