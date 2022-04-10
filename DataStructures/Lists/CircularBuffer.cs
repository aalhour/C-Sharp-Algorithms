using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Lists
{
    public class CircularBuffer<T> : ICollection<T>
    {
        private T[] _circularBuffer;
        private int _end;
        private int _start;
        private static readonly int _defaultBufferLength = 10;

        /// <summary>
        /// Returns the length of the buffer
        /// </summary>
        public int Length => _circularBuffer.Length;

        /// <summary>
        ///  Checks if no element is inserted into the buffer
        /// </summary>
        public bool IsEmpty => _count == 0;

        /// <summary>
        /// Checks if the buffer is filled up
        /// </summary>
        public bool IsFilledUp => Count == Length;

        /// <summary>
        /// Controls whether data should be overridden when it is continously inserted without reading
        /// </summary>
        public bool CanOverride
        {
            get;
        }

        /// <summary>
        /// Initializes a circular buffer with initial length of 10
        /// </summary>
        public CircularBuffer(bool canOverride = true) : this(_defaultBufferLength, canOverride)
        {
        }

        /// <summary>
        /// Initializes a circular buffer with given length
        /// </summary>
        /// <param name="length">The length of the buffer</param>
        public CircularBuffer(int length, bool canOverride = true)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException("length can not be zero or negative");
            }
            _circularBuffer = new T[length];
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
            if (CanOverride == false && IsFilledUp == true)
            {
                throw new CircularBufferFullException($"Circular Buffer is filled up. {value} can not be inserted");
            }
            InnerInsert(value);
        }

        // Inserts data into the buffer without checking if it is full
        private void InnerInsert(T value)
        {
            _circularBuffer[_end] = value;
            _end = (_end + 1) % Length;
            if (IsFilledUp)
            {
                _start = _end;
            }
            else
            {
                ++_count;
            }
        }

        /// <summary>
        ///     Reads and removes the value in front of the buffer, and places the next value in front.
        /// </summary>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The Circular Buffer is empty");
            }

            var result = _circularBuffer[_start];
            _circularBuffer[_start] = default;
            _start = (_start + 1) % Length;
            --_count;
            return result;
        }





        #region IEnumerable Implementation
        public IEnumerator<T> GetEnumerator()
        {
            if (!IsEmpty)
            {
                if (_start < _end)
                {
                    for (int i = _start; i < _end; i++)
                    {
                        yield return _circularBuffer[i];
                    }
                }
                else
                {
                    for (int i = _start; i < _circularBuffer.Length; i++)
                    {
                        yield return _circularBuffer[i];
                    }

                    for (int i = 0; i < _end; i++)
                    {
                        yield return _circularBuffer[i];
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region ICollection Implementation
        private int _count;
        /// <summary>
        /// Returns the number of elements.
        /// </summary>
        public int Count => _count;
        /// <summary>
        /// Checks whether this collection is readonly
        /// </summary>
        public bool IsReadOnly => false;
        /// <summary>
        /// Clears this instance
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _start = 0;
            _end = 0;
            Array.Clear(_circularBuffer, 0, _circularBuffer.Length);
        }
        /// <summary>
        /// Checks whether the buffer contains an item
        /// </summary>
        public bool Contains(T item)
        {
            if (IsEmpty)
            {
                return false;
            }
            else if (_start < _end)
            {
                if (item == null)
                {
                    for (int i = _start; i < _end; i++)
                    {
                        if (_circularBuffer[i] == null)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    for (int i = _start; i < _end; i++)
                    {
                        if (_circularBuffer[i] != null && _circularBuffer[i].Equals(item))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (item == null)
                {
                    for (int i = _start; i < _circularBuffer.Length; i++)
                    {
                        if (_circularBuffer[i] == null)
                        {
                            return true;
                        }
                    }

                    for (int i = 0; i < _end; i++)
                    {
                        if (_circularBuffer[i] == null)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    for (int i = _start; i < _circularBuffer.Length; i++)
                    {
                        if (_circularBuffer[i] != null && _circularBuffer[i].Equals(item))
                        {
                            return true;
                        }
                    }

                    for (int i = 0; i < _end; i++)
                    {
                        if (_circularBuffer[i] != null && _circularBuffer[i].Equals(item))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
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

            if (array.Length == 0 || arrayIndex >= array.Length || arrayIndex < 0)
            {
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
            if (!IsEmpty && Contains(item))
            {
                int shiftSpace = 0;
                if (_start < _end)
                {
                    for (int i = _start; i < _end; i++)
                    {
                        if ((item == null && _circularBuffer[i] ==null) || (_circularBuffer[i] != null && _circularBuffer[i].Equals(item)))
                        {
                            shiftSpace++;
                        }
                        else
                        {
                            if (shiftSpace > 0)
                            {
                                _circularBuffer[i - shiftSpace] = _circularBuffer[i];
                            }
                        }
                    }
                    _end -= shiftSpace;
                    _count -= shiftSpace;
                }
                else
                {
                    for (int i = _start; i < _circularBuffer.Length; i++)
                    {
                        if ((item == null && _circularBuffer[i] == null) || (_circularBuffer[i] != null && _circularBuffer[i].Equals(item)))
                        {
                            shiftSpace++;
                        }
                        else
                        {
                            if (shiftSpace > 0)
                            {
                                _circularBuffer[i - shiftSpace] = _circularBuffer[i];
                            }
                        }
                    }

                    for (int i = 0; i < _end; i++)
                    {
                        if ((item == null && _circularBuffer[i] == null) || (_circularBuffer[i] != null && _circularBuffer[i].Equals(item)))
                        {
                            shiftSpace++;
                        }
                        else
                        {
                            if (shiftSpace > i)
                            {
                                _circularBuffer[_circularBuffer.Length + (i - shiftSpace)] = _circularBuffer[i];
                            }
                            else
                            {
                                _circularBuffer[i - shiftSpace] = _circularBuffer[i];
                            }
                        }
                    }

                    _end -= shiftSpace;
                    if (_end < 0)
                    {
                        _end = _circularBuffer.Length + _end;
                    }
                    _count -= shiftSpace;
                }
            }

            return false;
        }
        #endregion
    }

    public class CircularBufferFullException : Exception
    {
        public CircularBufferFullException(string message) : base(message)
        {
        }
    }
}