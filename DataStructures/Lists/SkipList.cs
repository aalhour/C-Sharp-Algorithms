/***
 * The Skip-List data structure implementation. 
 * 
 * Uses the custom class SkipListNode<T>.
 * 
 * Note: A more memory efficient implementation would use structs, but this is the simplest implementation possible.
 */

using System;
using System.Collections.Generic;

using DataStructures.Lists;
using DataStructures.Common;

namespace DataStructures.Lists
{
    /// <summary>
    /// THE SKIP-LIST DATA STRUCTURE
    /// </summary>
    public class SkipList<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T>
    {
        private int _count { get; set; }
        private int _currentMaxLevel { get; set; }
        private Random _randomizer { get; set; }

        // The skip-list root node
        private SkipListNode<T> _firstNode { get; set; }

        // Readonly values
        private readonly int MaxLevel = 32;
        private readonly double Probability = 0.5;


        /// <summary>
        /// Private helper. Used in Add method.
        /// </summary>
        /// <returns></returns>
        private int _getNextLevel()
        {
            int lvl = 0;

            while (_randomizer.NextDouble() < Probability && lvl <= _currentMaxLevel && lvl < MaxLevel)
                ++lvl;

            return lvl;
        }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public SkipList()
        {
            _count = 0;
            _currentMaxLevel = 1;
            _randomizer = new Random();
            _firstNode = new SkipListNode<T>(default(T), MaxLevel);

            for (int i = 0; i < MaxLevel; ++i)
                _firstNode.Forwards[i] = _firstNode;
        }


        /// <summary>
        /// Getter accessor for the first node
        /// </summary>
        public SkipListNode<T> Root
        {
            get { return _firstNode; }
        }

        /// <summary>
        /// Checks if list is empty or not
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Return count of elements
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Return current max-node level
        /// </summary>
        public int Level
        {
            get { return _currentMaxLevel; }
        }

        /// <summary>
        /// Access elements by index
        /// </summary>
        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Adds item to the list
        /// </summary>
        public void Add(T item)
        {
            var current = _firstNode;
            var toBeUpdated = new SkipListNode<T>[MaxLevel];

            for (int i = _currentMaxLevel - 1; i >= 0; --i)
            {
                while (current.Forwards[i] != _firstNode && current.Forwards[i].Value.IsLessThan(item))
                    current = current.Forwards[i];

                toBeUpdated[i] = current;
            }

            current = current.Forwards[0];

            // Get the next node level, and update list level if required.
            int lvl = _getNextLevel();
            if (lvl > _currentMaxLevel)
            {
                for (int i = _currentMaxLevel; i < lvl; ++i)
                    toBeUpdated[i] = _firstNode;

                _currentMaxLevel = lvl;
            }

            // New node
            var newNode = new SkipListNode<T>(item, lvl);

            // Insert the new node into the skip list
            for (int i = 0; i < lvl; ++i)
            {
                newNode.Forwards[i] = toBeUpdated[i].Forwards[i];
                toBeUpdated[i].Forwards[i] = newNode;
            }

            // Increment the count
            ++_count;
        }

        /// <summary>
        /// Remove element from the list.
        /// </summary>
        public bool Remove(T item)
        {
            T deleted;
            return Remove(item, out deleted);
        }

        /// <summary>
        /// Remove an element from list and then return it
        /// </summary>
        public bool Remove(T item, out T deleted)
        {
            // Find the node in each of the levels
            var current = _firstNode;
            var toBeUpdated = new SkipListNode<T>[MaxLevel];

            // Walk after all the nodes that have values less than the node we are looking for.
            // Mark all nodes as toBeUpdated.
            for (int i = _currentMaxLevel - 1; i >= 0; --i)
            {
                while (current.Forwards[i] != _firstNode && current.Forwards[i].Value.IsLessThan(item))
                    current = current.Forwards[i];

                toBeUpdated[i] = current;
            }

            current = current.Forwards[0];

            // Return default value of T if the item was not found
            if (current.Value.IsEqualTo(item) == false)
            {
                deleted = default(T);
                return false;
            }

            // We know that the node is in the list.
            // Unlink it from the levels where it exists.
            for (int i = 0; i < _currentMaxLevel; ++i)
                if (toBeUpdated[i].Forwards[i] == current)
                    toBeUpdated[i].Forwards[i] = current.Forwards[i];

            // Decrement the count
            --_count;

            // Check to see if we've deleted the highest-level node
            // Decrement level
            while (_currentMaxLevel > 1 && _firstNode.Forwards[_currentMaxLevel - 1] == _firstNode)
                --_currentMaxLevel;

            // Assign the deleted output parameter to the node.Value
            deleted = current.Value;
            return true;
        }

