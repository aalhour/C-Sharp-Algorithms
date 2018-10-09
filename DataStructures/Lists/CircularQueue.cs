using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.CircularQueueSpace
{
    [Serializable]
    public class CircularQueue<T>:ICollection<T>,IEnumerable<T>
    {
        private int? _head;       // remove from head
        private int? _tail;       // insert at tail

        /// <summary>
        /// Creates a non readonly Queue
        /// </summary>
        public CircularQueue()
        {
            Init(false);
            QueueList = new List<T>();
        }

        /// <summary>
        /// Creates a Queue
        /// </summary>
        /// <param name="isReadOnly">Determines if the Queue is readonly</param>
        public CircularQueue(bool isReadOnly)
        {
            Init(isReadOnly);
            QueueList = new List<T>();
        }

        /// <summary>
        /// Creates a Queue
        /// </summary>
        /// <param name="quesize">Capacity of the Queue</param>
        /// <param name="isReadOnly">Determines if the Queue is readonly</param>
        public CircularQueue(int quesize, bool isReadOnly)
        {
            Init(isReadOnly);
            QueueList = new List<T>(quesize);
        }

        /// <summary>
        /// Creates a Queue
        /// </summary>
        /// <param name="quesize">Capacity of the Queue</param>
        public CircularQueue(int quesize)
        {
            Init(false);
            QueueList = new List<T>(quesize);
        }

        /// <summary>
        /// Init the Queue
        /// </summary>
        /// <param name="isReadOnly">Set if the Queue is going to be readonly</param>
        public void Init(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
            _head = null;
            _tail = null;
        }

        /// <summary>
        /// Add the element to the Queue
        /// </summary>
        /// <param name="elem">Element to be added</param>
        public void Enqueue(T elem)  // if next index to tail == head => Q is FULL
        {
            Contract.Assert(elem != null, "The element to insert was null");
            Contract.Assert(IsReadOnly == false, "The Circular queue is read only");

            int? newIndex = null;
            newIndex = _tail!=null ? NextIndex(_tail.Value) : NextIndex(null);

            _tail = newIndex;
            Contract.Assert(newIndex != null, "The new index was null");

            QueueList.Insert(newIndex.Value,elem);
            if (_head == null)
                _head = newIndex;
        }

        /// <summary>
        /// Remove an element from the Queue
        /// </summary>
        /// <returns>Element removed from the Queue</returns>
        public T Dequeue()  // After removing from head, if that was the only element in Q
        // Mark Q to be empty by setting head and tail to -1
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Trying to dequeue from an empty queue");
            }
            Contract.Assert(IsReadOnly == false, "The Circular queue is read only");

            var elem = QueueList[_head.Value];
            QueueList[_head.Value] = default(T);

            if (_head == _tail)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _head = NextIndex(_head.Value);
            }

            return elem;
        }

        /// <summary>
        /// Return the next index where to insert
        /// </summary>
        /// <param name="index">Last index used</param>
        /// <returns>Next index</returns>
        public int? NextIndex(int? index)
        {
            //if the _queueList has capacity 0, create a list with capacity 1
            if (QueueList.Capacity == 0)
            {
                QueueList = new List<T>(1);
            }
            if (index == null)
                return 0;
            return (index.Value + 1) % QueueList.Capacity;
        }

        /// <summary>
        /// Return an enumerator from IEnumerator
        /// </summary>
        /// <returns>Enumerator from IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return QueueList.GetEnumerator();
        }

        /// <summary>
        /// Return an enumerator from IEnumerable
        /// </summary>
        /// <returns>Enumerator from IEnumerable</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return QueueList.GetEnumerator();
        }

        /// <summary>
        /// Add an element to the Queue
        /// </summary>
        /// <param name="item">element to be added</param>
        public void Add(T item)
        {
            Contract.Assert(IsReadOnly == false, "The Circular queue is read only");
            Enqueue(item);
        }

        /// <summary>
        /// Reset the Queue
        /// </summary>
        public void Clear()
        {
            Contract.Assert(IsReadOnly == false, "The Circular queue is read only");
            QueueList = new List<T>();
        }

        /// <summary>
        /// Check if the queue contains the element
        /// </summary>
        /// <param name="item">Element to be checked</param>
        /// <returns>Whether the searched element was found</returns>
        public bool Contains(T item)
        {
            return QueueList.Contains(item);
        }

        /// <summary>
        /// Copy the Queue to an array from the given index
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="arrayIndex">Starting index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentNullException>(array != null, $"{nameof(array)}");
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, $"{nameof(arrayIndex)}");
            Contract.Requires<ArgumentException>(arrayIndex <= Count, "ArrayIndex less than count");

            var i = arrayIndex;
            foreach (var element in QueueList)
            {
                array.SetValue(element, i++);
            }
        }

        /// <summary>
        /// Remove an element from the Queue
        /// </summary>
        /// <param name="item">Element to be removed</param>
        /// <returns>Whether the element was removed or not</returns>
        public bool Remove(T item)
        {
            Contract.Assert(IsReadOnly == false, "The Circular queue is read only");

            if (!QueueList.Contains(item)) return false;
            QueueList.Remove(item);
            return true;
        }

        /// <summary>
        /// Count of the element in the Queue
        /// </summary>
        public int Count { 
             get { return QueueList.Count; }
        }

        /// <summary>
        /// Specifies if the Queue is readonly
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Returns the internal queue
        /// </summary>
        public List<T> QueueList { get; private set; }
    }
}
