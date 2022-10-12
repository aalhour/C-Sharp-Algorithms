using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Trees;

namespace DataStructures.SortedCollections;

/// <summary>
/// Sorted List (RBTree-based).
/// </summary>
public class SortedList<T> : IEnumerable<T>, ICollection<T>, IList<T> where T : IComparable<T>
{
    /// <summary>
    /// The internal collection is a Red-Black Tree.
    /// </summary>
    private RedBlackTree<T> _collection { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    public SortedList()
    {
        _collection = new RedBlackTree<T>();
    }

    /// <summary>
    /// Returns true if list is empty; otherwise, false.
    /// </summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Gets the count of items in list.
    /// </summary>
    public int Count => _collection.Count;

    public bool IsReadOnly => false;

    /// <summary>
    /// Determines whether the current collection contains a specific value.
    /// </summary>
    public bool Contains(T item)
    {
        return _collection.Contains(item);
    }

    /// <summary>
    /// Determines the index of a specific item in the current collection.
    /// </summary>
    public int IndexOf(T item)
    {
        // If the item doesn't exist in collection, return -1
        if (!Contains(item))
            return -1;

        int index = 0;
        var enumerator = _collection.GetInOrderEnumerator();

        while (enumerator.MoveNext())
        {
            // If the current item is found return index
            if (enumerator.Current.IsEqualTo(item))
                return index;

            // Increment index
            index++;
        }

        return -1;
    }

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    public T this[int index]
    {
        get
        {
            // In case list is empty
            if (IsEmpty)
                throw new Exception("List is empty.");
                
            // Validate index range
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            var enumerator = _collection.GetInOrderEnumerator();

            // Keep moving to the next item until index becomes 0
            while (enumerator.MoveNext() && index > 0)
                index--;

            // Return the enumerator's Current value
            return enumerator.Current;
        }
        set
        {
            try
            {
                _collection.Remove(this[index]);
                Add(value);
            }
            catch (IndexOutOfRangeException)
            {
                // Masks the get method (see above) exception with a new one.
                throw new IndexOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Adds the item to list.
    /// </summary>
    public void Add(T item)
    {
        _collection.Insert(item);
    }

    /// <summary>
    /// Removes the first occurrence of an item from list.
    /// </summary>
    public bool Remove(T item)
    {
        try 
        {
            _collection.Remove(item);
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Inserts the item at the specified index.
    /// </summary>
    public void Insert(int index, T item)
    {
        // It is meaningless to insert at a specific index since after every
        // insert operation, the collection will be rebalanced and the insertion
        // operation itself needs to ensure the sorting criteria, therefore the item
        // item insert at index i might not be the same after the operation has completed.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes an item at a specific index.
    /// </summary>
    public void RemoveAt(int index)
    {
        // Validate index range
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();

        var enumerator = _collection.GetInOrderEnumerator();

        // Keep moving to the next item until index becomes 0
        while (enumerator.MoveNext() && index > 0)
            index--;

        // Remove the enumerator's Current value from collection
        Remove(enumerator.Current);
    }

    /// <summary>
    /// Copies the items in list to an array starting from a given index.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        // Validate the array argument
        if(array == null)
            throw new ArgumentNullException("Array cannot be Null.");
            
        var enumerator = _collection.GetInOrderEnumerator();

        // Copy the items from the inorder-walker of the tree to the passed array
        while (enumerator.MoveNext() && arrayIndex < array.Length)
        {
            array[arrayIndex] = enumerator.Current;
            arrayIndex++;
        }
    }

    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
        _collection = new RedBlackTree<T>();
    }


    #region IEnumerable implementation

    public IEnumerator<T> GetEnumerator()
    {
        return _collection.GetInOrderEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}