using System;

namespace DataStructures.Graphs
{
    /// <summary>
    /// This interface should be implemented by all edges classes.
    /// </summary>
    public interface IEdge<TVertex> : IComparable<IEdge<TVertex>> where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether this edge is weighted.
        /// </summary>
        /// <value><c>true</c> if this edge is weighted; otherwise, <c>false</c>.</value>
        bool IsWeighted { get; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        TVertex Source { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        TVertex Destination { get; set; }

        /// <summary>
        /// Gets or sets the weight of edge.
        /// Unwighted edges can be thought of as edges of the same weight
        /// </summary>
        /// <value>The weight.</value>
        Int64 Weight { get; set; }
    }
}

