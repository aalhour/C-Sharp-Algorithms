using System;

namespace DataStructures.Graphs
{
    /// <summary>
    /// This interface should be implemented alongside the IGraph interface.
    /// </summary>
    public interface IWeightedGraph<T, W> where T : IComparable<T> where W : IComparable<W>
    {
        /// <summary>
        /// Connects two vertices together with a weight, in the direction: first->second.
        /// </summary>
        bool AddEdge(T source, T destination, W weight);

        /// <summary>
        /// Updates the edge weight from source to destination.
        /// </summary>
        bool UpdateEdgeWeight(T source, T destination, W weight);

        /// <summary>
        /// Get edge object from source to destination.
        /// </summary>
        WeightedEdge<T, W> GetEdge(T source, T destination);

        /// <summary>
        /// Returns the edge weight from source to destination.
        /// </summary>
        W GetEdgeWeight(T source, T destination);

        /// <summary>
        /// Returns the neighbours of a vertex as a dictionary of nodes-to-weights.
        /// </summary>
        System.Collections.Generic.Dictionary<T, W> NeighboursMap(T vertex);
    }

    public interface IWeightedGraph<T> : IWeightedGraph<T, Int64> where T : IComparable<T>
    {
        /// <summary>
        /// Get edge object from source to destination.
        /// </summary>
        new WeightedEdge<T> GetEdge(T source, T destination);
    }
}

