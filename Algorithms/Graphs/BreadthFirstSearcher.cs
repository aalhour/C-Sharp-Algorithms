/***
 * Implements the the Breadth-First Search algorithm. 
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
    public static class BreadthFirstSearcher
    {
        /// <summary>
        /// Iterative BFS implementation.
        /// Traverses nodes in graph starting from a specific node, printing them as they get visited.
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
            var queue = new Queue<T>(Graph.VerticesCount);

            queue.Enqueue (StartVertex);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                Console.Write(String.Format("({0}) ", current));

                foreach (var adjacent in Graph.Neighbours(current))
                {
                    if (!visited.Contains(adjacent))
                    {
                        visited.Add(adjacent);
                        queue.Enqueue(adjacent);
                    }
                }
            }
        }

        /// <summary>
        /// Iterative BFS implementation.
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

            int level = 0;													// keeps track of level
            var frontiers = new List<T>();									// keeps track of previous levels, i - 1
            var levels = new Dictionary<T, int>(Graph.VerticesCount);		// keeps track of visited nodes and their distances
            var parents = new Dictionary<T, object>(Graph.VerticesCount);	// keeps track of tree-nodes

            frontiers.Add(StartVertex);
            levels.Add(StartVertex, 0);
            parents.Add(StartVertex, null);

            // BFS VISIT CURRENT NODE
            Action(StartVertex);

            // TRAVERSE GRAPH
            while (frontiers.Count > 0)
            {
                var next = new List<T>();									// keeps track of the current level, i

                foreach (var node in frontiers)
                {
                    foreach (var adjacent in Graph.Neighbours(node))
                    {
                        if (!levels.ContainsKey(adjacent)) 				// not visited yet
                        {
                            // BFS VISIT NODE STEP
                            Action(adjacent);

                            levels.Add(adjacent, level);					// level[node] + 1
                            parents.Add(adjacent, node);
                            next.Add(adjacent);
                        }
                    }
                }

                frontiers = next;
                level = level + 1;
            }
        }

        /// <summary>
        /// Iterative BFS Implementation.
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

            int level = 0;													// keeps track of levels
            var frontiers = new List<T>();									// keeps track of previous levels, i - 1
            var levels = new Dictionary<T, int>(Graph.VerticesCount);		// keeps track of visited nodes and their distances
            var parents = new Dictionary<T, object>(Graph.VerticesCount);	// keeps track of tree-nodes

            frontiers.Add(StartVertex);
            levels.Add(StartVertex, 0);
            parents.Add(StartVertex, null);

            // BFS VISIT CURRENT NODE
            if (Match(StartVertex))
                return StartVertex;

            // TRAVERSE GRAPH
            while (frontiers.Count > 0)
            {
                var next = new List<T>();									// keeps track of the current level, i

                foreach (var node in frontiers)
                {
                    foreach (var adjacent in Graph.Neighbours(node))
                    {
                        if (!levels.ContainsKey(adjacent)) 				// not visited yet
                        {
                            // BFS VISIT NODE STEP
                            if (Match(adjacent))
                                return adjacent;

                            levels.Add(adjacent, level);					// level[node] + 1
                            parents.Add(adjacent, node);
                            next.Add(adjacent);
                        }
                    }
                }

                frontiers = next;
                level = level + 1;
            }

            throw new Exception("Item was not found!");
        }

    }

}

