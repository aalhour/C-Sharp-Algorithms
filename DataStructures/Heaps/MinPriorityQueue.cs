using System;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Heaps
{
    /// <summary>
    /// Implements the Priority Queue Data Structure.
    /// <typeparam name="TKey">Node's Value type</typeparam>
    /// <typeparam name="TPriority">Node's Priority type</typeparam>
    /// </summary>
    public class MinPriorityQueue<TKey, TPriority>
        where TKey : IComparable<TKey>
        where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        private BinaryMinHeap<PriorityQueueNode<TKey, TPriority>> _heap { get; set; }
        private Comparer<PriorityQueueNode<TKey, TPriority>> _priorityComparer { get; set; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public MinPriorityQueue() : this(0, null) { }

        public MinPriorityQueue(uint capacity) : this(capacity, null) { }

        public MinPriorityQueue(uint capacity, Comparer<PriorityQueueNode<TKey, TPriority>> priorityComparer)
        {
            // Make sure the TPriority is elegible for a priority
            if (!_validPriorityType())
                throw new NotSupportedException("The priority type is not supported.");

            // Initialize comparer
            if (priorityComparer == null)
                _priorityComparer = Comparer<PriorityQueueNode<TKey, TPriority>>.Default;
            else
                _priorityComparer = priorityComparer;

            // Initialize.
            _heap = new BinaryMinHeap<PriorityQueueNode<TKey, TPriority>>((int)capacity, this._priorityComparer);
        }


        /// <summary>
        /// Validates the Type of TPriority. Returns true if acceptable, false otherwise.
        /// </summary>
        /// <returns></returns>
        private bool _validPriorityType()
        {
            bool isValid = false;
            TypeCode typeCode = Type.GetTypeCode(typeof(TPriority));

            switch (typeCode)
            {
                //case TypeCode.DateTime:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    isValid = true;
                    break;
                default:
                    isValid = false;
                    break;
            }

            return isValid;
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
        /// </summary>
        public bool IsEmpty
        {
            get { return _heap.IsEmpty; }
        }

        /// <summary>
        /// Get the default max priority, if set, raises an exception if not set.
        /// Also sets the default max priority.
        /// </summary>
        public TPriority DefaultMaxPriority
        {
            get
            {
                object maxValue = default(TPriority);
                TypeCode typeCode = Type.GetTypeCode(typeof(TPriority));
                switch (typeCode)
                {
                    case TypeCode.Byte:
                        maxValue = byte.MaxValue;
                        break;
                    case TypeCode.Char:
                        maxValue = char.MaxValue;
                        break;
                    case TypeCode.DateTime:
                        maxValue = DateTime.MaxValue;
                        break;
                    case TypeCode.Decimal:
                        maxValue = decimal.MaxValue;
                        break;
                    case TypeCode.Double:
                        maxValue = decimal.MaxValue;
                        break;
                    case TypeCode.Int16:
                        maxValue = short.MaxValue;
                        break;
                    case TypeCode.Int32:
                        maxValue = int.MaxValue;
                        break;
                    case TypeCode.Int64:
                        maxValue = long.MaxValue;
                        break;
                    case TypeCode.SByte:
                        maxValue = sbyte.MaxValue;
                        break;
                    case TypeCode.Single:
                        maxValue = float.MaxValue;
                        break;
                    case TypeCode.UInt16:
                        maxValue = ushort.MaxValue;
                        break;
                    case TypeCode.UInt32:
                        maxValue = uint.MaxValue;
                        break;
                    case TypeCode.UInt64:
                        maxValue = ulong.MaxValue;
                        break;
                }

                return (TPriority)maxValue;
            }
        }

        /// <summary>
        /// Returns the highest priority element.
        /// </summary>
        /// <returns>The at highest priority.</returns>
        public TKey PeekAtMinPriority()
        {
            if (_heap.IsEmpty)
            {
                throw new ArgumentOutOfRangeException("Queue is empty.");
            }

            return _heap.Peek().Value;
        }


        /// <summary>
        /// Enqueue the specified key, with the default-max-priority value.
        /// </summary>
        public void Enqueue(TKey value)
        {
            var newNode = new PriorityQueueNode<TKey, TPriority>(value, DefaultMaxPriority);
            _heap.Add(newNode);
        }

        /// <summary>
        /// Enqueue the specified key, value and priority.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="priority">Priority.</param>
        public void Enqueue(TKey value, TPriority priority)
        {
            var newNode = new PriorityQueueNode<TKey, TPriority>(value, priority);
            _heap.Add(newNode);
        }

        /// <summary>
        /// Dequeue this instance.
        /// </summary>
        public TKey Dequeue()
        {
            if (_heap.IsEmpty)
                throw new ArgumentOutOfRangeException("Queue is empty.");

            return _heap.ExtractMin().Value;
        }

        /// <summary>
        /// Sets the priority.
        /// </summary>
        public void UpdatePriority(TKey key, TPriority newPriority)
        {
            // Handle boundaries errors
            if (_heap.IsEmpty)
                throw new ArgumentOutOfRangeException("Queue is empty.");

            int index = -1;
            for (int i = 0; i < _heap.Count; ++i)
            {
                if (_heap[i].Value.IsEqualTo(key))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                throw new KeyNotFoundException();

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
    /// <typeparam name="TKey">Node's Value type</typeparam>
    public class PriorityQueueNode<TKey, TPriority> : IComparable<PriorityQueueNode<TKey, TPriority>>
        where TKey : IComparable<TKey>
        where TPriority : IComparable<TPriority>
    {
        public TKey Value { get; set; }
        public TPriority Priority { get; set; }

        public PriorityQueueNode() : this(default(TKey), default(TPriority)) { }

        public PriorityQueueNode(TKey value, TPriority priority)
        {
            this.Value = value;
            this.Priority = priority;
        }

        public int CompareTo(PriorityQueueNode<TKey, TPriority> other)
        {
            if (other == null)
                return -1;

            return this.Priority.CompareTo(other.Priority);
        }
    }//end-of-node-class

}
