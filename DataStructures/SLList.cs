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
		private int count { get; set; }
		private SLListNode<T> firstNode { get; set; }
		private SLListNode<T> lastNode { get; set; }

        /// <summary>
        /// The Singly-Linked List Node class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class SLListNode<T>
        {
			public SLListNode<T> Next { get; set; }
			public T Data { get; set; }

			public SLListNode() {
				Next = null;
				Data = default(T);
			}

			public SLListNode(T dataItem) {
				Next = null;
				Data = dataItem;
			}
        }

		public SLList()
		{
			firstNode = null;
			lastNode = null;
			count = 0;
		}

        /// <summary>
        /// Getter function that returns the count of the list
        /// </summary>
        public int Count 
        { 
            get { 
                return count; 
            }
        }

		/// <summary>
		/// The Is List Empty check.
		/// </summary>
		/// <returns>true, if the list is empty, false otherwise.</returns>
		public bool IsEmpty()
		{
			return (count == 0);
		}

        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public T First 
        { 
            get {
				return (firstNode == null ? default(T) : firstNode.Data);
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public T Last
        {
            get {
                if (firstNode != null && lastNode == null) {
                    var currentNode = firstNode;
                    while (currentNode.Next != null) {
                        currentNode = currentNode.Next;
                    }
                    lastNode = currentNode;
					return currentNode.Data;
                } else {
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
			SLListNode<T> newNode = new SLListNode<T> (dataItem);

			if (firstNode == null) {
				firstNode = newNode;
				lastNode = newNode;
			} else {
				var currentNode = firstNode;
				newNode.Next = currentNode;
				firstNode = newNode;
			}

			// Increment the count.
			++count;
		}

		/// <summary>
        /// Inserts the specified dataItem at the end of the list.
        /// </summary>
        /// <param name="dataItem">The data value to be inserted to the list.</param>
        public void Append(T dataItem)
        {
			SLListNode<T> newNode = new SLListNode<T>(dataItem);

            if(firstNode == null) {
				firstNode = newNode;
				lastNode = newNode;
            } else {
                var currentNode = lastNode;
                currentNode.Next = newNode;
                lastNode = newNode;
            }

			// Increment the count.
            ++count;
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
			if (index == 0) {
				Prepend (dataItem);
			} else if (index == count) {
				Append (dataItem);
			} else if (index > 0 && index < count) {
				var currentNode = firstNode;
				SLListNode<T> newNode = new SLListNode<T> (dataItem);

				for(int i = 1; i < index; ++i) {
					currentNode = currentNode.Next;
				}

				newNode.Next = currentNode.Next;
				currentNode.Next = newNode;

				// Increment the count
				++count;
			} else {
				throw new IndexOutOfRangeException ();
			}
		}

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the list node to be removed.</param>
		public void RemoveAt(int index)
        {
			// Handle index out of bound errors
			if (count == 0 || index >= count) {
				throw new IndexOutOfRangeException ();
			}

			// Remove
			if (index == 0) {
                firstNode = firstNode.Next;

				// Decrement the count.
				--count;
            } else {
				int i = 0;
                var currentNode = firstNode;
				while (currentNode.Next != null) {
					if (i+1 == index) {
						try {
							currentNode.Next = currentNode.Next.Next;

							// Decrement the count.
							--count;
                            
							break;
						} catch (Exception ex) {
							throw ex.InnerException;
						}
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
			count = 0;
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
