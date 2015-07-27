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
    public class UndirectedDenseGraph<T> : IGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private const object EMPTY_VERTEX_SLOT = null;

        protected virtual int _edgesCount { get; set; }
        protected virtual int _verticesCount { get; set; }
        protected virtual int _verticesCapacity { get; set; }
        protected virtual ArrayList<object> _vertices { get; set; }
        protected virtual T _firstInsertedNode { get; set; }
        protected virtual bool[,] _adjacencyMatrix { get; set; }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public UndirectedDenseGraph(uint capacity = 10)
        {
            _edgesCount = 0;
            _verticesCount = 0;
            _verticesCapacity = (int)capacity;

            _vertices = new ArrayList<object>(_verticesCapacity);
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
        }


        /// <summary>
        /// Helper function that looks up if an edge exists.
        /// </summary>
        private bool _doesEdgeExist(int index1, int index2)
        {
            return (_adjacencyMatrix[index1, index2] || _adjacencyMatrix[index2, index1]);
        }

        /// <summary>
        /// Helper function that checks whether a vertex exist.
        /// </summary>
        private bool _doesVertexExist(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        /// A helper function used in graph traversal algorithsm. Prints the node.
        /// </summary>
        private void _visitNode(T vertex)
        {
            Console.Write(String.Format("({0}) ", vertex));
        }

        /// <summary>
        /// Gets the count of vetices.
        /// </summary>
        public int VerticesCount
        {
            get { return _verticesCount; }
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
        public IEnumerable<T> Vertices
        {
            get
            {
                foreach (var item in _vertices)
                {
                    if (item != null)
                        yield return (T)item;
                }
            }
        }

        /// <summary>
        /// Connects two vertices together.
        /// </summary>
        public bool AddEdge(T firstVertex, T secondVertex)
        {
            int indexOfFirst = _vertices.IndexOf(firstVertex);
            int indexOfSecond = _vertices.IndexOf(secondVertex);

            if (indexOfFirst == -1 || indexOfSecond == -1)
                return false;
            else if (_doesEdgeExist(indexOfFirst, indexOfSecond))
                return false;

            _adjacencyMatrix[indexOfFirst, indexOfSecond] = true;
            _adjacencyMatrix[indexOfSecond, indexOfFirst] = true;

            // Increment the edges count.
            ++_edgesCount;

            return true;
        }

        /// <summary>
        /// Deletes an edge, if exists, between two vertices.
        /// </summary>
        public bool RemoveEdge(T firstVertex, T secondVertex)
        {
            int indexOfFirst = _vertices.IndexOf(firstVertex);
            int indexOfSecond = _vertices.IndexOf(secondVertex);

            if (indexOfFirst == -1 || indexOfSecond == -1)
                return false;
            else if (!_doesEdgeExist(indexOfFirst, indexOfSecond))
                return false;

            _adjacencyMatrix[indexOfFirst, indexOfSecond] = false;
            _adjacencyMatrix[indexOfSecond, indexOfFirst] = false;

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
                throw new ArgumentNullException();

            foreach (var item in collection)
            {
                this.AddVertex(item);
            }
        }

        /// <summary>
        /// Adds a new vertex to graph.
        /// </summary>
        public bool AddVertex(T vertex)
        {
            // Return if graph reached it's maximum capacity
            if (_verticesCount >= _verticesCapacity)
                return false;

            // Return if vertex exists
            if (_doesVertexExist(vertex))
                return false;

            if (_verticesCount == 0)
                _firstInsertedNode = vertex;

            int indexOfDeleted = _vertices.IndexOf((object)null);

            if (indexOfDeleted != -1)
                _vertices[indexOfDeleted] = vertex;
            else
                _vertices.Add(vertex);

            // Increment the vertices count
            ++_verticesCount;

            return true;
        }

        /// <summary>
        /// Removes the specified vertex from graph.
        /// </summary>
        public bool RemoveVertex(T vertex)
        {
            int index = _vertices.IndexOf(vertex);

            // Return if vertex doesn't exists
            if (index == -1)
                return false;

            //_vertices.Remove (vertex);
            _vertices[index] = null;

            // Decrement the vertices count
            --_verticesCount;

            // Delete the edges
            for (int i = 0; i < _verticesCapacity; ++i)
            {
                if (_doesEdgeExist(index, i))
                {
                    _adjacencyMatrix[index, i] = false;
                    _adjacencyMatrix[i, index] = false;

                    // Decrement the edges count
                    --_edgesCount;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
        /// </summary>
        public bool HasEdge(T firstVertex, T secondVertex)
        {
            int indexOfFirst = _vertices.IndexOf(firstVertex);
            int indexOfSecond = _vertices.IndexOf(secondVertex);

            // Check for existence
            if (indexOfFirst == -1 || indexOfSecond == -1)
                return false;

            return _doesEdgeExist(indexOfFirst, indexOfSecond);
        }

        /// <summary>
        /// Determines whether this graph has the specified vertex.
        /// </summary>
        public bool HasVertex(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        /// Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        public DataStructures.Lists.DLinkedList<T> Neighbours(T vertex)
        {
            var neighbours = new DLinkedList<T>();
            int index = _vertices.IndexOf(vertex);

            if (index == -1)
                return neighbours;

            for (int i = 0; i < _vertices.Count; ++i)
            {
                if (_vertices[i] != null && _doesEdgeExist(index, i))
                    neighbours.Append((T)_vertices[i]);
            }

            return neighbours;
        }

        /// <summary>
        /// Returns the degree of the specified vertex.
        /// </summary>
        public int Degree(T vertex)
        {
            return Neighbours(vertex).Count;
        }

        /// <summary>
        /// Returns a human-readable string of the graph.
        /// </summary>
        public string ToReadable()
        {
            string output = string.Empty;

            for (int i = 0; i < _vertices.Count; ++i)
            {
                if (_vertices[i] == null)
                    continue;

                var node = (T)_vertices[i];
                var adjacents = string.Empty;

                output = String.Format(
                    "{0}\r\n{1}: ["
                    , output
                    , node
                );

                foreach (var adjacentNode in Neighbours(node))
                {
                    adjacents = String.Format("{0}{1},", adjacents, adjacentNode);
                }

                if (adjacents.Length > 0)
                    adjacents.Remove(adjacents.Length - 1);

                output = String.Format("{0}{1}]", output, adjacents);
            }

            return output;
        }

        /// <summary>
        /// A depth first search traversal of the graph starting from the first inserted node.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual ArrayList<T> DepthFirstWalk()
        {
            if (VerticesCount == 0)
                return new ArrayList<T>();

            return DepthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        /// A depth first search traversal of the graph, starting from a specified vertex.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual ArrayList<T> DepthFirstWalk(T startingVertex)
        {
            if (VerticesCount == 0)
                return new ArrayList<T>();
            else if (!HasVertex(startingVertex))
                throw new Exception("The specified starting vertex doesn't exist.");

            var current = startingVertex;
            var stack = new Lists.Stack<T>(VerticesCount);
            var visited = new HashSet<T>();
            var listOfNodes = new ArrayList<T>(VerticesCount);

            stack.Push(current);

            while (!stack.IsEmpty)
            {
                current = stack.Pop();

                if (visited.Contains(current))
                    continue;

                listOfNodes.Add(current);
                visited.Add(current);

                foreach (var adjacent in Neighbours(current))
                    if (!visited.Contains(adjacent))
                        stack.Push(adjacent);
            }

            return listOfNodes;
        }

        /// <summary>
        /// A breadth first search traversal of the graphstarting from the first inserted node.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual ArrayList<T> BreadthFirstWalk()
        {
            if (VerticesCount == 0)
                return new ArrayList<T>();

            return BreadthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        /// A breadth first search traversal of the graph, starting from a specified vertex.
        /// Returns the visited vertices of the graph.
        /// </summary>
        public virtual ArrayList<T> BreadthFirstWalk(T startingVertex)
        {
            if (VerticesCount == 0)
                return new ArrayList<T>();
            else if (!HasVertex(startingVertex))
                throw new Exception("The specified starting vertex doesn't exist.");

            var current = startingVertex;
            var queue = new Lists.Queue<T>(VerticesCount);
            var visited = new HashSet<T>();
            var listOfNodes = new ArrayList<T>(VerticesCount);

            listOfNodes.Add(current);
            visited.Add(current);

            queue.Enqueue(current);

            while (!queue.IsEmpty)
            {
                current = queue.Dequeue();
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
            _vertices.Clear();
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(rows: _verticesCapacity, columns: _verticesCapacity, defaultValue: false);
        }

    }

}

