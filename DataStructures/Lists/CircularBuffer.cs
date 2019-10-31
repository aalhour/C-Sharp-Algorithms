using System;

namespace DataStructures.Lists 
{
    public class CircularBuffer<T> 
    {
        private T[] _circularBuffer;
        private int _end;
        private int _start;
        private static readonly int _defaultBufferLength = 10;

        /// <summary>
        /// Controls whether data should be overridden when it is continously inserted without reading
        /// </summary>
        public bool CanOverride { get; }
        public bool IsEmpty { get => _end == _start; }
        public int Length { get => _circularBuffer.Length - 1; }
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
    }

    public class CircularBufferFullException: Exception 
    {
        public CircularBufferFullException(string message) : base(message) 
        {
        }
    }
}