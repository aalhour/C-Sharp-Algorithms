using System;
using System.Collections.Generic;

using Algorithms.Common;
using DataStructures.Graphs;
using DataStructures.Lists;

namespace Algorithms.Graphs
{
	public class BreadthFirstPaths<T> where T : IComparable<T>
	{
		private int _edgesCount { get; set; }
		private int _verticesCount { get; set; }
		private bool[] _visited { get; set; }
		private Int64[] _distances { get; set; }
		private int[] _predecessors { get; set; }

		// A dictionary that maps node-values to integer indeces
		private Dictionary<T, int> _nodesToIndices { get; set; }

		// A dictionary that maps integer index to node-value
		private Dictionary<int, T> _indicesToNodes { get; set; }

		// A const that represent an infinite distance
		private const Int64 INFINITY = Int64.MaxValue;

		// The index of the source vertex
		private int _sourceVertexIndex { get; set; }


		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		public BreadthFirstPaths (IGraph<T> Graph, T Source)
		{
			if (Graph == null)
				throw new ArgumentNullException ();
			else if (!Graph.HasVertex (Source))
				throw new ArgumentException ("The source vertex doesn't belong to graph.");

			_edgesCount = Graph.EdgesCount;
			_verticesCount = Graph.VerticesCount;

			_visited = new bool[_verticesCount];
			_distances = new Int64[_verticesCount];
			_predecessors = new int[_verticesCount];

			_nodesToIndices = new Dictionary<T, int> ();
			_indicesToNodes = new Dictionary<int, T> ();

			int i = 0;
			foreach (var node in Graph.Vertices) 
			{
				_nodesToIndices.Add (node, i);
				_indicesToNodes.Add (i, node);
				++i;
			}

			_sourceVertexIndex = _nodesToIndices [Source];

			_breadthFirstSearch (Graph, Source);
		}


		/// <summary>
		/// Privat Breadth First Search helper.
		/// </summary>
		private void _breadthFirstSearch(IGraph<T> graph, T source)
		{
			// Reset the visited and distances arrays
			_visited.Populate (false);
			_distances.Populate (INFINITY);

			// Define helper variables.
			var current = source;
			var queue = new DataStructures.Lists.Queue<T> (_verticesCount);

			// Set distance to current to zero
			_distances [_nodesToIndices[current]] = 0;

			// Set current to visited: true.
			_visited [_nodesToIndices[current]] = true;

			queue.Enqueue (current);

			while (!queue.IsEmpty) 
			{
				current = queue.Dequeue ();
				int indexOfCurrent = _nodesToIndices[current];

				foreach (var adjacent in graph.Neighbours(current)) 
				{
					int indexOfAdjacent = _nodesToIndices [adjacent];

					if (!_visited [indexOfAdjacent]) 
					{
						_predecessors [indexOfAdjacent] = indexOfCurrent;
						_distances [indexOfAdjacent] = _distances [indexOfCurrent] + 1;
						_visited [indexOfAdjacent] = true;

						queue.Enqueue (adjacent);
					}
				}//end-foreach
			}//end-while
		}


		/// <summary>
		/// Determines whether there is a path from the source vertex to this specified vertex.
		/// </summary>
		public bool HasPathTo(T destinationVertex)
		{
			if (!_nodesToIndices.ContainsKey (destinationVertex))
				throw new Exception ("Graph doesn't have the specified vertex.");
			
			int indexOfDest = _nodesToIndices [destinationVertex];
			return (_visited [indexOfDest]);
		}

		/// <summary>
		/// Returns the distance between the source vertex and the specified vertex.
		/// </summary>
		public long DistanceTo(T destinationVertex)
		{
			if (!_nodesToIndices.ContainsKey (destinationVertex))
				throw new Exception ("Graph doesn't have the specified vertex.");

			int indexOfDest = _nodesToIndices [destinationVertex];
			return (_distances [indexOfDest]);
		}

		/// <summary>
		/// Returns an enumerable collection of nodes that specify the shortest path from the source vertex to the destination vertex.
		/// </summary>
		public IEnumerable<T> ShortestPathTo(T destinationVertex)
		{
			if (!_nodesToIndices.ContainsKey (destinationVertex))
				throw new Exception ("Graph doesn't have the specified vertex.");
			else if (!HasPathTo (destinationVertex))
				return null;

			int indexOfDest = _nodesToIndices [destinationVertex];

			var stack = new DataStructures.Lists.Stack<T> ();

			int index;
			for (index = indexOfDest; _distances [index] != 0; index = _predecessors [index])
				stack.Push (_indicesToNodes[index]);

			// Push the source vertex
			stack.Push (_indicesToNodes[index]);

			return stack;
		}

	}

}

