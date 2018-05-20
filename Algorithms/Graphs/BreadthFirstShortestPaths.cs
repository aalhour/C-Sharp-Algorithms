/***
 * Computes Shortest-Paths for Unweighted Graphs using the Breadth-First Search algorithm.
 * It provides the capability to find shortest-paths from a single-source and multiple-sources, in addition to looking up reachable and unreachable nodes.
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Common;
using DataStructures.Graphs;

namespace Algorithms.Graphs
{
    public class BreadthFirstShortestPaths<T> where T : IComparable<T>
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


        /// <summary>
        /// CONSTRUCTOR.
        /// Breadth First Searcher from Single Source.
        /// </summary>
        public BreadthFirstShortestPaths(IGraph<T> Graph, T Source)
        {
            if (Graph == null)
                throw new ArgumentNullException();
            if (!Graph.HasVertex(Source))
                throw new ArgumentException("The source vertex doesn't belong to graph.");

            // Init
            _initializeDataMembers(Graph);

            // Single source BFS
            _breadthFirstSearch(Graph, Source);

            //bool optimalityConditionsSatisfied = checkOptimalityConditions (Graph, Source);
            Debug.Assert(checkOptimalityConditions(Graph, Source));
        }


        /// <summary>
        /// CONSTRUCTOR.
        /// Breadth First Searcher from Multiple Sources.
        /// </summary>
        public BreadthFirstShortestPaths(IGraph<T> Graph, IList<T> Sources)
        {
            if (Graph == null)
                throw new ArgumentNullException();
            if (Sources == null || Sources.Count == 0)
                throw new ArgumentException("Sources list is either null or empty.");

            // Init
            _initializeDataMembers(Graph);

            // Multiple sources BFS
            _breadthFirstSearch(Graph, Sources);
        }


        /************************************************************************************************************/


        /// <summary>
        /// Constructors helper function. Initializes some of the data memebers.
        /// </summary>
        private void _initializeDataMembers(IGraph<T> Graph)
        {
            _edgesCount = Graph.EdgesCount;
            _verticesCount = Graph.VerticesCount;

            _visited = new bool[_verticesCount];
            _distances = new Int64[_verticesCount];
            _predecessors = new int[_verticesCount];

            _nodesToIndices = new Dictionary<T, int>();
            _indicesToNodes = new Dictionary<int, T>();

            // Reset the visited, distances and predeccessors arrays
            int i = 0;
            foreach (var node in Graph.Vertices)
            {
                if (i >= _verticesCount)
                    break;

                _visited[i] = false;
                _distances[i] = INFINITY;
                _predecessors[i] = -1;

                _nodesToIndices.Add(node, i);
                _indicesToNodes.Add(i, node);

                ++i;
            }
        }

        /// <summary>
        /// Privat helper. Breadth First Search from Single Source.
        /// </summary>
        private void _breadthFirstSearch(IGraph<T> graph, T source)
        {
            // Set distance to current to zero
            _distances[_nodesToIndices[source]] = 0;

            // Set current to visited: true.
            _visited[_nodesToIndices[source]] = true;

            var queue = new Queue<T>(_verticesCount);
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int indexOfCurrent = _nodesToIndices[current];

                foreach (var adjacent in graph.Neighbours(current))
                {
                    int indexOfAdjacent = _nodesToIndices[adjacent];

                    if (!_visited[indexOfAdjacent])
                    {
                        _predecessors[indexOfAdjacent] = indexOfCurrent;
                        _distances[indexOfAdjacent] = _distances[indexOfCurrent] + 1;
                        _visited[indexOfAdjacent] = true;

                        queue.Enqueue(adjacent);
                    }
                }//end-foreach
            }//end-while
        }

        /// <summary>
        /// Privat helper. Breadth First Search from Multiple Sources.
        /// </summary>
        private void _breadthFirstSearch(IGraph<T> graph, IList<T> sources)
        {
            // Define helper variables.
            var queue = new Queue<T>(_verticesCount);

            foreach (var source in sources)
            {
                if (!graph.HasVertex(source))
                    throw new Exception("Graph doesn't has a vertex '" + source + "'");

                int index = _nodesToIndices[source];
                _distances[index] = 0;
                _visited[index] = true;
                queue.Enqueue(source);
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int indexOfCurrent = _nodesToIndices[current];

                foreach (var adjacent in graph.Neighbours(current))
                {
                    int indexOfAdjacent = _nodesToIndices[adjacent];

                    if (!_visited[indexOfAdjacent])
                    {
                        _predecessors[indexOfAdjacent] = indexOfCurrent;
                        _distances[indexOfAdjacent] = _distances[indexOfCurrent] + 1;
                        _visited[indexOfAdjacent] = true;

                        queue.Enqueue(adjacent);
                    }
                }//end-foreach
            }//end-while
        }

        /// <summary>
        /// Private helper. Checks optimality conditions for single source
        /// </summary>
        private bool checkOptimalityConditions(IGraph<T> graph, T source)
        {
            int indexOfSource = _nodesToIndices[source];

            // check that the distance of s = 0
            if (_distances[indexOfSource] != 0)
            {
                Console.WriteLine("Distance of source '" + source + "' to itself = " + _distances[indexOfSource]);
                return false;
            }

            // check that for each edge v-w dist[w] <= dist[v] + 1
            // provided v is reachable from s
            foreach (var node in graph.Vertices)
            {
                int v = _nodesToIndices[node];

                foreach (var adjacent in graph.Neighbours(node))
                {
                    int w = _nodesToIndices[adjacent];

                    if (HasPathTo(node) != HasPathTo(adjacent))
                    {
                        Console.WriteLine("edge " + node + "-" + adjacent);
                        Console.WriteLine("hasPathTo(" + node + ") = " + HasPathTo(node));
                        Console.WriteLine("hasPathTo(" + adjacent + ") = " + HasPathTo(adjacent));
                        return false;
                    }
                    if (HasPathTo(node) && (_distances[w] > _distances[v] + 1))
                    {
                        Console.WriteLine("edge " + node + "-" + adjacent);
                        Console.WriteLine("distanceTo[" + node + "] = " + _distances[v]);
                        Console.WriteLine("distanceTo[" + adjacent + "] = " + _distances[w]);
                        return false;
                    }
                }
            }

            // check that v = edgeTo[w] satisfies distTo[w] + distTo[v] + 1
            // provided v is reachable from source
            foreach (var node in graph.Vertices)
            {
                int w = _nodesToIndices[node];

                if (!HasPathTo(node) || node.IsEqualTo(source))
                    continue;

                int v = _predecessors[w];

                if (_distances[w] != _distances[v] + 1)
                {
                    Console.WriteLine("shortest path edge " + v + "-" + w);
                    Console.WriteLine("distanceTo[" + v + "] = " + _distances[v]);
                    Console.WriteLine("distanceTo[" + w + "] = " + _distances[w]);
                    return false;
                }
            }

            return true;
        }


        /************************************************************************************************************/


        /// <summary>
        /// Determines whether there is a path from the source vertex to this specified vertex.
        /// </summary>
        public bool HasPathTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");

            int dstIndex = _nodesToIndices[destination];
            return (_visited[dstIndex]);
        }

        /// <summary>
        /// Returns the distance between the source vertex and the specified vertex.
        /// </summary>
        public long DistanceTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");

            int dstIndex = _nodesToIndices[destination];
            return (_distances[dstIndex]);
        }

        /// <summary>
        /// Returns an enumerable collection of nodes that specify the shortest path from the source vertex to the destination vertex.
        /// </summary>
        public IEnumerable<T> ShortestPathTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
                throw new Exception("Graph doesn't have the specified vertex.");
            if (!HasPathTo(destination))
                return null;

            int dstIndex = _nodesToIndices[destination];
            var stack = new DataStructures.Lists.Stack<T>();

            int index;
            for (index = dstIndex; _distances[index] != 0; index = _predecessors[index])
                stack.Push(_indicesToNodes[index]);

            // Push the source vertex
            stack.Push(_indicesToNodes[index]);

            return stack;
        }

    }

}

