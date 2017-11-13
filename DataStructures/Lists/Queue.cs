using System;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <inheritdoc />
    /// <summary>
    /// The Queue (FIFO) Data Structure.
    /// </summary>
    public class Queue<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// INSTANCE VARIABLE.
        /// </summary>
        private int Size { get; set; }
        private int HeadPointer { get; set; }
        private int TailPointer { get; set; }

        // The internal collection.
        private T[] Collection { get; set; }
        private const int DefaultCapacity = 8;

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
        public Queue() : this(DefaultCapacity) { }

        public Queue(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Size = 0;
            HeadPointer = 0;
            TailPointer = 0;
            Collection = new T[initialCapacity];
        }


        /// <summary>
        /// Resize the internal array to a new size.
        /// </summary>
        private void Resize(int newSize)
        {
            if (newSize <= Size || IsMaximumCapacityReached) return;

            var capacity = (Collection.Length == 0 ? DefaultCapacity : Collection.Length * 2);

            // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            var maxCapacity = (DefaultMaxCapacityIsX64 == true ? MAXIMUM_ARRAY_LENGTH_x64 : MAXIMUM_ARRAY_LENGTH_x86);

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
                Array.Copy(Collection, HeadPointer, tempCollection, 0, Size);
                Collection = tempCollection;
            }
            catch (OutOfMemoryException)
            {
                if (DefaultMaxCapacityIsX64 == true)
                {
                    DefaultMaxCapacityIsX64 = false;
                    Resize(capacity);
                }
                else
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Returns count of elements in queue
        /// </summary>
        public int Count => Size;

        /// <summary>
        /// Checks whether the queue is empty.
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Returns the top element in queue
        /// </summary>
        public T Top
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("Queue is empty.");

                return (T)Collection[HeadPointer];
            }
        }

        /// <summary>
        /// Inserts an element at the end of the queue
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Enqueue(T dataItem)
        {
            if (Size == Collection.Length)
            {
                try
                {
                    Resize(Collection.Length * 2);
                }
                catch (OutOfMemoryException ex)
                {
                    throw ex;
                }
            }

            // Enqueue item at tail and then increment tail
            Collection[TailPointer++] = dataItem;

            // Wrap around
            if (TailPointer == Collection.Length)
                TailPointer = 0;

            // Increment size
            Size++;
        }

        /// <summary>
        /// Removes the Top Element from queue, and assigns it's value to the "top" parameter.
        /// </summary>
        /// <return>The top element container.</return>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Queue is empty.");

            var topItem = Collection[HeadPointer];
            Collection[HeadPointer] = default(T);

            // Decrement the size
            Size--;

            // Increment the head pointer
            HeadPointer++;

            // Reset the pointer
            if (HeadPointer == Collection.Length)
                HeadPointer = 0;

            // Shrink the internal collection
            if (Size <= 0 || Collection.Length <= DefaultCapacity || Size > Collection.Length / 4)
                return (T) topItem;

            // Get head and tail
            var head = Collection[HeadPointer];
            var tail = Collection[TailPointer];

            // Shrink
            Resize((Collection.Length / 3) * 2);

            // Update head and tail pointers
            HeadPointer = Array.IndexOf(Collection, head);
            TailPointer = Array.IndexOf(Collection, tail);

            return (T)topItem;
        }

        /// <summary>
        /// Returns an array version of this queue.
        /// </summary>
        /// <returns>System.Array.</returns>
        public T[] ToArray()
        {
            var array = new T[Size];

            int j = 0;
            for (int i = 0; i < Size; ++i)
            {
                array[j] = (T)Collection[HeadPointer + i];
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

            var i = 0;
            for (i = 0; i < Count; ++i)
                listAsString = $"{listAsString}[{i}] => {array[i]}\r\n";

            return listAsString;
        }


        /********************************************************************************/


        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator() as IEnumerator<T>;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
