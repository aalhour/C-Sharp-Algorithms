/***
 * The Directed Sparse Graph Data Structure.
 * 
 * Definition:
 * A sparse graph is a graph G = (V, E) in which |E| = O(|V|).
 * A directed graph is a graph where each edge follow one direction only between any two vertices.
 * 
 * An adjacency-list digraph (directed-graph) representation. 
 * Implements the IGraph<T> interface.
 */

using System;
using System.Collections.Generic;

using DataStructures.Lists;

namespace DataStructures.Graphs
{
    public class DirectedSparseGraph<T> : IGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        protected virtual int _edgesCount { get; set; }
        protected virtual T _firstInsertedNode { get; set; }
        protected virtual Dictionary<T, DLinkedList<T>> _adjacencyList { get; set; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public DirectedSparseGraph() : this(10) { }

        public DirectedSparseGraph(uint initialCapacity)
        {
            _edgesCount = 0;
            _adjacencyList = new Dictionary<T, DLinkedList<T>>((int)initialCapacity);
        }


        /// <summary>
        /// Helper function. Checks if edge exist in graph.
        /// </summary>
        protected virtual bool _doesEdgeExist(T vertex1, T vertex2)
        {
            return (_adjacencyList[vertex1].Contains(vertex2));
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
            get { return _adjacencyList.Count; }
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
                foreach (var vertex in _adjacencyList)
                    yield return vertex.Key;
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
                foreach (var vertex in _adjacencyList)
                    foreach (var adjacent in vertex.Value)
                        yield return (new UnweightedEdge<T>(
                            vertex.Key,   // from
                            adjacent      // to
                        ));
            }
        }

        /// <summary>
        /// Get all incoming directed unweighted edges to a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> IncomingEdges(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");
            
            foreach(var adjacent in _adjacencyList.Keys)
            {
                if (_adjacencyList[adjacent].Contains(vertex))
                    yield return (new UnweightedEdge<T>(
                        adjacent,   // from
                        vertex      // to
                    ));
            }//end-foreach
        }

        /// <summary>
        /// Get all outgoing directed unweighted edges from a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> OutgoingEdges(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");

            foreach(var adjacent in _adjacencyList[vertex])
                yield return (new UnweightedEdge<T>(
                    vertex,     // from
                    adjacent    // to
                ));
        }


        /// <summary>
        /// Connects two vertices together in the direction: first->second.
        /// </summary>
        public virtual bool AddEdge(T source, T destination)
        {
            // Check existence of nodes and non-existence of edge
            if (!HasVertex(source) || !HasVertex(destination))
                return false;
            if (_doesEdgeExist(source, destination))
                return false;

            // Add edge from source to destination
            _adjacencyList[source].Append(destination);

            // Increment edges count
            ++_edgesCount;

            return true;
        }

        /// <summary>
        /// Removes edge, if exists, from source to destination.
        /// </summary>
        public virtual bool RemoveEdge(T source, T destination)
        {
            // Check existence of nodes and non-existence of edge
            if (!HasVertex(source) || !HasVertex(destination))
                return false;
            if (!_doesEdgeExist(source, destination))
                return false;

            // Remove edge from source to destination
            _adjacencyList[source].Remove(destination);

            // Decrement the edges count
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
            if (HasVertex(vertex))
                return false;

            if (_adjacencyList.Count == 0)
                _firstInsertedNode = vertex;

            _adjacencyList.Add(vertex, new DLinkedList<T>());

            return true;
        }

        /// <summary>
        /// Removes the specified vertex from graph.
        /// </summary>
        public virtual bool RemoveVertex(T vertex)
        {
            // Check existence of vertex
            if (!HasVertex(vertex))
                return false;

            // Subtract the number of edges for this vertex from the total edges count
            _edgesCount = _edgesCount - _adjacencyList[vertex].Count;

            // Remove vertex from graph
            _adjacencyList.Remove(vertex);

            // Remove destination edges to this vertex
            foreach (var adjacent in _adjacencyList)
            {
                if (adjacent.Value.Contains(vertex))
                {
                    adjacent.Value.Remove(vertex);

                    // Decrement the edges count.
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
            return (_adjacencyList.ContainsKey(source) && _adjacencyList.ContainsKey(destination) && _doesEdgeExist(source, destination));
        }

        /// <summary>
        /// Checks whether a vertex exists in the graph
        /// </summary>
        public virtual bool HasVertex(T vertex)
        {
            return _adjacencyList.ContainsKey(vertex);
        }

        /// <summary>
        /// Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        public virtual DLinkedList<T> Neighbours(T vertex)
        {
            if (!HasVertex(vertex))
                return null;

            return _adjacencyList[vertex];
        }

        /// <summary>
        /// Returns the degree of the specified vertex.
        /// </summary>
        public virtual int Degree(T vertex)
        {
            if (!HasVertex(vertex))
                throw new KeyNotFoundException();

            return _adjacencyList[vertex].Count;
        }

        /// <summary>
        /// Returns a human-readable string of the graph.
        /// </summary>
        public virtual string ToReadable()
        {
            string output = string.Empty;

            foreach (var node in _adjacencyList)
            {
                var adjacents = string.Empty;

                output = String.Format("{0}\r\n{1}: [", output, node.Key);

                foreach (var adjacentNode in node.Value)
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
            // Check for existence of source
            if (VerticesCount == 0)
                return new ArrayList<T>(0);
            if (!HasVertex(source))
                throw new KeyNotFoundException("The source vertex doesn't exist.");

            var visited = new HashSet<T>();
            var stack = new DataStructures.Lists.Stack<T>();
            var listOfNodes = new ArrayList<T>(VerticesCount);

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
            // Check for existence of source
            if (VerticesCount == 0)
                return new ArrayList<T>(0);
            if (!HasVertex(source))
                throw new KeyNotFoundException("The source vertex doesn't exist.");

            var visited = new HashSet<T>();
            var queue = new DataStructures.Lists.Queue<T>();
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
            _adjacencyList.Clear();
        }

    }

}
