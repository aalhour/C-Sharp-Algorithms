using System;

namespace DataStructures.Sets
{
    /// <summary>
    /// Disjoint Set (Union-Find) over the integers 0..size-1.
    /// Uses union by rank and path compression, giving near-constant time per operation.
    /// </summary>
    public class DisjointSet
    {
        private readonly int[] _parent;
        private readonly int[] _rank;

        /// <summary>Creates a set where each element starts in its own group.</summary>
        public DisjointSet(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be negative.");

            _parent = new int[size];
            _rank = new int[size];

            for (int i = 0; i < size; i++)
                _parent[i] = i;

            Count = size;
        }

        /// <summary>The number of separate groups right now.</summary>
        public int Count { get; private set; }

        /// <summary>Returns the representative (root) of the group containing x.</summary>
        public int Find(int x)
        {
            ValidateIndex(x);

            // Path compression: point every visited node directly at the root.
            if (_parent[x] != x)
                _parent[x] = Find(_parent[x]);

            return _parent[x];
        }

        /// <summary>
        /// Joins the groups containing a and b.
        /// Returns true if two different groups were merged, false if they were already together.
        /// </summary>
        public bool Union(int a, int b)
        {
            int rootA = Find(a);
            int rootB = Find(b);

            if (rootA == rootB)
                return false;

            // Union by rank: attach the shorter tree under the taller one.
            if (_rank[rootA] < _rank[rootB])
            {
                _parent[rootA] = rootB;
            }
            else if (_rank[rootA] > _rank[rootB])
            {
                _parent[rootB] = rootA;
            }
            else
            {
                _parent[rootB] = rootA;
                _rank[rootA]++;
            }

            Count--;
            return true;
        }

        /// <summary>True if a and b are in the same group.</summary>
        public bool Connected(int a, int b)
        {
            return Find(a) == Find(b);
        }

        private void ValidateIndex(int x)
        {
            if (x < 0 || x >= _parent.Length)
                throw new ArgumentOutOfRangeException(nameof(x), "Element is outside the set's range.");
        }
    }
}