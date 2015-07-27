using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Graphs
{
	/// <summary>
	/// The Directed Dense Graph Data Structure.
	/// 
	/// Definition:
	/// A dense graph is a graph G = (V, E) in which |E| = O(|V|^2).
	/// 
	/// This class represents the graph as an incidence-matrix (two dimensional boolean array).
	/// </summary>
	public class DirectedWeightedDenseGraph<T> : DirectedDenseGraph<T>, IWeightedGraph<T> where T : IComparable<T>
	{
		/// <summary>
		/// INSTANCE VARIABLES
		/// </summary>
		private const object EMPTY_VERTEX_SLOT = (object)null;
		private const int EMPTY_EDGE_SLOT = 0;

		protected new int[,] _adjacencyMatrix { get; set; }


		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		public DirectedWeightedDenseGraph (uint capacity = 10)
		{
			_edgesCount = 0;
			_verticesCount = 0;
			_verticesCapacity = (int)capacity;

			_vertices = new ArrayList<object> (_verticesCapacity);
			_adjacencyMatrix = new int[_verticesCapacity, _verticesCapacity];
			_adjacencyMatrix.Populate (rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: EMPTY_EDGE_SLOT);
		}


		/// <summary>
		/// Helper function. Checks if edge exist in graph.
		/// </summary>
		protected override bool _doesEdgeExist(int source, int destination)
		{
			return (_adjacencyMatrix [source, destination] != EMPTY_EDGE_SLOT);
		}

		/// <summary>
		/// Helper function. Gets the weight of a directed edge.
		/// </summary>
		private int _getEdgeWeight(int source, int destination)
		{
			return _adjacencyMatrix [source, destination];
		}

		/// <summary>
		/// Returns true, if graph is weighted; false otherwise.
		/// </summary>
		public override bool IsWeighted
		{
			get { return true; }
		}

		[Obsolete("Use the AddEdge method with the weight parameter.")]
		public new bool AddEdge(T source, T destination)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Connects two vertices together with a weight, in the direction: first->second.
		/// </summary>
		public virtual bool AddEdge(T source, T destination, int weight)
		{
			// Return if the weight is equals to the empty edge value
			if (weight == EMPTY_EDGE_SLOT)
				return false;
			
			// Get indices of vertices
			int srcIndex = _vertices.IndexOf (source);
			int dstIndex = _vertices.IndexOf (destination);

			// Check existence of vertices and non-existence of edge
			if (srcIndex == -1 || dstIndex == -1)
				return false;
			else if (_doesEdgeExist(srcIndex, dstIndex))
				return false;

			_adjacencyMatrix [srcIndex, dstIndex] = weight;

			// Increment edges count
			++_edgesCount;

			return true;
		}

		/// <summary>
		/// Removes edge, if exists, from source to destination.
		/// </summary>
		public override bool RemoveEdge(T source, T destination)
		{
			// Get indices of vertices
			int srcIndex = _vertices.IndexOf (source);
			int dstIndex = _vertices.IndexOf (destination);

			// Check existence of vertices and non-existence of edge
			if (srcIndex == -1 || dstIndex == -1)
				return false;
			else if (!_doesEdgeExist(srcIndex, dstIndex))
				return false;

			_adjacencyMatrix [srcIndex, dstIndex] = EMPTY_EDGE_SLOT;

			// Increment edges count
			--_edgesCount;

			return true;
		}

		/// <summary>
		/// Updates the edge weight from source to destination.
		/// </summary>
		public virtual bool UpdateEdgeWeight(T source, T destination, int weight)
		{
			// Return if the weight is equals to the empty edge value
			if (weight == EMPTY_EDGE_SLOT)
				return false;

			// Get indices of vertices
			int srcIndex = _vertices.IndexOf (source);
			int dstIndex = _vertices.IndexOf (destination);

			// Check existence of vertices and non-existence of edge
			if (srcIndex == -1 || dstIndex == -1)
				return false;
			else if (!_doesEdgeExist(srcIndex, dstIndex))
				return false;

			_adjacencyMatrix [srcIndex, dstIndex] = weight;

			return true;
		}

		/// <summary>
		/// Removes the specified vertex from graph.
		/// </summary>
		public override bool RemoveVertex(T vertex)
		{
			// Return if graph is empty
			if (_verticesCount == 0)
				return false;

			// Get index of vertex
			int index = _vertices.IndexOf(vertex);

			// Return if vertex doesn't exists
			if (index == -1)
				return false;

			// Lazy-delete the vertex from graph
			//_vertices.Remove (vertex);
			_vertices[index] = EMPTY_VERTEX_SLOT;

			// Decrement the vertices count
			--_verticesCount;

			// Remove all outgoing and incoming edges to this vertex
			for (int i = 0; i < _verticesCapacity; ++i) 
			{
				// Source edge
				if (_doesEdgeExist(index, i))
				{
					_adjacencyMatrix [index, i] = EMPTY_EDGE_SLOT;

					// Decrement the edges count
					--_edgesCount;
				}

				// Destination edge
				if (_doesEdgeExist(i, index))
				{
					_adjacencyMatrix [i, index] = EMPTY_EDGE_SLOT;

					// Decrement the edges count
					--_edgesCount;
				}
			}

			return true;
		}

		/// <summary>
		/// Returns the edge weight from source to destination.
		/// </summary>
		public virtual int GetEdgeWeight(T source, T destination)
		{
			// Get indices of vertices
			int srcIndex = _vertices.IndexOf (source);
			int dstIndex = _vertices.IndexOf (destination);

			if (srcIndex == -1 || dstIndex == -1)
				throw new Exception ("One of the vertices don't exist.");
			else if(!_doesEdgeExist(srcIndex, dstIndex))
				throw new Exception ("Edge doesn't exist.");
			
			// Check the existence of vertices and the directed edge
			return _getEdgeWeight(srcIndex, dstIndex);
		}

		/// <summary>
		/// Returns the neighbours of a vertex as a dictionary of nodes-to-weights.
		/// </summary>
		public virtual Dictionary<T, int> NeighboursMap(T vertex)
		{
			var neighbors = new Dictionary<T, int> ();
			int source = _vertices.IndexOf (vertex);

			// Check existence of vertex
			if (source != -1) 
				for (int adjacent = 0; adjacent < _vertices.Count; ++adjacent) 
					if (_vertices[adjacent] != null && _doesEdgeExist(source, adjacent))
						neighbors.Add ((T)_vertices [adjacent], _getEdgeWeight(source, adjacent));

			return neighbors;
		}

		/// <summary>
		/// Returns a human-readable string of the graph.
		/// </summary>
		public override string ToReadable ()
		{
			string output = string.Empty;

			for (int i = 0; i < _vertices.Count; ++i)
			{
				if (_vertices[i] == null)
					continue;

				var node = (T)_vertices[i];
				var adjacents = string.Empty;

				output = String.Format("{0}\r\n{1}: [", output, node);

				// foreach (var adjacentNode in Neighbours(node))
				// 	adjacents = String.Format("{0}{1}({2}), ", adjacents, adjacentNode, _getEdgeWeight(i, _vertices.IndexOf(adjacentNode)));

				foreach(var adjacentNode in NeighboursMap(node))
					adjacents = String.Format("{0}{1}({2}), ", adjacents, adjacentNode.Key, adjacentNode.Value);

				if (adjacents.Length > 0)
					adjacents.Remove(adjacents.Length - 1);

				output = String.Format("{0}{1}]", output, adjacents);
			}

			return output;
		}

		/// <summary>
		/// Clear this graph.
		/// </summary>
		public override void Clear ()
		{
			_edgesCount = 0;
			_verticesCount = 0;
			_vertices = new ArrayList<object> (_verticesCapacity);
			_adjacencyMatrix = new int[_verticesCapacity, _verticesCapacity];
			_adjacencyMatrix.Populate (rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: EMPTY_EDGE_SLOT);
		}

	}

}

