using System;

using DataStructures.Common;

namespace DataStructures.Graphs
{
    /// <summary>
    /// The graph edge class.
    /// </summary>
    public class UnweightedEdge<TVertex> : IEdge<TVertex> where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Gets or sets the source vertex.
        /// </summary>
        /// <value>The source.</value>
        public TVertex Source { get; set; }

        /// <summary>
        /// Gets or sets the destination vertex.
        /// </summary>
        /// <value>The destination.</value>
        public TVertex Destination { get; set; }

        /// <summary>
        /// Gets a value indicating whether this edge is weighted.
        /// </summary>
        public bool IsWeighted
        {
            get
            { return false; }
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public UnweightedEdge(TVertex src, TVertex dst)
        {
            Source = src;
            Destination = dst;
        }


        #region IComparable implementation
        public int CompareTo(IEdge<TVertex> other)
        {
            if (other == null || !(other is UnweightedEdge<TVertex>))
                return -1;

            bool areNodesEqual = Source.IsEqualTo<TVertex>(other.Source) && Destination.IsEqualTo<TVertex>(other.Destination);

            if (!areNodesEqual)
                return -1;
            return 0;
        }
        #endregion
    }
}

