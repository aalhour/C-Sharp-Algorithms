
namespace DataStructures.Heaps
{
    public interface IMaxHeap<T> where T : System.IComparable<T>
    {
        /// <summary>
        /// Returns the number of elements in heap
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Checks whether this heap is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Heapifies the specified newCollection. Overrides the current heap.
        /// </summary>
        /// <param name="newCollection">New collection.</param>
        void Initialize(System.Collections.Generic.IList<T> newCollection);

        /// <summary>
        /// Adding a new key to the heap.
        /// </summary>
        /// <param name="heapKey">Heap key.</param>
        void Add(T heapKey);

        /// <summary>
        /// Find the maximum node of a max heap.
        /// </summary>
        /// <returns>The maximum.</returns>
        T Peek();

        /// <summary>
        /// Removes the node of maximum value from a max heap.
        /// </summary>
        void RemoveMax();

        /// <summary>
        /// Returns the node of maximum value from a max heap after removing it from the heap.
        /// </summary>
        /// <returns>The max.</returns>
        T ExtractMax();

        /// <summary>
        /// Clear this heap.
        /// </summary>
        void Clear();

        /// <summary>
        /// Rebuilds the heap.
        /// </summary>
        void RebuildHeap();

        /// <summary>
        /// Returns an array version of this heap.
        /// </summary>
        /// <returns>The array.</returns>
        T[] ToArray();

        /// <summary>
        /// Returns a list version of this heap.
        /// </summary>
        /// <returns>The list.</returns>
        System.Collections.Generic.List<T> ToList();

        /// <summary>
        /// Returns a new max heap that contains all elements of this heap.
        /// </summary>
        /// <returns>The max heap.</returns>
        IMinHeap<T> ToMinHeap();
    }
}
