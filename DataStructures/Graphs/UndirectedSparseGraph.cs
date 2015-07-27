using System;
using System.Collections.Generic;

using DataStructures.Lists;

namespace DataStructures.Graphs
{
    /// <summary>
    /// The Sparse Graph Data Structure.
    /// 
    /// Definition:
    /// A sparse graph is a graph G = (V, E) in which |E| = O(|V|).
    /// 
    /// This class represents the graph as an adjacency list (dictionary).
    /// </summary>
    public class UndirectedSparseGraph<T> : IGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        protected virtual int _edgesCount { get; set; }
        protected virtual T _firstInsertedNode { get; set; }
        protected virtual Dictionary<T, DLinkedList<T>> _adjacencyList { get; set; }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public UndirectedSparseGraph()
        {
            _edgesCount = 0;
            _adjacencyList = new Dictionary<T, DLinkedList<T>>();
        }

        public UndirectedSparseGraph(int size)
        {
            _edgesCount = 0;
            _adjacencyList = new Dictionary<T, DLinkedList<T>>(size);
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
                var list = new ArrayList<T>();
                foreach (var vertex in _adjacencyList.Keys)
                    list.Add(vertex);

                return list;
            }
        }

        /// <summary>
        /// Connects two vertices together.
        /// </summary>
        public virtual bool AddEdge(T firstVertex, T secondVertex)
        {
            bool status = false;

            // Check existence of vertices
            if (_adjacencyList.ContainsKey(firstVertex) && _adjacencyList.ContainsKey(secondVertex))
            {
                var neighbours = _adjacencyList[firstVertex];

                // Check existence of an edge in one list.
                // If edge doesn't exist, add the connecting edge to both vertices' neighbours lists.
                if (!neighbours.Contains(secondVertex))
                {
                    _adjacencyList[firstVertex].Append(secondVertex);
                    _adjacencyList[secondVertex].Append(firstVertex);

                    // Increment the edges count
                    ++_edgesCount;

                    status = true;
                }
            }

            return status;
        }

        /// <summary>
        /// Deletes an edge, if exists, between two vertices.
        /// </summary>
        public virtual bool RemoveEdge(T firstVertex, T secondVertex)
        {
            bool status = false;

            // Check existence of vertices
            if (_adjacencyList.ContainsKey(firstVertex) && _adjacencyList.ContainsKey(secondVertex))
            {
                var neighbours = _adjacencyList[firstVertex];

                // Check existence of an edge in one list.
                // If edge doesn't exist, add the connecting edge to both vertices' neighbours lists.
                if (neighbours.Contains(secondVertex))
                {
                    _adjacencyList[firstVertex].Remove(secondVertex);
                    _adjacencyList[secondVertex].Remove(firstVertex);

                    // Decrement the edges count
                    --_edgesCount;

                    status = true;
                }
            }

            return status;
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
        public virtual bool AddVertex(T vertex)
        {
            // Check existence of vertex
            if (!_adjacencyList.ContainsKey(vertex))
            {
                if (_adjacencyList.Count == 0)
                    _firstInsertedNode = vertex;

                _adjacencyList.Add(vertex, new DLinkedList<T>());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified vertex from graph.
        /// </summary>
        public virtual bool RemoveVertex(T vertex)
        {
            // Check existence
            if (_adjacencyList.ContainsKey(vertex))
            {
                _adjacencyList.Remove(vertex);

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

            return false;
        }

        /// <summary>
        /// Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
        /// </summary>
        public virtual bool HasEdge(T firstVertex, T secondVertex)
        {
            bool status = false;

            // Check existence of vertices
            if (_adjacencyList.ContainsKey(firstVertex) && _adjacencyList.ContainsKey(secondVertex))
            {
                // Check the neighbours of only one, because the edge is always added to the two vertices.
                // If the edge is there, it returns true, else it returns false.
                status |= _adjacencyList[firstVertex].Contains(secondVertex);	// binary or
            }

            return status;
        }

        /// <summary>
        /// Determines whether this graph has the specified vertex.
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
            if (_adjacencyList.ContainsKey(vertex))
                return _adjacencyList[vertex];

            return null;
        }

        /// <summary>
        /// Returns the degree of the specified vertex.
        /// </summary>
        public virtual int Degree(T vertex)
        {
            if (!_adjacencyList.ContainsKey(vertex))
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

                output = String.Format(
                    "{0}\r\n{1}: ["
                    , output
                    , node.Key
                );

                foreach (var adjacentNode in node.Value)
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
        /// Recursive DFS Helper function. Visits the neighbors of a given vertex.
        /// </summary>
        protected virtual void _DFSVisitNeighbors(T fromVertex, ref Dictionary<T, object> parents)
        {
            foreach (var node in Neighbours(fromVertex))
            {
                if (!parents.ContainsKey(node))
                {
                    _visitNode(node);
                    parents.Add(node, fromVertex);
                    _DFSVisitNeighbors(node, ref parents);
                }
            }
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
            _adjacencyList.Clear();
        }

    }

}

