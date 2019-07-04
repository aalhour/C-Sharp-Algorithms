using System;

using DataStructures.Common;

namespace DataStructures.Graphs
{
    /// <summary>
    /// The graph edge class.
    /// </summary>
    public class UnweightedEdge<TVertex> : IEdge<TVertex> where TVertex : IComparable<TVertex>
    {
        private const int _edgeWeight = 0;

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
        /// [PRIVATE MEMBER] Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public Int64 Weight
        {
            get { throw new NotImplementedException("Unweighted edges don't have weights."); }
            set { throw new NotImplementedException("Unweighted edges can't have weights."); }
        }

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
            if (other == null)
                return -1;

            bool areNodesEqual = Source.IsEqualTo<TVertex>(other.Source) && Destination.IsEqualTo<TVertex>(other.Destination);

            if (!areNodesEqual)
                return -1;
            return 0;
        }
        #endregion
    }
}

