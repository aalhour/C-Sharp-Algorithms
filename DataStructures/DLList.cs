using System;
using System.Linq;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Doubly-Linked List Data Structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DLList<T>
    {
        private DLListNode<T> firstNode = null;
        private DLListNode<T> lastNode = null;
        private int count = 0;

        /// <summary>
        /// The Doubly-Linked List Node class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class DLListNode<T>
        {
            public DLListNode<T> Next = null;
            public DLListNode<T> Previous = null;
            public T Data = default(T);
        }

        /// <summary>
        /// Getter function that returns the count of the list
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public DLListNode<T> First
        {
            get
            {
                return firstNode;
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public DLListNode<T> Last
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
                    return currentNode;
                }
                else
                {
                    return lastNode;
                }
            }
        }

        /// <summary>
        /// Inserts a data value into the list.
        /// </summary>
        /// <param name="dataItem">The data value to be inserted to the list</param>
        /// <returns>A Singly-Lined List Node object.</returns>
        public DLListNode<T> Append(T dataItem)
        {
            DLListNode<T> newNode = new DLListNode<T>() { Data = dataItem };

            if (firstNode == null)
            {
                firstNode = newNode;
                lastNode = newNode;
            }
            else
            {
                var currentNode = lastNode;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;
                lastNode = newNode;
            }

            count++;

            return newNode;
        }

        /// <summary>
        /// Removes a given list-node from the list.
        /// </summary>
        /// <param name="listNode">The list node to be removed.</param>
        /// <returns>True if removed successfully, false otherwise.</returns>
        public bool Remove(DLListNode<T> listNode)
        {
            bool removeStatus = false;

            // Null-value check.
            if (listNode == null || count == 0) return removeStatus;

            if (firstNode == listNode)
            {
                firstNode = firstNode.Next;
                firstNode.Previous = null;
                removeStatus = true;
                --count;
            }
            else
            {
                var currentNode = firstNode;
                while (currentNode.Next != null)
                {
                    if (currentNode.Next == listNode)
                    {
                        try
                        {
                            if (currentNode.Next.Next != null)
                            {
                                (currentNode.Next.Next).Previous = currentNode;
                            }
                            currentNode.Next = currentNode.Next.Next;

                            removeStatus = true;
                            --count;
                        }
                        catch(Exception ex)
                        {
                            throw ex.InnerException;
                        }
                        
                        break;
                    }

                    currentNode = currentNode.Next;
                }
            }

            return removeStatus;
        }

        /// <summary>
        /// Returns the list items as a readable multi--line string.
        /// </summary>
        /// <returns></returns>
        public string ToReadable()
        {
            string listAsString = string.Empty;
            int i = 0;
            var currentNode = firstNode;

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
