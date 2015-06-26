using System;
using System.Collections.Generic;

namespace DataStructures.Heaps
{
    /// <summary>
    /// Implements the Priority Queue Data Structure.
    /// <typeparam name="V">Node's Value type</typeparam>
    /// <typeparam name="P">Node's Priority type</typeparam>
    /// </summary>
    public class PriorityQueue<V, P> where P : IComparable<P>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        private BinaryMaxHeap<PriorityQueueNode<V, P>> _heap { get; set; }
        private Comparer<PriorityQueueNode<V, P>> _priorityComparer { get; set; }


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
        public PriorityQueue(int capacity, Comparer<PriorityQueueNode<V, P>> priorityComparer)
        {
            if (capacity >= 0)
            {
                if (priorityComparer == null)
                {
                    _priorityComparer = Comparer<PriorityQueueNode<V, P>>.Default;
                }
                else
                {
                    _priorityComparer = priorityComparer;
                }

                _heap = new BinaryMaxHeap<PriorityQueueNode<V, P>>(capacity, this._priorityComparer);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Please provide a valid capacity.");
            }
        }


        /// <summary>
        /// Returns the count of elements in the queue.
        /// </summary>
        public int Count()
        {
            return _heap.Count();
        }


        /// <summary>
        /// Checks if the queue is empty
        /// <returns>True if queue is empty; false otherwise.</returns>
        /// </summary>
        public bool IsEmpty()
        {
            return _heap.IsEmpty();
        }


        /// <summary>
        /// Returns the highest priority element.
        /// </summary>
        /// <returns>The at highest priority.</returns>
        public V PeekAtHighestPriority()
        {
            if (_heap.IsEmpty())
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            return _heap.Peek().Value;
        }


        /// <summary>
        /// Enqueue the specified value without priority.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Enqueue(V value)
        {
            Enqueue(value, default(P));
        }


        /// <summary>
        /// Enqueue the specified key, value and priority.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="priority">Priority.</param>
        public void Enqueue(V value, P priority)
        {
            var newNode = new PriorityQueueNode<V, P>(value, priority);
            _heap.Add(newNode);
        }


        /// <summary>
        /// Dequeue this instance.
        /// </summary>
        public V Dequeue()
        {
            if (_heap.IsEmpty())
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            return _heap.ExtractMax().Value;
        }


        /// <summary>
        /// Sets the priority.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="newPriority">New priority.</param>
        public void SetPriority(int index, P newPriority)
        {
            // Handle boundaries errors
            if (_heap.IsEmpty())
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }
            else if (index < 0 || index >= _heap.Count())
            {
                throw new ArgumentOutOfRangeException("Please provide a valid index.");
            }

            _heap[index].Priority = newPriority;
        }


        /// <summary>
        /// Removes the node that has the specified value.
        /// </summary>
        /// <param name="value">Value.</param>
        //public void Remove(V value)
        //{
        //    if (_heap.IsEmpty)
        //    {
        //        throw new ArgumentOutOfRangeException ("Queue is empty.");
        //    }
        //
        //    var valueComparer = Comparer<V>.Default;
        //
        //    Predicate<PriorityQueueNode<V, P>> match = 
        //        new Predicate<PriorityQueueNode<V, P>> (
        //            item => valueComparer.Compare(item.Value, value) == 0);
        //
        //    _heap.RemoveAll (match);
        //}


        /// <summary>
        /// Clear this priority queue.
        /// </summary>
        public void Clear()
        {
            _heap.Clear();
        }
    }



    /// <summary>
    /// The Priority-queue node.
    /// </summary>
    /// <typeparam name="K">Node's Key type</typeparam>
    /// <typeparam name="V">Node's Value type</typeparam>
    public class PriorityQueueNode<V, P> : IComparable<PriorityQueueNode<V, P>> where P : IComparable<P>
    {
        public V Value { get; set; }
        public P Priority { get; set; }

        public PriorityQueueNode() : this(default(V), default(P)) { }

        public PriorityQueueNode(V value, P priority)
        {
            this.Value = value;
            this.Priority = priority;
        }

        public int CompareTo(PriorityQueueNode<V, P> other)
        {
            if (other == null)
                return -1;

            return this.Priority.CompareTo(other.Priority);
        }
    }//end-of-node-class


    /// <summary>
    /// Priority-queue node comparer.
    /// </summary>
    public class PriorityQueueNodeComparer<V, P> : IComparer<PriorityQueueNode<V, P>> where P : IComparable<P>
    {
        public int Compare(PriorityQueueNode<V, P> first, PriorityQueueNode<V, P> second)
        {
            return first.Priority.CompareTo(second.Priority);
        }
    }//end-of-comparer-class
}