        /// <summary>
        /// Checks if an item is in the list
        /// </summary>
        public bool Contains(T item)
        {
            T itemOut;
            return Find(item, out itemOut);
        }

        /// <summary>
        /// Look for an element and return it if found
        /// </summary>
        public bool Find(T item, out T result)
        {
            var current = _firstNode;

            // Walk after all the nodes that have values less than the node we are looking for
            for (int i = _currentMaxLevel - 1; i >= 0; --i)
                while (current.Forwards[i] != _firstNode && current.Forwards[i].Value.IsLessThan(item))
                    current = current.Forwards[i];

            current = current.Forwards[0];

            // Return true if we found the element; false otherwise
            if (current.Value.IsEqualTo(item))
            {
                result = current.Value;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Deletes the min element if the list is empty; otherwise throws exception
        /// </summary>
        public T DeleteMin()
        {
            T min;

            if (!TryDeleteMin(out min))
            {
                throw new InvalidOperationException("SkipList is empty.");
            }

            return min;
        }

        /// <summary>
        /// Tries to delete the min element, returns false if list is empty
        /// </summary>
        public bool TryDeleteMin(out T result)
        {
            if (IsEmpty)
            {
                result = default(T);
                return false;
            }

            return Remove(_firstNode.Forwards[0].Value, out result);
        }

        /// <summary>
        /// Returns the first element if the list is not empty; otherwise throw an exception
        /// </summary>
        public T Peek()
        {
            T peek;

            if (!TryPeek(out peek))
            {
                throw new InvalidOperationException("SkipList is empty.");
            }

            return peek;
        }

        /// <summary>
        /// Tries to return the first element, if the list is empty it returns false
        /// </summary>
        public bool TryPeek(out T result)
        {
            if (IsEmpty)
            {
                result = default(T);
                return false;
            }

            result = _firstNode.Forwards[0].Value;
            return true;
        }


        #region IEnumerable<T> Implementation
        /// <summary>
        /// IEnumerable method implementation
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            var node = _firstNode;
            while (node.Forwards[0] != null && node.Forwards[0] != _firstNode)
            {
                node = node.Forwards[0];
                yield return node.Value;
            }
        }

        /// <summary>
        /// IEnumerable method implementation
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable<T> Implementation


        #region ICollection<T> Implementation
        /// <summary>
        /// Checks whether this collection is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Copy this list to an array
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            // Validate the array and arrayIndex
            if (array == null)
                throw new ArgumentNullException();
            else if (array.Length == 0 || arrayIndex >= array.Length || arrayIndex < 0)
                throw new IndexOutOfRangeException();

            // Get enumerator
            var enumarator = this.GetEnumerator();

            // Copy elements as long as there is any in the list and as long as the index is within the valid range
            for (int i = arrayIndex; i < array.Length; ++i)
            {
                if (enumarator.MoveNext())
                    array[i] = enumarator.Current;
                else
                    break;
            }
        }

        /// <summary>
        /// Clears this instance
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _currentMaxLevel = 1;
            _randomizer = new Random();
            _firstNode = new SkipListNode<T>(default(T), MaxLevel);

            for (int i = 0; i < MaxLevel; ++i)
                _firstNode.Forwards[i] = _firstNode;
        }
        #endregion

    }

}
