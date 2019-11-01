using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Lists 
{
    public class CircularBuffer<T> : IEnumerable<T> , ICollection<T> where T : IComparable<T> {
        private T[] _circularBuffer;
        private int _end;
        private int _start;
        private int _length { get => _circularBuffer.Length - 1; }
        private readonly T _default;
        private static readonly int _defaultBufferLength = 10;

        /// <summary>
        /// Controls whether data should be overridden when it is continously inserted without reading
        /// </summary>
        public bool CanOverride { get; }
        public bool IsEmpty { get => _end == _start; }
        public bool IsFilledUp { get => ((_end + 1) % _circularBuffer.Length == _start) && !_circularBuffer[_start].Equals(_circularBuffer[_end]); }


        /// <summary>
        /// Initializes a circular buffer with initial length of 10
        /// </summary>
        public CircularBuffer(bool canOverride=true) : this(_defaultBufferLength, canOverride) 
        {
        }

        /// <summary>
        /// Initializes a circular buffer with given length
        /// </summary>
        /// <param name="length">The length of the buffer</param>
        public CircularBuffer(int length, bool canOverride=true) 
        {
            if (length < 1) 
            {
                throw new ArgumentOutOfRangeException("length can not be zero or negative");
            }
            _circularBuffer = new T[length + 1];
            _default = _circularBuffer[length];
            _end = 0;
            _start = 0;
            CanOverride = canOverride;
        }

        /// <summary>
        /// Writes value to the back of the buffer
        /// </summary>
        /// <param name="value">value to be added to the buffer</param>
        public void Add(T value) 
        {
            if (CanOverride) 
            {
                innerInsert(value);
            }
            else 
            {
                if (IsFilledUp) 
                {
                    throw new CircularBufferFullException($"Circular Buffer is filled up. {value} can not be inserted");
                }
                innerInsert(value);
            }
        }

        // Inserts data into the buffer without checking if it is full
        private void innerInsert(T value) 
        {
            _circularBuffer[_end] = value;
            _end = (_end + 1) % _circularBuffer.Length;
            if (_end == _start) 
            {
                _start = (_start + 1) % _circularBuffer.Length;
            }
        }

        /// <summary>
        ///     Reads and removes the value in front of the buffer, and places the next value in front.
        /// </summary>
        public T Pop() 
        {
            var result = _circularBuffer[_start];
            _circularBuffer[_start] = _circularBuffer[_end];
            _start = (_start + 1) % _circularBuffer.Length;
            return result;
        }

        #region IEnumerable Implementation
        public IEnumerator<T> GetEnumerator() 
        {
            for (int i = 0; i < Count; i++) {
                yield return _circularBuffer[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return GetEnumerator();
        }
        #endregion

        #region ICollection Implementation
        /// <summary>
        /// Returns the number of elements.
        /// </summary>
        public int Count 
        {
            get { return _length; }
        }
        /// <summary>
        /// Checks whether this collection is readonly
        /// </summary>
        public bool IsReadOnly 
        {
            get { return false; }
        }
        /// <summary>
        /// Clears this instance
        /// </summary>
        public void Clear() 
        {
            for (int i = 0; i < _circularBuffer.Length; i++) 
            {
                _circularBuffer[i] = _default;
            }
        }
        /// <summary>
        /// Checks whether the buffer contains an item
        /// </summary>
        public bool Contains(T item) 
        {
            return _circularBuffer.Contains(item);
        }
        /// <summary>
        /// Copies this buffer to an array
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex) 
        {
            if (array == null) 
            {
                throw new ArgumentNullException("array can not be null");
            }

            if (array.Length == 0 || arrayIndex >= array.Length || arrayIndex < 0) {
                throw new IndexOutOfRangeException();
            }
     
            // Get enumerator
            var enumarator = GetEnumerator();

            // Copy elements if there is any in the buffer and if the index is within the valid range
            while (arrayIndex < array.Length) 
            {
                if (enumarator.MoveNext()) 
                {
                    array[arrayIndex] = enumarator.Current;
                    arrayIndex++;
                }
                else 
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Removes an item from the buffer
        /// </summary>
        public bool Remove(T item) 
        {
            if (Contains(item)) 
            {
                for (int i = 0; i < _circularBuffer.Length; i++) 
                {
                    if (item.Equals(_circularBuffer[i])) 
                    {
                        _circularBuffer[i] = _default;
                    }
                }
                return true;
            }
            else 
            {
                return false;
            }
        }
        #endregion
    }

    public class CircularBufferFullException: Exception 
    {
        public CircularBufferFullException(string message) : base(message) 
        {
        }
    }
}