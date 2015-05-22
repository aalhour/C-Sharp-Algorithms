using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// The Stack (LIFO) Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class Stack<T>
    {
        /// <summary>
        /// Instance variables.
        /// _collection: Array-Based List.
        /// Count: Public Getter for returning the number of elements.
        /// </summary>
        private ArrayList<T> _collection { get; set; }
        public int Count { get { return _collection.Count; } }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public Stack()
        {
            // The internal collection is implemented as an array-based list.
            // See the ArrayList.cs for the list implementation.
            _collection = new ArrayList<T>();
        }


        public Stack(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // The internal collection is implemented as an array-based list.
            // See the ArrayList.cs for the list implementation.
            _collection = new ArrayList<T>(initialCapacity);
        }


        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        /// <returns>True if stack is empty, false otherwise.</returns>
        public bool IsEmpty
        {
            get
            {
                return _collection.IsEmpty;
            }
        }


        /// <summary>
        /// Returns the top element in the stack.
        /// </summary>
        public T Top
        {
            get
            {
                try
                {
                    return _collection.First;
                }
                catch (Exception)
                {
                    throw new Exception("Stack is empty.");
                }
            }
        }


        /// <summary>
        /// Inserts an element at the top of the stack.
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Push(T dataItem)
        {
            _collection.InsertAt(dataItem, 0);
        }


        /// <summary>
        /// Removes the top element from stack.
        /// </summary>
        public void Pop()
        {
            if (Count > 0)
            {
                _collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Stack is empty.");
            }
        }


        /// <summary>
        /// Removes the Top Element from stack, and assigns it's value to the "top" parameter.
        /// </summary>
        /// <param name="top">Varialbe to hold the Top Element value.</param>
        public void Pop(out T top)
        {
            if (Count > 0)
            {
                top = Top;
                _collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Stack is empty.");
            }
        }


        /// <summary>
        /// Returns an array version of this stack.
        /// </summary>
        /// <returns>System.Array.</returns>
        public T[] ToArray()
        {
            return _collection.ToArray();
        }


        /// <summary>
        /// Returns a human-readable, multi-line, print-out (string) of this stack.
        /// </summary>
        /// <returns>String.</returns>
        public string ToHumanReadable()
        {
            return _collection.ToHumanReadable();
        }

    }

}
