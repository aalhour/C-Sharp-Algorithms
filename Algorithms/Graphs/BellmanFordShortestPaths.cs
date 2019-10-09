using System;
using System.Diagnostics;
using System.Collections.Generic;
using Algorithms.Common;
using DataStructures.Graphs;

namespace Algorithms.Graphs
{
    public class BellmanFordShortestPaths<TGraph, TVertex>
        where TGraph : IGraph<TVertex>, IWeightedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private int _edgesCount;
        private int _verticesCount;
        private long[] _distances;
        private int[] _predecessors;
        private WeightedEdge<TVertex>[] _edgeTo;

        // A dictionary that maps node-values to integer indeces
        private Dictionary<TVertex, int> _nodesToIndices;

        // A dictionary that maps integer index to node-value
        private Dictionary<int, TVertex> _indicesToNodes;

        // A const that represent an infinite distance
        private const Int64 Infinity = Int64.MaxValue;
        private const int NilPredecessor = -1;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public BellmanFordShortestPaths(TGraph Graph, TVertex Source)
        {
            if (Graph == null) {
                throw new ArgumentNullException ();
            } else {
                if (!Graph.HasVertex (Source))
                    throw new ArgumentException ("The source vertex doesn't belong to graph.");

                // Init
                _initializeDataMembers (Graph);

                // Traverse the graph
                var status = _bellmanFord (Graph, Source);

                if (status == false)
                    throw new Exception ("Negative-weight cycle detected.");

                Debug.Assert (_checkOptimalityConditions (Graph, Source));
            }
        }


        /************************************************************************************************************/


        /// <summary>
        /// The Bellman-Ford Algorithm.
        /// </summary>
        /// <returns>True if shortest-path computation is finished with no negative-weight cycles detected; otehrwise, false.</returns>
        private bool _bellmanFord(TGraph graph, TVertex source)
        {
            int srcIndex = _nodesToIndices[source];
            _distances[srcIndex] = 0;

            var edges = graph.Edges as IEnumerable<WeightedEdge<TVertex>>;

            // First pass
            // Calculate shortest paths and relax all edges.
            for (int i = 1; i < graph.VerticesCount - 1; ++i)
            {
                foreach (var edge in edges)
                {
                    int fromIndex = _nodesToIndices[edge.Source];
                    int toIndex = _nodesToIndices[edge.Destination];

                    // calculate a new possible weighted path if the edge weight is less than infinity
                    var delta = Infinity;
                    if (edge.Weight < Infinity && (Infinity - edge.Weight) > _distances[fromIndex])  // Handles overflow
                        delta = _distances[fromIndex] + edge.Weight;

                    // Relax the edge
                    // if check is true, a shorter path is found from current to adjacent
                    if (delta < _distances[toIndex])
                    {
                        _edgeTo[toIndex] = edge;
                        _distances[toIndex] = delta;
                        _predecessors[toIndex] = fromIndex;
                    }
                }
            }

            // Second pass
            // Check for negative-weight cycles.
            foreach (var edge in edges)
            {
                int fromIndex = _nodesToIndices[edge.Source];
                int toIndex = _nodesToIndices[edge.Destination];

                // calculate a new possible weighted path if the edge weight is less than infinity
                var delta = Infinity;
                if (edge.Weight < Infinity && (Infinity - edge.Weight) > _distances[fromIndex])  // Handles overflow
                    delta = _distances[fromIndex] + edge.Weight;

                // if check is true a negative-weight cycle is detected
                // return false;
                if (delta < _distances[toIndex])
                    return false;
            }

            // Completed shortest paths computation.
            // No negative edges were detected.
            return true;
        }

