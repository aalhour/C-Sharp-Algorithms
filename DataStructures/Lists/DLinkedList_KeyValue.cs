using System;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Lists
{
    /// <summary>
    /// The Doubly-Linked List Node class.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedListNode<TKey, TValue> : IComparable<DLinkedListNode<TKey, TValue>> where TKey : IComparable<TKey>
    {
        private TKey _key;
        private TValue _value;
        private DLinkedListNode<TKey, TValue> _next;
        private DLinkedListNode<TKey, TValue> _previous;

        public DLinkedListNode() : this(default(TKey), default(TValue)) { }
        public DLinkedListNode(TKey key, TValue value) : this(key, value, null, null) { }
        public DLinkedListNode(TKey key, TValue value, DLinkedListNode<TKey, TValue> next, DLinkedListNode<TKey, TValue> previous)
        {
            Key = key;
            Value = value;
            Next = next;
            Previous = previous;
        }

        public virtual TKey Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        public virtual TValue Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public virtual DLinkedListNode<TKey, TValue> Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public virtual DLinkedListNode<TKey, TValue> Previous
        {
            get { return this._previous; }
            set { this._previous = value; }
        }

        public int CompareTo(DLinkedListNode<TKey, TValue> other)
        {
            if (other == null) return -1;

            return this.Key.CompareTo(other.Key);
        }
    }


    /// <summary>
    /// Doubly-Linked List Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedList<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Instance variables.
        /// </summary>
        private int _count;
        private DLinkedListNode<TKey, TValue> _firstNode { get; set; }
        private DLinkedListNode<TKey, TValue> _lastNode { get; set; }

        public virtual DLinkedListNode<TKey, TValue> Head
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
        protected virtual DLinkedListNode<TKey, TValue> _getNodeByIndex(int index)
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                return _firstNode;
            }

            if (index == (Count - 1))
            {
                return _lastNode;
            }

            if (index > 0 && index < (Count - 1))
            {
                DLinkedListNode<TKey, TValue> currentNode = null;

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

                return currentNode;
            }

            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Gets the element by the specified key
        /// </summary>
        protected virtual DLinkedListNode<TKey, TValue> _getNodeByKey(TKey key)
        {
            if (key.IsEqualTo(_firstNode.Key))
            {
                return _firstNode;
            }

            if (key.IsEqualTo(_lastNode.Key))
            {
                return _lastNode;
            }

            var currentNode = this._firstNode;
            while (currentNode != null)
            {
                if (key.IsEqualTo(currentNode.Key))
                    break;

                currentNode = currentNode.Next;
            }

            if (currentNode == null)
                throw new KeyNotFoundException();

            return currentNode;
        }

        /// <summary>
        /// Sets the node's value by index.
        /// </summary>
        protected virtual void _setValueByIndex(int index, TValue value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                _firstNode.Value = value;
            }
            else if (index == (Count - 1))
            {
                _lastNode.Value = value;
            }
            else if (index > 0 && index < (Count - 1))
            {
                DLinkedListNode<TKey, TValue> currentNode = null;

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

                currentNode.Value = value;
            }
        }

        /// <summary>
        /// Sets the node's value by key.
        /// </summary>
        protected virtual void _setValueByKey(TKey key, TValue value)
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException("List is empty.");

            if (key.IsEqualTo(_firstNode.Key))
            {
                _firstNode.Value = value;
            }
            else if (key.IsEqualTo(_lastNode.Key))
            {
                _lastNode.Value = value;
            }
            else
            {
                var currentNode = this._firstNode;
                while (currentNode != null)
                {
                    if (currentNode.Key.IsEqualTo(key))
                        break;

                    currentNode = currentNode.Next;
                }

                if (currentNode == null)
                    throw new KeyNotFoundException();

                currentNode.Value = value;
            }
        }

        /// <summary>
        /// Sets the node object by index.
        /// </summary>
        protected virtual void _setNodeByIndex(int index, TKey key, TValue value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                _firstNode.Key = key;
                _firstNode.Value = value;
            }
            else if (index == (Count - 1))
            {
                _lastNode.Key = key;
                _lastNode.Value = value;
            }
            else if (index > 0 && index < (Count - 1))
            {
                DLinkedListNode<TKey, TValue> currentNode = null;

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

                currentNode.Key = key;
                currentNode.Value = value;
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
        public virtual KeyValuePair<TKey, TValue> First
        {
            get
            {
                if (IsEmpty())
                {
                    throw new Exception("Empty list.");
                }

                return new KeyValuePair<TKey, TValue>(_firstNode.Key, _firstNode.Value);
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public virtual KeyValuePair<TKey, TValue> Last
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
                }

                return new KeyValuePair<TKey, TValue>(_lastNode.Key, _lastNode.Value);
            }
        }

        /// <summary>
        /// Returns a list of the keys.
        /// </summary>
        public virtual List<TKey> Keys
        {
            get
            {
                List<TKey> list = new List<TKey>(Count);

                var currentNode = _firstNode;
                for (int i = 0; i < Count; ++i)
                {
                    if (currentNode != null)
                    {
                        list.Add(currentNode.Key);
                        currentNode = currentNode.Next;
                    }
                    else
                    {
                        break;
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// Returns a list of the values.
        /// </summary>
        public virtual List<TValue> Values
        {
            get
            {
                List<TValue> list = new List<TValue>(Count);

                var currentNode = _firstNode;
                for (int i = 0; i < Count; ++i)
                {
                    if (currentNode != null)
                    {
                        list.Add(currentNode.Value);
                        currentNode = currentNode.Next;
                    }
                    else
                    {
                        break;
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// Prepend the key-value at the beginning of the list.
        /// </summary>
        public virtual void Prepend(TKey key, TValue value)
        {
            var newNode = new DLinkedListNode<TKey, TValue>(key, value);

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
        /// Append the key-value item at the end of the list.
        /// </summary>
        public virtual void Append(TKey key, TValue value)
        {
            var newNode = new DLinkedListNode<TKey, TValue>(key, value);

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
        /// Inserts the a new key-value item at the specified index.
        /// </summary>
        public virtual void InsertAt(int index, TKey key, TValue value)
        {
            if (index == 0)
            {
                Prepend(key, value);
            }
            else if (index == Count)
            {
                Append(key, value);
            }
            else if (index > 0 && index < Count)
            {
                DLinkedListNode<TKey, TValue> currentNode = null;
                var newNode = new DLinkedListNode<TKey, TValue>(key, value);

                // Decide from which reference to traverse the list, and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                if (index > (Count / 2))
                {
                    currentNode = _lastNode;
                    for (int i = (Count - 1); i > index - 1; --i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;
                    for (int i = 0; i < index - 1; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;

                // Increment the count
                _count++;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Inserts the key-value after specified index.
        /// </summary>
        public virtual void InsertAfter(int index, TKey key, TValue value)
        {
            // Insert at previous index.
            InsertAt(index - 1, key, value);
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
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

                // Decrement count.
                _count--;
            }
            else if (index == Count - 1)
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;

                // Decrement count.
                _count--;
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

                // Decrement count.
                _count--;
            }//end-else
        }

        /// <summary>
        /// Removes the item with the specified key.
        /// </summary>
        public virtual void RemoveBy(TKey key)
        {
            // Remove
            if (key.IsEqualTo(_firstNode.Key))
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;

                // Decrement count.
                _count--;
            }
            else if (key.IsEqualTo(_lastNode.Key))
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;

                // Decrement count.
                _count--;
            }
            else
            {
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (currentNode != null)
                {
                    if (currentNode.Key.IsEqualTo(key))
                        break;

                    currentNode = currentNode.Next;
                }//end-while

                if (currentNode != null)
                {
                    // Remove element
                    var newPrevious = currentNode.Previous;
                    var newNext = currentNode.Next;
                    newPrevious.Next = newNext;

                    if (newNext != null)
                        newNext.Previous = newPrevious;

                    currentNode = newPrevious;

                    // Decrement count.
                    _count--;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }//end-else
        }

        /// <summary>
        /// Updates the value of an element at the specified index.
        /// </summary>
        public virtual void UpdateValueByIndex(int index, TValue value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            _setValueByIndex(index, value);
        }

        /// <summary>
        /// Updates the value of an element by it's key.
        /// </summary>
        public virtual void UpdateValueByKey(TKey key, TValue value)
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException();

            _setValueByKey(key, value);
        }

        /// <summary>
        /// Updates the key and value of an element at the specified index.
        /// </summary>
        public virtual void UpdateAtIndex(int index, TKey key, TValue value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            _setNodeByIndex(index, key, value);
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
        /// Chesk whether the specified key exists in the list.
        /// </summary>
        public virtual bool ContainsKey(TKey key)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            try
            {
                return Find(key).Key.IsEqualTo(key);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Find the specified item in the list.
        /// </summary>
        public virtual KeyValuePair<TKey, TValue> Find(TKey key)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            var currentNode = _firstNode;
            while (currentNode != null)
            {
                if (currentNode.Key.IsEqualTo(key))
                    break;

                currentNode = currentNode.Next;
            }

            if (currentNode != null)
                return new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
            throw new KeyNotFoundException("Item was not found.");
        }

        /// <summary>
        /// Find all elements in list that match the predicate.
        /// </summary>
        /// <param name="match">Predicate function.</param>
        /// <returns>List of elements.</returns>
        public virtual List<KeyValuePair<TKey, TValue>> FindAll(Predicate<TKey> match)
        {
            if (IsEmpty())
                throw new Exception("List is empty.");

            var currentNode = _firstNode;
            var list = new List<KeyValuePair<TKey, TValue>>();

            while (currentNode != null)
            {
                if (match(currentNode.Key))
                    list.Add(new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value));

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
        public virtual List<KeyValuePair<TKey, TValue>> GetRange(int index, int countOfElements)
        {
            DLinkedListNode<TKey, TValue> currentNode = null;
            List<KeyValuePair<TKey, TValue>> newList = new List<KeyValuePair<TKey, TValue>>();

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
                var keyValue = new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
                newList.Add(keyValue);
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
                var nextNode = currentNode.Next;
                while (nextNode != null)
                {
                    if (nextNode.Key.IsLessThan(currentNode.Key))
                    {
                        var temp = nextNode.Key;
                        nextNode.Key = currentNode.Key;
                        currentNode.Key = temp;
                    }

                    nextNode = nextNode.Next;
                }

                currentNode = currentNode.Next;
            }
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
                listAsString = String.Format("{0}[{1}] => {2}\r\n", listAsString, i, currentNode.Key);
                currentNode = currentNode.Next;
                ++i;
            }

            return listAsString;
        }

    }

}
