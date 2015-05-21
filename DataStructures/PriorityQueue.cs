using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// 
    /// WORK IN PROGRESS.
    /// 
    /// Implements the Priority Queue Data Structure.
    /// </summary>
    public class PriorityQueue<K, V, P>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
		private MaxHeap<PriorityQueueNode<K, V, P>> _heap { get; set; }
		private Comparer<PriorityQueueNode<K, V, P>> _priorityComparer { get; set; }


		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		public PriorityQueue() : this(0, null) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="capacity">Capacity of priority queue.</param>
		public PriorityQueue(int capacity) : this(capacity, null) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="capacity">Capacity of priority queue.</param>
		/// <param name="priorityComparer">The node's priority comparer.</param>
		public PriorityQueue(int capacity, Comparer<PriorityQueueNode<K, V, P>> priorityComparer)
		{
			if (capacity >= 0)
			{
				this._priorityComparer = priorityComparer ?? (new PriorityQueueNodeComparer<K, V, P>());

				_heap = new MaxHeap<PriorityQueueNode<K, V, P>> (capacity, this._priorityComparer);
			}
			else
			{
				throw new ArgumentOutOfRangeException ("Please provide a valid capacity.");
			}
		}


        /// <summary>
		/// Inserts a new item with a priority
        /// </summary>
        /// <param name="key">Node key.</param>
        /// <param name="value">Node value.</param>
        /// <param name="priority">Node priority.</param>
        public void InsertWithPriority(K key, V value, P priority)
        {
			var newHeapNode = new PriorityQueueNode<K, V, P> (key, value, priority);
			_heap.Insert (newHeapNode);
        }


        /// <summary>
		/// Remove the highest priority element and return it.
        /// </summary>
        /// <returns>The highest priority.</returns>
        public V PullHighestPriority()
        {
			return _heap.ExtractMax ().Value;
        }


        /// <summary>
		/// Returns the highest priority element.
        /// </summary>
        /// <returns>The at highest priority.</returns>
        public V PeekAtHighestPriority()
        {
			return _heap.Peek ().Value;
        }


        //
        // Removes the first-matched element that has the value = dataItem
        public void Remove(K key, V value)
        {
			var keyComparer = Comparer<K>.Default;
			var valueComparer = Comparer<V>.Default;

			Predicate<PriorityQueueNode<K, V, P>> match = 
				new Predicate<PriorityQueueNode<K, V, P>> (
					item => 
					keyComparer.Compare(item.Key, key) == 0 && 
					valueComparer.Compare(item.Value, value) == 0);

			_heap.RemoveAll (match);
        }
    }


	/// <summary>
	/// The Keyed Heap Node object.
	/// </summary>
	/// <typeparam name="K">Node's Key type</typeparam>
	/// <typeparam name="V">Node's Value type</typeparam>
	/// <typeparam name="P">Node's Priority type</typeparam>
	public class PriorityQueueNode<K, V, P>
	{
		public K Key { get; set; }
		public V Value { get; set; }
		public P Priority { get; set; }

		public PriorityQueueNode() : this(default(K), default(V), default(P)) { }

		public PriorityQueueNode (K key, V value, P priority)
		{
			this.Key = key;
			this.Value = value;
			this.Priority = priority;
		}
	}//end-of-node-class


	public class PriorityQueueNodeComparer<K, V, P> : Comparer<PriorityQueueNode<K, V, P>>
	{
		private readonly Comparer<P> _priorityComparer = Comparer<P>.Default;

		#region implemented abstract members of Comparer
		public override int Compare (PriorityQueueNode<K, V, P> first, PriorityQueueNode<K, V, P> second)
		{
			return _priorityComparer.Compare (first.Priority, second.Priority);
		}
		#endregion
	}//end-of-comparer-class
}
