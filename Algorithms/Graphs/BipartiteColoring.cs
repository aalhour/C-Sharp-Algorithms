/***
 * Bipartite Graph.
 * 
 * Checks if a graph is a bipartite or note. Colors the graph is bipartite.
 * 
 * Provides:
 *  * ColorOf: If graph is bipartite return the color of a given vertex in constant time; otherwise, throws an exception.
 *  * IsBipartite: Returns true if graph is bipartite; otherwise, false.
 * 
 * TODO:
 *  * Implement the OddCycle method, which will return an enumerator over the odd cycle, if exists.
 */

using System;
using System.Collections.Generic;

using DataStructures.Graphs;

namespace Algorithms.Graphs
{
    /// <summary>
    /// The Bipartite Colors type.
    /// </summary>
    public enum BipartiteColor
    {
        Red = 0, 
        Blue = 1
    };


    /// <summary>
    /// Bipartite Graph Coloring/Labeling.
    /// </summary>
    public class BipartiteColoring<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private bool _isBipartite { get; set; }
        private int _edgesCount { get; set; }
        private int _verticesCount { get; set; }
        private bool[] _visited { get; set; }
        private BipartiteColor[] _nodesColors { get; set; }
        private Stack<TVertex> _cycle { get; set; }

        // A dictionary that maps node-values to integer indeces
        private Dictionary<TVertex, int> _nodesToIndices { get; set; }

        // A dictionary that maps integer index to node-value
        private Dictionary<int, TVertex> _indicesToNodes { get; set; }


        /// <summary>
        /// CONTRUSTOR
        /// </summary>
        public BipartiteColoring(IGraph<TVertex> Graph)
        {
            // Validate Graph parameter
            if (Graph == null)
                throw new ArgumentNullException();
            else if (Graph.VerticesCount < 2)
                throw new InvalidOperationException("Graph contains less elements than required.");

            // Init data members
            _initializeDataMembers(Graph);

            // Set bipartite flag to true
            _isBipartite = true;

            // Compute bipartiteness
            foreach (var vertex in Graph.Vertices)
            {
                var vertexIndex = _nodesToIndices[vertex];

                // Check the bipartite from this vertex, if it was not visited
                if (!_visited[vertexIndex])
                {
                    _isBipartite = _isBipartiteHelper(Graph, vertex);

                    // Stop discovery of graph when bipartiteness doesn't hold 
                    if (!_isBipartite)
                        throw new InvalidOperationException("Graph contains an odd cycle.");
                }
            }
        }


        /// <summary>
        /// Constructors helper function. Initializes some of the data memebers.
        /// </summary>
        private void _initializeDataMembers(IGraph<TVertex> Graph)
        {
            _isBipartite = false;
            _cycle = null;

            _edgesCount = Graph.EdgesCount;
            _verticesCount = Graph.VerticesCount;

            _visited = new bool[_verticesCount];
            _nodesColors = new BipartiteColor[_verticesCount];

            _nodesToIndices = new Dictionary<TVertex, int>();
            _indicesToNodes = new Dictionary<int, TVertex>();

            // Reset the visited, distances and predeccessors arrays
            int i = 0;
            foreach (var node in Graph.Vertices)
            {
                if (i >= _verticesCount)
                    break;

                _visited[i] = false;
                _nodesColors[i] = BipartiteColor.Red;

                _nodesToIndices.Add(node, i);
                _indicesToNodes.Add(i, node);

                ++i;
            }

        }

        /// <summary>
        /// Constructors helper function. Computes the bipartite of graph from a source vertex.
        /// </summary>
        private bool _isBipartiteHelper(IGraph<TVertex> Graph, TVertex Source)
        {
            var queue = new Queue<TVertex>();
            queue.Enqueue(Source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currIndex = _nodesToIndices[current];

                // Visit node
                if (!_visited[currIndex])
                {
                    _visited[currIndex] = true;
                    _nodesColors[currIndex] = BipartiteColor.Red;
                }

                // Discover bfs-level neighbors
                foreach(var adjacent in Graph.Neighbours(current))
                {
                    var adjIndex = _nodesToIndices[adjacent];

                    if (!_visited[adjIndex])
                    {
                        _visited[adjIndex] = true;
                        _nodesColors[adjIndex] = (_nodesColors[currIndex] == BipartiteColor.Red) ? BipartiteColor.Blue : BipartiteColor.Red;

                        queue.Enqueue(adjacent);
                    }
                    else if (_nodesColors[currIndex] == _nodesColors[adjIndex])
                    {
                        return false;
                    }
                }//end-foreach
            }//end-while

            return true;
        }


        /// <summary>
        /// Determines the graph is bipartite.
        /// </summary>
        public bool IsBipartite()
        {
            return _isBipartite;
        }

        /// <summary>
        /// Returns the color of a vertex.
        /// </summary>
        public BipartiteColor ColorOf(TVertex vertex)
        {
            if (!_isBipartite)
                throw new InvalidOperationException("Graph is not bipartite.");
            else if (!_nodesToIndices.ContainsKey(vertex))
                throw new InvalidOperationException("Vertex doesn't belong to graph.");

            return _nodesColors[_nodesToIndices[vertex]];
        }

        /// <summary>
        /// Returns the odd-cycle in graoh, if any.
        /// </summary>
        /// <returns>The cycle.</returns>
        public IEnumerable<TVertex> OddCycle()
        {
            throw new NotImplementedException();
        }

    }

}

