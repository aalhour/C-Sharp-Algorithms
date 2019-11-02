using System;

using DataStructures.Common;

namespace DataStructures.Graphs
{
    /// <summary>
    /// The graph weighted edge class.
    /// </summary>
    public class WeightedEdge<TVertex> : IEdge<TVertex> where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public TVertex Source { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public TVertex Destination { get; set; }

        /// <summary>
        /// Gets or sets the weight of edge.
        /// </summary>
        /// <value>The weight.</value>
        public Int64 Weight { get; set; }

        /// <summary>
        /// Gets a value indicating whether this edge is weighted.
        /// </summary>
        public bool IsWeighted
        {
            get { return true; }
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public WeightedEdge(TVertex src, TVertex dst, Int64 weight)
        {
            Source = src;
            Destination = dst;
            Weight = weight;
        }


        #region IComparable implementation
        public int CompareTo(IEdge<TVertex> other)
        {
            if (other == null)
                return -1;
            
            bool areNodesEqual = Source.IsEqualTo<TVertex>(other.Source) && Destination.IsEqualTo<TVertex>(other.Destination);

            if (!areNodesEqual)
                return -1;
            return Weight.CompareTo(other.Weight);
        }
        #endregion
    }
}
