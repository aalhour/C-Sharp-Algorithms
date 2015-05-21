using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Implements the Keyed Priority Queue Data Structure.
	/// All nodes have: a Key, a Value, a Priority
	/// <typeparam name="K">Node's Key type</typeparam>
	/// <typeparam name="V">Node's Value type</typeparam>
	/// <typeparam name="P">Node's Priority type</typeparam>
    /// </summary>
    public class PriorityQueue<K, V, P>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
		private MaxHeap<PriorityQueueNode<K, V, P>> _heap { get; set; }
		private Comparer<PriorityQueueNode<K, V, P>> _priorityComparer { get; set; }
		private HashSet<K> _keysList { get; set; }


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
				_keysList = new HashSet<K> ();

				this._priorityComparer = 
					priorityComparer ?? (new PriorityQueueNodeComparer<K, V, P>());

				_heap = new MaxHeap<PriorityQueueNode<K, V, P>> (capacity, this._priorityComparer);
			}
			else
			{
				throw new ArgumentOutOfRangeException ("Please provide a valid capacity.");
			}
		}


        /// <summary>
		/// Remove the highest priority element and return it.
        /// </summary>
        /// <returns>The highest priority.</returns>
        public V PullHighestPriority()
        {
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}

			return _heap.ExtractMax ().Value;
        }


        /// <summary>
		/// Returns the highest priority element.
        /// </summary>
        /// <returns>The at highest priority.</returns>
        public V PeekAtHighestPriority()
        {
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}

			return _heap.Peek ().Value;
        }


		/// <summary>
		/// Enqueue the specified key and value without priority.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void Enqueue(K key, V value)
		{
			Enqueue (key, value, default(P));
		}


		/// <summary>
		/// Enqueue the specified key, value and priority.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		/// <param name="priority">Priority.</param>
		public void Enqueue(K key, V value, P priority)
		{
			if (_keysList.Contains (key))
			{
				throw new ArgumentException ("Key is already used.");
			}

			_keysList.Add (key);
			var newNode = new PriorityQueueNode<K, V, P> (key, value, priority);
			_heap.Insert (newNode);
		}


		/// <summary>
		/// Dequeue this instance.
		/// </summary>
		public V Dequeue()
		{
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}

			var highest = _heap.Peek ();
			_keysList.Remove (highest.Key);

			_heap.RemoveMax();
			return highest.Value;
		}


		/// <summary>
		/// Sets the priority.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="newPriority">New priority.</param>
		public void SetPriority(K key, P newPriority)
		{
			// Handle boundaries errors
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}
			else if (!_keysList.Contains (key))
			{
				throw new KeyNotFoundException ();
			}

			var keyComparer = Comparer<K>.Default;

			for (int i = 0; i < _heap.Count; ++i)
			{
				if (keyComparer.Compare (_heap [i].Key, key) == 0)
				{
					_heap [i].Priority = newPriority;
					break;
				}
			}
		}


		/// <summary>
		/// Removes the node that has the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public void Remove(K key)
		{
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}

			var keyComparer = Comparer<K>.Default;

			Predicate<PriorityQueueNode<K, V, P>> match = 
				new Predicate<PriorityQueueNode<K, V, P>> (
					item => keyComparer.Compare(item.Key, key) == 0);

			_heap.RemoveAll (match);
		}


		/// <summary>
		/// Removes the node that has the specified key and value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public void Remove(K key, V value)
        {
			if (_heap.IsEmpty)
			{
				throw new ArgumentOutOfRangeException ("Queue is empty.");
			}

			var keyComparer = Comparer<K>.Default;
			var valueComparer = Comparer<V>.Default;

			Predicate<PriorityQueueNode<K, V, P>> match = 
				new Predicate<PriorityQueueNode<K, V, P>> (
					item => 
					keyComparer.Compare(item.Key, key) == 0 && 
					valueComparer.Compare(item.Value, value) == 0);

			_heap.RemoveAll (match);
        }


		/// <summary>
		/// Clear this priority queue.
		/// </summary>
		public void Clear()
		{
			_heap.Clear ();
		}
    }


	/// <summary>
	/// The Priority-queue node.
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


	/// <summary>
	/// Keyed Priority-queue node comparer.
	/// </summary>
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
