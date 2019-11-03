using System;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Lists
{
    /// <summary>
    /// The Doubly-Linked List Node class.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedListNode<T> : IComparable<DLinkedListNode<T>> where T : IComparable<T>
    {
        private T _data;
        private DLinkedListNode<T> _next;
        private DLinkedListNode<T> _previous;

        public DLinkedListNode() : this(default(T)) { }
        public DLinkedListNode(T dataItem) : this(dataItem, null, null) { }
        public DLinkedListNode(T dataItem, DLinkedListNode<T> next, DLinkedListNode<T> previous)
        {
            Data = dataItem;
            Next = next;
            Previous = previous;
        }

        public virtual T Data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public virtual DLinkedListNode<T> Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public virtual DLinkedListNode<T> Previous
        {
            get { return this._previous; }
            set { this._previous = value; }
        }

        public int CompareTo(DLinkedListNode<T> other)
        {
            if (other == null) return -1;

            return this.Data.CompareTo(other.Data);
        }
    }


    /***********************************************************************************/


    /// <summary>
    /// Doubly-Linked List Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Instance variables.
        /// </summary>
        private int _count;
        private DLinkedListNode<T> _firstNode { get; set; }
        private DLinkedListNode<T> _lastNode { get; set; }

        public virtual DLinkedListNode<T> Head
        {
            get { return this._firstNode; }
        }

        public virtual int Count
        {
            get { return this._count; }
        }


        /// <summary>
        /// Gets the element at the specified index
        /// </summary>
        /// <param name="index">Index of element</param>
        /// <returns>Element</returns>
        protected virtual T _getElementAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                return First;
            }

            if (index == (Count - 1))
            {
                return Last;
            }

            DLinkedListNode<T> currentNode = null;

            // Decide from which reference to traverse the list, and then move the currentNode reference to the index
            // If index > half then traverse it from the end (_lastNode reference)
            // Otherwise, traverse it from the beginning (_firstNode refrence)
            if (index > (Count / 2))
            {
                currentNode = this._lastNode;
                for (int i = (Count - 1); i > index; --i)
                {
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                currentNode = this._firstNode;
                for (int i = 0; i < index; ++i)
                {
                    currentNode = currentNode.Next;
                }
            }

            return currentNode.Data;
        }

        /// <summary>
        /// Sets the value of the element at the specified index
        /// </summary>
        /// <param name="index">Index of element to update.</param>
        /// <returns>Element</returns>
        protected virtual void _setElementAt(int index, T value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                _firstNode.Data = value;
            }
            else if (index == (Count - 1))
            {
                _lastNode.Data = value;
            }
            else
            {
                DLinkedListNode<T> currentNode = null;

                // Decide from which reference to traverse the list, and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                if (index > (Count / 2))
                {
                    currentNode = this._lastNode;
                    for (int i = (Count - 1); i > index; --i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;
                    for (int i = 0; i < index; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                currentNode.Data = value;
            }
        }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public DLinkedList()
        {
            _count = 0;
            _firstNode = null;
            _lastNode = null;
        }

        /// <summary>
        /// Determines whether this List is empty.
        /// </summary>
        /// <returns><c>true</c> if this list is empty; otherwise, <c>false</c>.</returns>
        public virtual bool IsEmpty()
        {
            return (Count == 0);
        }

        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public virtual T First
        {
            get
            {
                if (IsEmpty())
                {
                    throw new Exception("Empty list.");
                }

                return _firstNode.Data;
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public virtual T Last
        {
            get
            {
                if (IsEmpty())
                {
                    throw new Exception("Empty list.");
                }

                if (_lastNode == null)
                {
                    var currentNode = _firstNode;
                    while (currentNode.Next != null)
                    {
                        currentNode = currentNode.Next;
                    }
                    _lastNode = currentNode;
                    return currentNode.Data;
                }

                return _lastNode.Data;
            }
        }

        /// <summary>
        /// Implements the collection-index operator.
        /// Gets or sets the element at the specified index
        /// </summary>
        /// <param name="index">Index of element.</param>
        public virtual T this[int index]
        {
            get { return this._getElementAt(index); }
            set { this._setElementAt(index, value); }
        }

        /// <summary>
        /// Returns the index of an item if exists.
        /// </summary>
        public virtual int IndexOf(T dataItem)
        {
            int i = 0;
            bool found = false;
            var currentNode = _firstNode;

            // Get currentNode to reference the element at the index.
            while (i < Count)
            {
                if (currentNode.Data.IsEqualTo(dataItem))
                {
                    found = true;
                    break;
                }

                currentNode = currentNode.Next;
                i++;
            }//end-while

            return (found == true ? i : -1);
        }

        /// <summary>
        /// Prepend the specified dataItem at the beginning of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public virtual void Prepend(T dataItem)
        {
            DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

            if (_firstNode == null)
            {
                _firstNode = _lastNode = newNode;
            }
            else
            {
                var currentNode = _firstNode;
                newNode.Next = currentNode;
                currentNode.Previous = newNode;
                _firstNode = newNode;
            }

            // Increment the count.
            _count++;
        }

        /// <summary>
        /// Append the specified dataItem at the end of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public virtual void Append(T dataItem)
        {
            DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

            if (_firstNode == null)
            {
                _firstNode = _lastNode = newNode;
            }
            else
            {
                var currentNode = _lastNode;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;
                _lastNode = newNode;
            }

            // Increment the count.
            _count++;
        }

        /// <summary>
        /// Inserts the dataItem at the specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public virtual void InsertAt(T dataItem, int index)
        {
            if(index < 0 || index > Count)
                throw new IndexOutOfRangeException();
            
            if (index == 0)
            {
                Prepend(dataItem);
            }
            else if (index == Count)
            {
                Append(dataItem);
            }
            else
            {
                DLinkedListNode<T> currentNode = null;
                DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

                currentNode = this._firstNode;
                for (int i = 0; i < index - 1; ++i)
                {
                    currentNode = currentNode.Next;
                }

                var oldNext = currentNode.Next;

                if (oldNext != null)
                    currentNode.Next.Previous = newNode;

                newNode.Next = oldNext;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;

                // Increment the count
                _count++;
            }
        }

        /// <summary>
        /// Inserts the dataItem after specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public virtual void InsertAfter(T dataItem, int index)
        {
            // Insert at previous index.
            InsertAt(dataItem, index - 1);
        }

        /// <summary>
        /// Remove the specified dataItem.
        /// </summary>
        public virtual void Remove(T dataItem)
        {
            // Handle index out of bound errors
            if (IsEmpty())
                throw new IndexOutOfRangeException();

            if (_firstNode.Data.IsEqualTo(dataItem))
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (_lastNode.Data.IsEqualTo(dataItem))
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                // Remove
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (currentNode.Next != null)
                {
                    if (currentNode.Data.IsEqualTo(dataItem))
                        break;

                    currentNode = currentNode.Next;
                }//end-while

                // Throw exception if item was not found
                if (!currentNode.Data.IsEqualTo(dataItem))
                    throw new Exception("Item was not found!");

                // Remove element
                DLinkedListNode<T> newPrevious = currentNode.Previous;
                DLinkedListNode<T> newNext = currentNode.Next;

                if (newPrevious != null)
                    newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }

            // Decrement count.
            _count--;
        }

        /// <summary>
        /// Remove the specified dataItem.
        /// </summary>
        public virtual void RemoveFirstMatch(Predicate<T> match)
        {
            // Handle index out of bound errors
            if (IsEmpty())
                throw new IndexOutOfRangeException();

            if (match(_firstNode.Data))
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (match(_lastNode.Data))
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                // Remove
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (currentNode.Next != null)
                {
                    if (match(currentNode.Data))
                        break;

                    currentNode = currentNode.Next;
                }//end-while

                // If we reached the last node and item was not found
                // Throw exception
                if (!match(currentNode.Data))
                    throw new Exception("Item was not found!");

                // Remove element
                DLinkedListNode<T> newPrevious = currentNode.Previous;
                DLinkedListNode<T> newNext = currentNode.Next;

                if (newPrevious != null)
                    newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }

            // Decrement count.
            _count--;
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <returns>True if removed successfully, false otherwise.</returns>
        /// <param name="index">Index of item.</param>
        public virtual void RemoveAt(int index)
        {
            // Handle index out of bound errors
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            // Remove
            if (index == 0)
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (index == Count - 1)
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                int i = 0;
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (i < index)
                {
                    currentNode = currentNode.Next;
                    i++;
                }//end-while


                // Remove element
                var newPrevious = currentNode.Previous;
                var newNext = currentNode.Next;
                newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }//end-else

            // Decrement count.
            _count--;
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public virtual void Clear()
        {
            _count = 0;
            _firstNode = _lastNode = null;
        }

        /// <summary>
        /// Chesk whether the specified element exists in the list.
        /// </summary>
        /// <param name="dataItem">Value to check for.</param>
        /// <returns>True if found; false otherwise.</returns>
        public virtual bool Contains(T dataItem)
        {
            try
            {
                return Find(dataItem).IsEqualTo(dataItem);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Find the specified item in the list.
        /// </summary>
        /// <param name="dataItem">Value to find.</param>
        /// <returns>value.</returns>
        public virtual T Find(T dataItem)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            var currentNode = _firstNode;
            while (currentNode != null)
            {
                if (currentNode.Data.IsEqualTo(dataItem))
                    return dataItem;

                currentNode = currentNode.Next;
            }

            throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Tries to find a match for the predicate. Returns true if found; otherwise false.
        /// </summary>
        public virtual bool TryFindFirst(Predicate<T> match, out T found)
        {
            // Initialize the output parameter
            found = default(T);

            if (IsEmpty())
                return false;

            var currentNode = _firstNode;

            try
            {
                while (currentNode != null)
                {
                    if (match(currentNode.Data))
                    {
                        found = currentNode.Data;
                        return true;
                    }

                    currentNode = currentNode.Next;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find the first element that matches the predicate from all elements in list.
        /// </summary>
        public virtual T FindFirst(Predicate<T> match)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            var currentNode = _firstNode;

            while (currentNode != null)
            {
                if (match(currentNode.Data))
                    return currentNode.Data;

                currentNode = currentNode.Next;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Find all elements in list that match the predicate.
        /// </summary>
        /// <param name="match">Predicate function.</param>
        /// <returns>List of elements.</returns>
        public virtual List<T> FindAll(Predicate<T> match)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            var currentNode = _firstNode;
            var list = new List<T>();

            while (currentNode != null)
            {
                if (match(currentNode.Data))
                    list.Add(currentNode.Data);

                currentNode = currentNode.Next;
            }

            return list;
        }

        /// <summary>
        /// Returns a number of elements as specified by countOfElements, starting from the specified index.
        /// </summary>
        /// <param name="index">Starting index.</param>
        /// <param name="countOfElements">The number of elements to return.</param>
        /// <returns>Doubly-Linked List of elements</returns>
        public virtual DLinkedList<T> GetRange(int index, int countOfElements)
        {
            DLinkedListNode<T> currentNode = null;
            DLinkedList<T> newList = new DLinkedList<T>();

            // Handle Index out of Bound errors
            if (Count == 0)
            {
                return newList;
            }

            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException();
            }

            // Decide from which reference to traverse the list, and then move the currentNode reference to the index
            // If index > half then traverse it from the end (_lastNode reference)
            // Otherwise, traverse it from the beginning (_firstNode refrence)
            if (index > (Count / 2))
            {
                currentNode = this._lastNode;
                for (int i = (Count - 1); i > index; --i)
                {
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                currentNode = this._firstNode;
                for (int i = 0; i < index; ++i)
                {
                    currentNode = currentNode.Next;
                }
            }

            // Append the elements to the new list using the currentNode reference
            while (currentNode != null && newList.Count <= countOfElements)
            {
                newList.Append(currentNode.Data);
                currentNode = currentNode.Next;
            }

            return newList;
        }

        /// <summary>
        /// Sorts the entire list using Selection Sort.
        /// </summary>
        public virtual void SelectionSort()
        {
            if (IsEmpty())
                return;

            var currentNode = _firstNode;
            while (currentNode != null)
            {
                var minNode = currentNode;
                var nextNode = currentNode.Next;
                while (nextNode != null)
                {
                    if (nextNode.Data.IsLessThan(minNode.Data))
                    {
                        minNode = nextNode;
                    }

                    nextNode = nextNode.Next;
                }

                if (minNode != currentNode)
                {
                    var temp = minNode.Data;
                    minNode.Data = currentNode.Data;
                    currentNode.Data = temp;
                }

                currentNode = currentNode.Next;
            }
        }

        /// <summary>
        /// Return an array version of this list.
        /// </summary>
        /// <returns></returns>
        public virtual T[] ToArray()
        {
            T[] array = new T[Count];

            var currentNode = _firstNode;
            for (int i = 0; i < Count; ++i)
            {
                if (currentNode != null)
                {
                    array[i] = currentNode.Data;
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            return array;
        }

        /// <summary>
        /// Returns a System.List version of this DLList instace.
        /// </summary>
        /// <returns>System.List of elements</returns>
        public virtual List<T> ToList()
        {
            List<T> list = new List<T>(Count);

            var currentNode = _firstNode;
            for (int i = 0; i < Count; ++i)
            {
                if (currentNode != null)
                {
                    list.Add(currentNode.Data);
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            return list;
        }

        /// <summary>
        /// Returns the list items as a readable multi--line string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToReadable()
        {
            string listAsString = string.Empty;
            int i = 0;
            var currentNode = _firstNode;

            while (currentNode != null)
            {
                listAsString = String.Format("{0}[{1}] => {2}\r\n", listAsString, i, currentNode.Data);
                currentNode = currentNode.Next;
                ++i;
            }

            return listAsString;
        }

        /********************************************************************************/

        public IEnumerator<T> GetEnumerator()
        {
            var node = _firstNode;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }

            // Alternative: IEnumerator class instance
            // return new DLinkedListEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();

            // Alternative: IEnumerator class instance
            // return new DLinkedListEnumerator(this);
        }

        /********************************************************************************/

        internal class DLinkedListEnumerator : IEnumerator<T>
        {
            private DLinkedListNode<T> _current;
            private DLinkedList<T> _doublyLinkedList;

            public DLinkedListEnumerator(DLinkedList<T> list)
            {
                this._current = list.Head;
                this._doublyLinkedList = list;
            }

            public T Current
            {
                get { return this._current.Data; }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_current.Next != null)
                    _current = _current.Next;
                else
                    return false;

                return true;
            }

            public bool MovePrevious()
            {
                if (_current.Previous != null)
                    _current = _current.Previous;
                else
                    return false;

                return true;
            }

            public void Reset()
            {
                _current = _doublyLinkedList.Head;
            }

            public void Dispose()
            {
                _current = null;
                _doublyLinkedList = null;
            }
        }

    }

}
