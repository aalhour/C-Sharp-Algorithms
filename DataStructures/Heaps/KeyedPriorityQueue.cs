using System;
using System.Collections.Generic;

namespace DataStructures.Heaps
{
    /// <summary>
    /// Implements the Keyed Priority Queue Data Structure.
    /// All nodes have: a Key, a Value, a Priority
    /// <typeparam name="K">Node's Key type</typeparam>
    /// <typeparam name="V">Node's Value type</typeparam>
    /// <typeparam name="P">Node's Priority type</typeparam>
    /// </summary>
    public class PriorityQueue<K, V, P> where P : IComparable<P>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        private BinaryMaxHeap<PriorityQueueNode<K, V, P>> _heap { get; set; }
        private Comparer<PriorityQueueNode<K, V, P>> _priorityComparer { get; set; }
        private Dictionary<K, int> _keysMap { get; set; }


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
                if (priorityComparer == null)
                {
                    _priorityComparer = Comparer<PriorityQueueNode<K, V, P>>.Default;
                }
                else
                {
                    _priorityComparer = priorityComparer;
                }

                _heap = new BinaryMaxHeap<PriorityQueueNode<K, V, P>>(capacity, this._priorityComparer);
                _keysMap = new Dictionary<K, int>();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Please provide a valid capacity.");
            }
        }


        /// <summary>
        /// Returns the count of elements in the queue.
        /// </summary>
        public int Count
        {
            get { return _heap.Count; }
        }


        /// <summary>
        /// Checks if the queue is empty
        /// <returns>True if queue is empty; false otherwise.</returns>
        /// </summary>
        public bool IsEmpty
        {
            get { return _heap.IsEmpty; }
        }


        /// <summary>
        /// Returns an array of keys
        /// </summary>
        public K[] Keys
        {
            get
            {
                var keysArray = new K[_keysMap.Count];
                _keysMap.Keys.CopyTo(keysArray, 0);
                return keysArray;
            }
        }


        /// <summary>
        /// Returns the highest priority element.
        /// </summary>
        /// <returns>The at highest priority.</returns>
        public V PeekAtHighestPriority()
        {
            if (_heap.IsEmpty)
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            return _heap.Peek().Value;
        }


        /// <summary>
        /// Enqueue the specified key and value without priority.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public void Enqueue(K key, V value)
        {
            Enqueue(key, value, default(P));
        }


        /// <summary>
        /// Enqueue the specified key, value and priority.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="priority">Priority.</param>
        public void Enqueue(K key, V value, P priority)
        {
            if (!_keysMap.ContainsKey(key))
            {
                _keysMap.Add(key, 1);
            }
            else
            {
                _keysMap[key] += 1;
            }

            var newNode = new PriorityQueueNode<K, V, P>(key, value, priority);
            _heap.Add(newNode);
        }


        /// <summary>
        /// Dequeue this instance.
        /// </summary>
        public V Dequeue()
        {
            if (_heap.IsEmpty)
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            var highest = _heap.Peek();

            // Decrement the key's counter
            _keysMap[highest.Key] = _keysMap[highest.Key] - 1;
            if (_keysMap[highest.Key] == 0)
            {
                _keysMap.Remove(highest.Key);
            }

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
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            if (!_keysMap.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var keyComparer = Comparer<K>.Default;

            for (int i = 0; i < _heap.Count; ++i)
            {
                if (keyComparer.Compare(_heap[i].Key, key) == 0)
                {
                    _heap[i].Priority = newPriority;
                    break;
                }
            }
        }


        /// <summary>
        /// Clear this priority queue.
        /// </summary>
        public void Clear()
        {
            _heap.Clear();
        }

        ///// <summary>
        ///// Removes the node that has the specified key.
        ///// </summary>
        ///// <param name="key">Key.</param>
        //public void Remove(K key)
        //{
        //    if (_heap.IsEmpty)
        //    {
        //        throw new ArgumentOutOfRangeException ("Queue is empty.");
        //    }
        //
        //    var keyComparer = Comparer<K>.Default;
        //
        //    Predicate<PriorityQueueNode<K, V, P>> match = 
        //        new Predicate<PriorityQueueNode<K, V, P>> (
        //            item => keyComparer.Compare(item.Key, key) == 0);
        //
        //    _heap.RemoveAll (match);
        //}


        ///// <summary>
        ///// Removes the node that has the specified key and value.
        ///// </summary>
        ///// <param name="key">Key.</param>
        ///// <param name="value">Value.</param>
        //public void Remove(K key, V value)
        //{
        //    if (_heap.IsEmpty)
        //    {
        //        throw new ArgumentOutOfRangeException ("Queue is empty.");
        //    }
        //
        //    var keyComparer = Comparer<K>.Default;
        //    var valueComparer = Comparer<V>.Default;
        //
        //    Predicate<PriorityQueueNode<K, V, P>> match = 
        //        new Predicate<PriorityQueueNode<K, V, P>> (
        //            item => 
        //            keyComparer.Compare(item.Key, key) == 0 && 
        //            valueComparer.Compare(item.Value, value) == 0);
        //
        //    _heap.RemoveAll (match);
        //}
    }



    /// <summary>
    /// The Priority-queue node.
    /// </summary>
    /// <typeparam name="K">Node's Key type</typeparam>
    /// <typeparam name="V">Node's Value type</typeparam>
    /// <typeparam name="P">Node's Priority type</typeparam>
    public class PriorityQueueNode<K, V, P> : IComparable<PriorityQueueNode<K, V, P>> where P : IComparable<P>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public P Priority { get; set; }

        public PriorityQueueNode() : this(default(K), default(V), default(P)) { }

        public PriorityQueueNode(K key, V value, P priority)
        {
            this.Key = key;
            this.Value = value;
            this.Priority = priority;
        }

        public int CompareTo(PriorityQueueNode<K, V, P> other)
        {
            if (other == null)
                return -1;

            return this.Priority.CompareTo(other.Priority);
        }
    }//end-of-node-class


    /// <summary>
    /// Keyed Priority-queue node comparer.
    /// </summary>
    public class PriorityQueueNodeComparer<K, V, P> : IComparer<PriorityQueueNode<K, V, P>> where P : IComparable<P>
    {
        public int Compare(PriorityQueueNode<K, V, P> first, PriorityQueueNode<K, V, P> second)
        {
            return first.Priority.CompareTo(second.Priority);
        }
    }//end-of-comparer-class

}
