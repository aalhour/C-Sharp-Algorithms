/***
 * Implements the Depth-First Search algorithm in two ways: Iterative and Recursive. 
 * 
 * Provides multiple functions for traversing graphs: 
 *  1. PrintAll(), 
 *  2. VisitAll(Action<T> forEachFunc), 
 *  3. FindFirstMatch(Predicate<T> match). 
 * 
 * The VisitAll() applies a function to every graph node. The FindFirstMatch() function searches the graph for a predicate match.
 */

using System;
using System.Collections.Generic;

using DataStructures.Graphs;

namespace Algorithms.Graphs
{
    public static class DepthFirstSearcher
    {
        /// <summary>
        /// DFS Recursive Helper function. 
        /// Visits the neighbors of a given vertex recusively, and applies the given Action<T> to each one of them.
        /// </summary>
        private static void _visitNeighbors<T>(T Vertex, ref IGraph<T> Graph, ref Dictionary<T, object> Parents, Action<T> Action) where T : IComparable<T>
        {
            foreach (var adjacent in Graph.Neighbours(Vertex))
            {
                if (!Parents.ContainsKey(adjacent))
                {
                    // DFS VISIT NODE
                    Action(adjacent);

                    // Save adjacents parent into dictionary
                    Parents.Add(adjacent, Vertex);

                    // Recusively visit adjacent nodes
                    _visitNeighbors(adjacent, ref Graph, ref Parents, Action);
                }
            }
        }

        /// <summary>
        /// Recursive DFS Implementation with helper.
        /// Traverses all the nodes in a graph starting from a specific node, applying the passed action to every node.
        /// </summary>
        public static void VisitAll<T>(ref IGraph<T> Graph, T StartVertex, Action<T> Action) where T : IComparable<T>
        {
            // Check if graph is empty
            if (Graph.VerticesCount == 0)
                throw new Exception("Graph is empty!");

            // Check if graph has the starting vertex
            if (!Graph.HasVertex(StartVertex))
                throw new Exception("Starting vertex doesn't belong to graph.");

            var parents = new Dictionary<T, object>(Graph.VerticesCount);	// keeps track of visited nodes and tree-edges

            foreach (var vertex in Graph.Neighbours(StartVertex))
            {
                if (!parents.ContainsKey(vertex))
                {
                    // DFS VISIT NODE
                    Action(vertex);

                    // Add to parents dictionary
                    parents.Add(vertex, null);

                    // Visit neighbors using recusrive helper
                    _visitNeighbors(vertex, ref Graph, ref parents, Action);
                }
            }
        }

        /// <summary>
        /// Iterative DFS Implementation.
        /// Given a starting node, dfs the graph and print the nodes as they get visited.
        /// </summary>
        public static void PrintAll<T>(IGraph<T> Graph, T StartVertex) where T : IComparable<T>
        {
            // Check if graph is empty
            if (Graph.VerticesCount == 0)
                throw new Exception("Graph is empty!");

            // Check if graph has the starting vertex
            if (!Graph.HasVertex(StartVertex))
                throw new Exception("Starting vertex doesn't belong to graph.");

            var visited = new HashSet<T>();
            var stack = new Stack<T>(Graph.VerticesCount);

            stack.Push(StartVertex);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (!visited.Contains(current))
                {
                    // DFS VISIT NODE STEP
                    Console.Write(String.Format("({0}) ", current));
                    visited.Add(current);

                    // Get the adjacent nodes of current
                    foreach (var adjacent in Graph.Neighbours(current))
                        if (!visited.Contains(adjacent))
                            stack.Push(adjacent);
                }
            }

        }

        /// <summary>
        /// Iterative DFS Implementation.
        /// Given a predicate function and a starting node, this function searches the nodes of the graph for a first match.
        /// </summary>
        public static T FindFirstMatch<T>(IGraph<T> Graph, T StartVertex, Predicate<T> Match) where T : IComparable<T>
        {
            // Check if graph is empty
            if (Graph.VerticesCount == 0)
                throw new Exception("Graph is empty!");

            // Check if graph has the starting vertex
            if (!Graph.HasVertex(StartVertex))
                throw new Exception("Starting vertex doesn't belong to graph.");

            var stack = new Stack<T>();
            var parents = new Dictionary<T, object>(Graph.VerticesCount);	// keeps track of visited nodes and tree-edges

            object currentParent = null;
            stack.Push(StartVertex);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                // Skip loop if node was already visited
                if (!parents.ContainsKey(current))
                {
                    // Save its parent into the dictionary
                    // Mark it as visited
                    parents.Add(current, currentParent);

                    // DFS VISIT NODE STEP
                    if (Match(current))
                        return current;

                    // Get currents adjacent nodes (might add already visited nodes).
                    foreach (var adjacent in Graph.Neighbours(current))
                        if (!parents.ContainsKey(adjacent))
                            stack.Push(adjacent);

                    // Mark current as the father of its adjacents. This helps keep track of tree-nodes.
                    currentParent = current;
                }
            }//end-while

            throw new Exception("Item was not found!");
        }

    }

}