        /// <summary>
        /// Constructors helper function. Initializes some of the data memebers.
        /// </summary>
        private void _initializeDataMembers(TGraph Graph)
        {
            _edgesCount = Graph.EdgesCount;
            _verticesCount = Graph.VerticesCount;

            _distances = new Int64[_verticesCount];
            _predecessors = new int[_verticesCount];
            _edgeTo = new WeightedEdge<TVertex>[_edgesCount];

            _nodesToIndices = new Dictionary<TVertex, int>();
            _indicesToNodes = new Dictionary<int, TVertex>();

            // Reset the information arrays
            int i = 0;
            foreach (var node in Graph.Vertices)
            {
                if (i >= _verticesCount)
                    break;

                _edgeTo[i] = null;
                _distances[i] = Infinity;
                _predecessors[i] = NilPredecessor;

                _nodesToIndices.Add(node, i);
                _indicesToNodes.Add(i, node);

                ++i;
            }
        }

        /// <summary>
        /// Constructors helper function. Checks Optimality Conditions:
        /// (i) for all edges e:            distTo[e.to()] <= distTo[e.from()] + e.weight()
        /// (ii) for all edge e on the SPT: distTo[e.to()] == distTo[e.from()] + e.weight()
        /// </summary>
        private bool _checkOptimalityConditions(TGraph graph, TVertex source)
        {
            // Get the source index (to be used with the information arrays).
            int s = _nodesToIndices[source];

            // check that distTo[v] and edgeTo[v] are consistent
            if (_distances[s] != 0 || _predecessors[s] != NilPredecessor)
            {
                Console.WriteLine("distanceTo[s] and edgeTo[s] are inconsistent");
                return false;
            }

            for (int v = 0; v < graph.VerticesCount; v++)
            {
                if (v == s) continue;

                if (_predecessors[v] == NilPredecessor && _distances[v] != Infinity)
                {
                    Console.WriteLine("distanceTo[] and edgeTo[] are inconsistent for at least one vertex.");
                    return false;
                }
            }

            // check that all edges e = v->w satisfy distTo[w] <= distTo[v] + e.weight()
            foreach (var vertex in graph.Vertices)
            {
                int v = _nodesToIndices[vertex];

                foreach (var edge in graph.NeighboursMap(vertex))
                {
                    int w = _nodesToIndices[edge.Key];

                    if (_distances[v] + edge.Value < _distances[w])
                    {
                        Console.WriteLine("edge " + vertex + "-" + edge.Key + " is not relaxed");
                        return false;
                    }
                }
            }

            // check that all edges e = v->w on SPT satisfy distTo[w] == distTo[v] + e.weight()
            foreach (var vertex in graph.Vertices)
            {
                int w = _nodesToIndices[vertex];

                if (_edgeTo[w] == null)
                    continue;

                var edge = _edgeTo[w];
                int v = _nodesToIndices[edge.Source];

                if (!vertex.IsEqualTo(edge.Destination))
                    return false;

                if ((_distances[v] + edge.Weight) != _distances[w])
                {
                    Console.WriteLine("edge " + edge.Source + "-" + edge.Destination + " on shortest path not tight");
                    return false;
                }
            }

            return true;
        }


        /************************************************************************************************************/


        /// <summary>
        /// Determines whether there is a path from the source vertex to this specified vertex.
        /// </summary>
        public bool HasPathTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");

            int index = _nodesToIndices[destination];
            return _distances[index] != Infinity;
        }

        /// <summary>
        /// Returns the distance between the source vertex and the specified vertex.
        /// </summary>
        public long DistanceTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");

            int index = _nodesToIndices[destination];
            return _distances[index];
        }

        /// <summary>
        /// Returns an enumerable collection of nodes that specify the shortest path from the source vertex to the destination vertex.
        /// </summary>
        public IEnumerable<TVertex> ShortestPathTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");
            if (!HasPathTo(destination))
                return null;

            int dstIndex = _nodesToIndices[destination];
            var stack = new DataStructures.Lists.Stack<TVertex>();

            int index;
            for (index = dstIndex; _distances[index] != 0; index = _predecessors[index])
                stack.Push(_indicesToNodes[index]);

            // Push the source vertex
            stack.Push(_indicesToNodes[index]);

            return stack;
        }

    }

}
