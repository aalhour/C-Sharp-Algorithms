/***
 * Computes one topological sorting of a DAG (Directed Acyclic Graph). 
 * This class depends on the CyclesDetector static class.
 */

using System;
using System.Collections.Generic;
using DataStructures.Graphs;

namespace Algorithms.Graphs
{
    public static class TopologicalSorter
    {
        /// <summary>
        /// Private recursive helper.
        /// </summary>
        private static void _topoSortHelper<T>(IGraph<T> graph, T source, ref DataStructures.Lists.Stack<T> topoSortStack, ref HashSet<T> visited) where T : IComparable<T>
        {
            visited.Add(source);

            foreach (var adjacent in graph.Neighbours(source))
                if (!visited.Contains(adjacent))
                    _topoSortHelper<T>(graph, adjacent, ref topoSortStack, ref visited);

            topoSortStack.Push(source);
        }


        /// <summary>
        /// The Topological Sorting algorithm
        /// </summary>
        public static IEnumerable<T> Sort<T>(IGraph<T> Graph) where T : IComparable<T>
        {
            // If the graph is either null or is not a DAG, throw exception.
            if (Graph == null)
                throw new ArgumentNullException();
            if (!Graph.IsDirected || CyclesDetector.IsCyclic<T>(Graph))
                throw new Exception("The graph is not a DAG.");

            var visited = new HashSet<T>();
            var topoSortStack = new DataStructures.Lists.Stack<T>();

            foreach (var vertex in Graph.Vertices)
                if (!visited.Contains(vertex))
                    _topoSortHelper<T>(Graph, vertex, ref topoSortStack, ref visited);

            return topoSortStack;
        }
    }
}
