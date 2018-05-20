/***
 * The Directed Dense Graph Data Structure.
 * 
 * Definition:
 * A dense graph is a graph G = (V, E) in which |E| = O(|V|^2).
 * A directed graph is a graph where each edge follow one direction only between any two vertices.
 * 
 * An incidence-matrix digraph representation. Follows almost the same implementation 
 * details of the Undirected version, except for managing the directed edges.
 * Implements the IGraph<T> interface.
 */

using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Graphs
{
    public class DirectedDenseGraph<T> : IGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private const object EMPTY_VERTEX_SLOT = (object)null;

        protected virtual int _edgesCount { get; set; }
        protected virtual int _verticesCount { get; set; }
        protected virtual int _verticesCapacity { get; set; }
        protected virtual ArrayList<object> _vertices { get; set; }
        protected virtual T _firstInsertedNode { get; set; }
        protected virtual bool[,] _adjacencyMatrix { get; set; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public DirectedDenseGraph(uint capacity = 10)
        {
            _edgesCount = 0;
            _verticesCount = 0;
            _verticesCapacity = (int)capacity;

            _vertices = new ArrayList<object>(_verticesCapacity);
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
        }


        /// <summary>
        /// Helper function. Checks if edge exist in graph.
        /// </summary>
        protected virtual bool _doesEdgeExist(int source, int destination)
        {
            return (_adjacencyMatrix[source, destination] == true);
        }

        /// <summary>
        /// Returns true, if graph is directed; false otherwise.
        /// </summary>
        public virtual bool IsDirected
        {
            get { return true; }
        }

        /// <summary>
        /// Returns true, if graph is weighted; false otherwise.
        /// </summary>
        public virtual bool IsWeighted
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the count of vetices.
        /// </summary>
        public virtual int VerticesCount
        {
            get { return _verticesCount; }
        }

        /// <summary>
        /// Gets the count of edges.
        /// </summary>
        public virtual int EdgesCount
        {
            get { return _edgesCount; }
        }

        /// <summary>
        /// Returns the list of Vertices.
        /// </summary>
        public virtual IEnumerable<T> Vertices
        {
            get
            {
                foreach (var vertex in _vertices)
                    if (vertex != null)
                        yield return (T)vertex;
            }
        }


        IEnumerable<IEdge<T>> IGraph<T>.Edges
        {
            get { return this.Edges; }
        }

        IEnumerable<IEdge<T>> IGraph<T>.IncomingEdges(T vertex)
        {
            return this.IncomingEdges(vertex);
        }

        IEnumerable<IEdge<T>> IGraph<T>.OutgoingEdges(T vertex)
        {
            return this.OutgoingEdges(vertex);
        }


        /// <summary>
        /// An enumerable collection of all directed unweighted edges in graph.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> Edges
        {
            get
            {
                foreach (var vertex in _vertices)
                    foreach (var outgoingEdge in OutgoingEdges((T)vertex))
                        yield return outgoingEdge;
            }
        }

        /// <summary>
        /// Get all incoming directed unweighted edges to a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> IncomingEdges(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");

            int source = _vertices.IndexOf(vertex);

            for (int adjacent = 0; adjacent < _vertices.Count; ++adjacent)
            {
                if (_vertices[adjacent] != null && _doesEdgeExist(adjacent, source))
                {
                    yield return (new UnweightedEdge<T>(
                        (T)_vertices[adjacent], // from
                        vertex                  // to
                    ));
                }
            }//end-for
        }

        /// <summary>
        /// Get all outgoing directed unweighted edges from a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> OutgoingEdges(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");

            int source = _vertices.IndexOf(vertex);

            for (int adjacent = 0; adjacent < _vertices.Count; ++adjacent)
            {
                if (_vertices[adjacent] != null && _doesEdgeExist(source, adjacent))
                {
                    yield return (new UnweightedEdge<T>(
                        vertex,                 // from
                        (T)_vertices[adjacent]  // to
                    ));
                }
            }//end-for
        }


        /// <summary>
        /// Connects two vertices together in the direction: first->second.
        /// </summary>
        public virtual bool AddEdge(T source, T destination)
        {
            // Get indices of vertices
            int srcIndex = _vertices.IndexOf(source);
            int dstIndex = _vertices.IndexOf(destination);

            // Check existence of vertices and non-existence of edge
            if (srcIndex == -1 || dstIndex == -1)
                return false;
            if (_doesEdgeExist(srcIndex, dstIndex))
                return false;

            _adjacencyMatrix[srcIndex, dstIndex] = true;

            // Increment edges count
            ++_edgesCount;

            return true;
        }

        /// <summary>
        /// Removes edge, if exists, from source to destination.
        /// </summary>
        public virtual bool RemoveEdge(T source, T destination)
        {
            // Get indices of vertices
            int srcIndex = _vertices.IndexOf(source);
            int dstIndex = _vertices.IndexOf(destination);

            // Check existence of vertices and non-existence of edge
            if (srcIndex == -1 || dstIndex == -1)
                return false;
            if (!_doesEdgeExist(srcIndex, dstIndex))
                return false;

            _adjacencyMatrix[srcIndex, dstIndex] = false;

            // Increment edges count
            --_edgesCount;

            return true;
        }

        /// <summary>
        /// Add a collection of vertices to the graph.
        /// </summary>
        public virtual void AddVertices(IList<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            foreach (var vertex in collection)
                AddVertex(vertex);
        }

        /// <summary>
        /// Add vertex to the graph
        /// </summary>
        public virtual bool AddVertex(T vertex)
        {
            // Return of the capacity is reached
            if (_verticesCount >= _verticesCapacity)
                return false;

            // Return if vertex already exists
            if (HasVertex(vertex))
                return false;

            // Initialize first inserted node
            if (_verticesCount == 0)
                _firstInsertedNode = vertex;

            // Try inserting vertex at previously lazy-deleted slot
            int indexOfNull = _vertices.IndexOf(EMPTY_VERTEX_SLOT);

            if (indexOfNull != -1)
                _vertices[indexOfNull] = vertex;
            else
                _vertices.Add(vertex);

            // Increment vertices count
            ++_verticesCount;

            return true;
        }

        /// <summary>
        /// Removes the specified vertex from graph.
        /// </summary>
        public virtual bool RemoveVertex(T vertex)
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
                    _adjacencyMatrix[index, i] = false;

                    // Decrement the edges count
                    --_edgesCount;
                }

                // Destination edge
                if (_doesEdgeExist(i, index))
                {
                    _adjacencyMatrix[i, index] = false;

                    // Decrement the edges count
                    --_edgesCount;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether there is an edge from source to destination.
        /// </summary>
        public virtual bool HasEdge(T source, T destination)
        {
            // Get indices of vertices
            int srcIndex = _vertices.IndexOf(source);
            int dstIndex = _vertices.IndexOf(destination);

            // Check the existence of vertices and the directed edge
            return (srcIndex != -1 && dstIndex != -1 && _doesEdgeExist(srcIndex, dstIndex));
        }

        /// <summary>
        /// Determines whether this graph has the specified vertex.
        /// </summary>
        public virtual bool HasVertex(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        /// Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        public virtual DataStructures.Lists.DLinkedList<T> Neighbours(T vertex)
        {
            var neighbors = new DLinkedList<T>();
            int source = _vertices.IndexOf(vertex);

            // Check existence of vertex
            if (source != -1)
                for (int adjacent = 0; adjacent < _vertices.Count; ++adjacent)
                    if (_vertices[adjacent] != null && _doesEdgeExist(source, adjacent))
                        neighbors.Append((T)_vertices[adjacent]);

            return neighbors;
        }

        /// <summary>
        /// Returns the degree of the specified vertex.
        /// </summary>
        public virtual int Degree(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException();

            return Neighbours(vertex).Count;
        }

        /// <summary>
        /// Returns a human-readable string of the graph.
        /// </summary>
        public virtual string ToReadable()
        {
            string output = string.Empty;

            for (int i = 0; i < _vertices.Count; ++i)
            {
                if (_vertices[i] == null)
                    continue;

                var node = (T)_vertices[i];
                var adjacents = string.Empty;

                output = String.Format("{0}\r\n{1}: [", output, node);

                foreach (var adjacentNode in Neighbours(node))
                    adjacents = String.Format("{0}{1},", adjacents, adjacentNode);

                if (adjacents.Length > 0)
                    adjacents = adjacents.TrimEnd(new char[] { ',', ' ' });

                output = String.Format("{0}{1}]", output, adjacents);
            }

            return output;
        }

        /// <summary>
        /// A depth first search traversal of the graph starting from the first inserted node.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk()
        {
            return DepthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        /// A depth first search traversal of the graph, starting from a specified vertex.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk(T source)
        {
            if (_verticesCount == 0)
                return new ArrayList<T>();
            if (!HasVertex(source))
                throw new Exception("The specified starting vertex doesn't exist.");

            var stack = new Lists.Stack<T>(_verticesCount);
            var visited = new HashSet<T>();
            var listOfNodes = new ArrayList<T>(_verticesCount);

            stack.Push(source);

            while (!stack.IsEmpty)
            {
                var current = stack.Pop();

                if (!visited.Contains(current))
                {
                    listOfNodes.Add(current);
                    visited.Add(current);

                    foreach (var adjacent in Neighbours(current))
                        if (!visited.Contains(adjacent))
                            stack.Push(adjacent);
                }
            }

            return listOfNodes;
        }

        /// <summary>
        /// A breadth first search traversal of the graphstarting from the first inserted node.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk()
        {
            return BreadthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        /// A breadth first search traversal of the graph, starting from a specified vertex.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk(T source)
        {
            if (_verticesCount == 0)
                return new ArrayList<T>();
            if (!HasVertex(source))
                throw new Exception("The specified starting vertex doesn't exist.");

            var visited = new HashSet<T>();
            var queue = new Lists.Queue<T>(VerticesCount);
            var listOfNodes = new ArrayList<T>(VerticesCount);

            listOfNodes.Add(source);
            visited.Add(source);

            queue.Enqueue(source);

            while (!queue.IsEmpty)
            {
                var current = queue.Dequeue();
                var neighbors = Neighbours(current);

                foreach (var adjacent in neighbors)
                {
                    if (!visited.Contains(adjacent))
                    {
                        listOfNodes.Add(adjacent);
                        visited.Add(adjacent);
                        queue.Enqueue(adjacent);
                    }
                }
            }

            return listOfNodes;
        }

        /// <summary>
        /// Clear this graph.
        /// </summary>
        public virtual void Clear()
        {
            _edgesCount = 0;
            _verticesCount = 0;
            _vertices = new ArrayList<object>(_verticesCapacity);
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
        }

    }

}

