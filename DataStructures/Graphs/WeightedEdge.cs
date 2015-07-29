using System;

using DataStructures.Common;

namespace DataStructures.Graphs
{
    /// <summary>
    /// The weighted edge class.
    /// </summary>
    public class WeightedEdge<T> : IComparable<WeightedEdge<T>> where T : IComparable<T>
    {
        public T Source { get; set; }
        public T Destination { get; set; }
        public Int32 Weight { get; set; }

        public WeightedEdge(T src, T dst, Int32 weight)
        {
            Source = src;
            Destination = dst;
            Weight = weight;
        }

        public int CompareTo(WeightedEdge<T> other)
        {
            bool areNodesEqual = Source.IsEqualTo<T>(other.Source) && Destination.IsEqualTo<T>(other.Destination);

            if (!areNodesEqual)
                return -1;
            else
                return Weight.CompareTo(other.Weight);
        }
    }
}
