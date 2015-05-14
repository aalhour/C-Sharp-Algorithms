using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrcutres
{
    /// <summary>
    /// Singly Linked List Data Structure
    /// </summary>
    public class SLList<T>
    {
        private SLListNode<T> firstNode = null;
        private SLListNode<T> lastNode = null;
        private int count = 0;

        /// <summary>
        /// The Singly-Linked List Node class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class SLListNode<T>
        {
            public SLListNode<T> Next = null;
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
        public SLListNode<T> First 
        { 
            get 
            {
                return firstNode;
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public SLListNode<T> Last
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
        public SLListNode<T> Append(T dataItem)
        {
            SLListNode<T> newNode = new SLListNode<T>() { Data = dataItem };

            if(firstNode == null)
            {
                firstNode = newNode;
                lastNode = newNode;
            }
            else
            {
                var currentNode = lastNode;
                currentNode.Next = newNode;
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
        public bool Remove(SLListNode<T> listNode)
        {
            bool removeStatus = false;

            // Null-value check.
            if (listNode == null || count == 0) return removeStatus;

            if (firstNode == listNode)
            {
                firstNode = firstNode.Next;
                --count;
                removeStatus = true;
            }
            else
            {
                var currentNode = firstNode;
                while (currentNode.Next != null)
                {
                    if(currentNode.Next == listNode)
                    {
                        currentNode.Next = currentNode.Next.Next;
                        --count;
                        removeStatus = true;
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
