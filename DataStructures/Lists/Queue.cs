using System;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// The Queue (FIFO) Data Structure.
    /// </summary>
    public class Queue<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLE.
        /// </summary>
        private int _size { get; set; }
        private int _headPointer { get; set; }
        private int _tailPointer { get; set; }

        // The internal collection.
        private T[] _collection { get; set; }
        private const int _defaultCapacity = 8;

        // This sets the default maximum array length to refer to MAXIMUM_ARRAY_LENGTH_x64
        // Set the flag IsMaximumCapacityReached to false
        bool DefaultMaxCapacityIsX64 = true;
        bool IsMaximumCapacityReached = false;

        // The C# Maximum Array Length (before encountering overflow)
        // Reference: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        public const int MAXIMUM_ARRAY_LENGTH_x64 = 0X7FEFFFFF; //x64
        public const int MAXIMUM_ARRAY_LENGTH_x86 = 0x8000000; //x86


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Queue() : this(_defaultCapacity) { }

        public Queue(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _size = 0;
            _headPointer = 0;
            _tailPointer = 0;
            _collection = new T[initialCapacity];
        }


        /// <summary>
        /// Resize the internal array to a new size.
        /// </summary>
        private void _resize(int newSize)
        {
            if (newSize > _size && !IsMaximumCapacityReached)
            {
                int capacity = (_collection.Length == 0 ? _defaultCapacity : _collection.Length * 2);

                // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
                // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
                int maxCapacity = (DefaultMaxCapacityIsX64 == true ? MAXIMUM_ARRAY_LENGTH_x64 : MAXIMUM_ARRAY_LENGTH_x86);

                // Handle the new proper size
                if (capacity < newSize)
                    capacity = newSize;

                if (capacity >= maxCapacity)
                {
                    capacity = maxCapacity - 1;
                    IsMaximumCapacityReached = true;
                }

                // Try resizing and handle overflow
                try
                {
                    //Array.Resize (ref _collection, newSize);

                    var tempCollection = new T[newSize];
                    Array.Copy(_collection, _headPointer, tempCollection, 0, _size);
                    _collection = tempCollection;
                }
                catch (OutOfMemoryException)
                {
                    if (DefaultMaxCapacityIsX64 == true)
                    {
                        DefaultMaxCapacityIsX64 = false;
                        _resize(capacity);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// Returns count of elements in queue
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Checks whether the queue is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return _size == 0; }
        }

        /// <summary>
        /// Returns the top element in queue
        /// </summary>
        public T Top
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("Queue is empty.");

                return _collection[_headPointer];
            }
        }

        /// <summary>
        /// Inserts an element at the end of the queue
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Enqueue(T dataItem)
        {
            if (_size == _collection.Length)
            {
                try
                {
                    _resize(_collection.Length * 2);
                }
                catch (OutOfMemoryException ex)
                {
                    throw ex;
                }
            }

            // Enqueue item at tail and then increment tail
            _collection[_tailPointer++] = dataItem;

            // Wrap around
            if (_tailPointer == _collection.Length)
                _tailPointer = 0;

            // Increment size
            _size++;
        }

        /// <summary>
        /// Removes the Top Element from queue, and assigns it's value to the "top" parameter.
        /// </summary>
        /// <return>The top element container.</return>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Queue is empty.");

            var topItem = _collection[_headPointer];
            _collection[_headPointer] = default(T);

            // Decrement the size
            _size--;

            // Increment the head pointer
            _headPointer++;

            // Reset the pointer
            if (_headPointer == _collection.Length)
                _headPointer = 0;

            // Shrink the internal collection
            if (_size > 0 && _collection.Length > _defaultCapacity && _size <= _collection.Length / 4)
            {
                // Get head and tail
                var head = _collection[_headPointer];
                var tail = _collection[_tailPointer];

                // Shrink
                _resize((_collection.Length / 3) * 2);

                // Update head and tail pointers
                _headPointer = Array.IndexOf(_collection, head);
                _tailPointer = Array.IndexOf(_collection, tail);
            }

            return topItem;
        }

        /// <summary>
        /// Returns an array version of this queue.
        /// </summary>
        /// <returns>System.Array.</returns>
        public T[] ToArray()
        {
            var array = new T[_size];

            int j = 0;
            for (int i = 0; i < _size; ++i)
            {
                array[j] = _collection[_headPointer + i];
                j++;
            }

            return array;
        }

        /// <summary>
        /// Returns a human-readable, multi-line, print-out (string) of this queue.
        /// </summary>
        public string ToHumanReadable()
        {
            var array = ToArray();
            string listAsString = string.Empty;

            int i = 0;
            for (i = 0; i < Count; ++i)
                listAsString = String.Format("{0}[{1}] => {2}\r\n", listAsString, i, array[i]);

            return listAsString;
        }


        /********************************************************************************/


        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator() as IEnumerator<T>;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
