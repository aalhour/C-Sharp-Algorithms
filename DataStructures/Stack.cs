using System;
using System.Linq;
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
        /// Collection: Doubly-Linked List.
        /// Count: Public Getter for returning the number of elements.
        /// </summary>
		private DLinkedList<T> Collection { get; set; }
        public int Count { get { return Collection.Count; } }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Stack()
        {
            // The internal collection is implemented as a doubly-linked list.
            // See the DLList.cs for the list implementation.
			Collection = new DLinkedList<T>();
        }


        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        /// <returns>True if stack is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            return Collection.IsEmpty();
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
                    return Collection.First;
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
            Collection.Prepend(dataItem);
        }


        /// <summary>
        /// Removes the top element from stack.
        /// </summary>
        public void Pop()
        {
            if (Count > 0)
            {
                Collection.RemoveAt(0);
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
                Collection.RemoveAt(0);
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
            return Collection.ToArray();
        }


        /// <summary>
        /// Returns a System.List version of this stack.
        /// </summary>
        /// <returns>System.List.</returns>
        public List<T> ToList()
        {
            return Collection.ToList();
        }


        /// <summary>
        /// Returns a readable, multi-line print-out (string) of stack.
        /// </summary>
        /// <returns>String.</returns>
        public string ToReadable()
        {
            return Collection.ToReadable();
        }

    }

}
