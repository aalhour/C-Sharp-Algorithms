using System;
using System.Linq;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Singly Linked List Data Structure
    /// </summary>
    public class SLList<T>
    {
        /// <summary>
        /// The Singly-Linked List Node class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class SLListNode<T>
        {
            public SLListNode<T> Next { get; set; }
            public T Data { get; set; }

            public SLListNode()
            {
                Next = null;
                Data = default(T);
            }

            public SLListNode(T dataItem)
            {
                Next = null;
                Data = dataItem;
            }
        }


        /// <summary>
        /// Instance variables
        /// </summary>
        private SLListNode<T> firstNode { get; set; }
        private SLListNode<T> lastNode { get; set; }
        public int Count { private set; get; }


        /// <summary>
        /// A function that is used to update the lastNode reference.
        /// </summary>
        private void UpdateLastNode()
        {
            var currentNode = firstNode;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            lastNode = currentNode;
        }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public SLList()
        {
            firstNode = null;
            lastNode = null;
            Count = 0;
        }


        /// <summary>
        /// The Is List Empty check.
        /// </summary>
        /// <returns>true, if the list is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            return (Count == 0);
        }


        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public T First
        {
            get
            {
                return (firstNode == null ? default(T) : firstNode.Data);
            }
        }


        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public T Last
        {
            get
            {
                if (firstNode != null && lastNode == null)
                {
                    var currentNode = firstNode;
                    while (currentNode.Next != null)
                    {
                        currentNode = currentNode.Next;
                    }
                    lastNode = currentNode;
                    return currentNode.Data;
                }
                else
                {
                    return default(T);
                }
            }
        }


        /// <summary>
        /// Inserts the specified dataItem at the beginning of the list.
        /// </summary>
        /// <param name="dataItem">The data value to be inserted to the list.</param>
        public void Prepend(T dataItem)
        {
            SLListNode<T> newNode = new SLListNode<T>(dataItem);

            if (firstNode == null)
            {
                firstNode = lastNode = newNode;
            }
            else
            {
                var currentNode = firstNode;
                newNode.Next = currentNode;
                firstNode = newNode;
            }

            // Increment the count.
            ++Count;
        }


        /// <summary>
        /// Inserts the specified dataItem at the end of the list.
        /// </summary>
        /// <param name="dataItem">The data value to be inserted to the list.</param>
        public void Append(T dataItem)
        {
            SLListNode<T> newNode = new SLListNode<T>(dataItem);

            if (firstNode == null)
            {
                firstNode = lastNode = newNode;
            }
            else
            {
                if (lastNode == null)
                {
                    UpdateLastNode();
                }

                var currentNode = lastNode;
                currentNode.Next = newNode;
                lastNode = newNode;
            }

            // Increment the count.
            ++Count;
        }


        /// <summary>
        /// Inserts a specified item dataItem at an index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public void InsertAt(T dataItem, int index)
        {
            // Handle scope of insertion.
            // Prepend? Append? Or Insert in the range?
            if (index == 0)
            {
                Prepend(dataItem);
            }
            else if (index == Count)
            {
                Append(dataItem);
            }
            else if (index > 0 && index < Count)
            {
                var currentNode = firstNode;
                SLListNode<T> newNode = new SLListNode<T>(dataItem);

                for (int i = 1; i < index; ++i)
                {
                    currentNode = currentNode.Next;
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;

                // Increment the count
                ++Count;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the list node to be removed.</param>
        public void RemoveAt(int index)
        {
            // Handle index out of bound errors
            if (Count == 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            // Remove
            if (index == 0)
            {
                firstNode = firstNode.Next;

                // Decrement the count.
                --Count;
            }
            else
            {
                int i = 0;
                var currentNode = firstNode;
                while (currentNode.Next != null)
                {
                    if (i + 1 == index)
                    {
                        currentNode.Next = currentNode.Next.Next;

                        // Decrement the count.
                        --Count;

                        if (index == (Count - 1))
                        {
                            lastNode = null;
                        }

                        break;
                    }

                    ++i;
                    currentNode = currentNode.Next;
                }
            }
        }


        /// <summary>
        /// Clears all the items in the list.
        /// </summary>
        public void Clear()
        {
            firstNode = null;
            lastNode = null;
            Count = 0;
        }


        /// <summary>
        /// Returns the list items as a readable multi--line string.
        /// </summary>
        /// <returns></returns>
        public string ToReadable()
        {
            int i = 0;
            var currentNode = firstNode;
            string listAsString = string.Empty;

            while (currentNode != null)
            {
                listAsString = String.Format("{0}[{1}] => {2}\r\n", listAsString, i, currentNode.Data);
                currentNode = currentNode.Next;
                ++i;
            }

            return listAsString;
        }

    }

}
