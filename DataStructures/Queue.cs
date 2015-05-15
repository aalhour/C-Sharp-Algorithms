using System;
using System.Linq;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// The Queue (FIFO) Data Structure.
    /// </summary>
    public class Queue<T>
    {
        /// <summary>
        /// Instance varialbes.
        /// Collection: Doubly-Linked List.
        /// Count: Public Getter for returning the number of elements.
        /// </summary>
        private DLinkedList<T> Collection { get; set; }
        public int Count { get { return Collection.Count; } }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Queue()
        {
            // The internal collection is implemented as a doubly-linked list.
            // See the DLList.cs for the list implementation.
			Collection = new DLinkedList<T>();
        }


        /// <summary>
        /// Checks whether the queue is empty.
        /// </summary>
        /// <returns>True if queue is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            return Collection.IsEmpty();
        }


        /// <summary>
        /// Returns the top element in queue
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
                    throw new Exception("Queue is empty.");
                }
            }
        }


        /// <summary>
        /// Inserts an element at the end of the queue
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Push(T dataItem)
        {
            Collection.Append(dataItem);
        }


        /// <summary>
        /// Removes the top element in queue.
        /// </summary>
        public void Pop()
        {
            if (Count > 0)
            {
                Collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Queue is empty.");
            }
        }


        /// <summary>
        /// Removes the Top Element from queue, and assigns it's value to the "top" parameter.
        /// </summary>
        /// <param name="?"></param>
        public void Pop(out T top)
        {
            if (Count > 0)
            {
                top = Top;
                Collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Queue is empty.");
            }
        }


        /// <summary>
        /// Returns an array version of this queue.
        /// </summary>
        /// <returns>System.Array.</returns>
        public T[] ToArray()
        {
            return Collection.ToArray();
        }


        /// <summary>
        /// Returns a System.List version of this queue.
        /// </summary>
        /// <returns>System.List.</returns>
        public List<T> ToList()
        {
            return Collection.ToList();
        }


        /// <summary>
        /// Returns a readable, multi-line print-out (string) of queue.
        /// </summary>
        /// <returns>String.</returns>
        public string ToReadable()
        {
            return Collection.ToReadable();
        }

    }

}
