using System;
using System.Collections;
using System.Collections.Generic;
using Algorithms.Sorting;

namespace Algorithms.Search
{
    public class BinarySearcher<T> : IEnumerator<T>
    {
        private readonly IList<T> _collection;
        private readonly Comparer<T> _comparer;
        private T _item;
        private int _currentItemIndex;
        private int _leftIndex;
        private int _rightIndex;

        /// <summary>
        /// The value of the current item
        /// </summary>
        public T Current
        {
            get
            {
                return _collection[_currentItemIndex];
            }
        }

        object IEnumerator.Current => Current;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="collection">A list</param>
        /// <param name="comparer">A comparer</param>
        public BinarySearcher(IList<T> collection, Comparer<T> comparer)
        {
            if (collection == null)
            {
                throw new NullReferenceException("List is null");
            }
            _collection = collection;
            _comparer = comparer;
            HeapSorter.HeapSort(_collection);
        }

        /// <summary>
        /// Apply Binary Search in a list.
        /// </summary>
        /// <param name="item">The item we search</param>
        /// <returns>If item found, its' index, -1 otherwise</returns>
        public int BinarySearch(T item)
        {
            bool notFound = true;

            if (item == null)
            {
                throw new NullReferenceException("Item to search for is not set");
            }
            Reset();
            _item = item;

            while ((_leftIndex <= _rightIndex) && notFound)
            {
                notFound = MoveNext();
            }

            if (notFound)
            {
                Reset();
            }
            return _currentItemIndex;
        }

        /// <summary>
        /// An implementation of IEnumerator's MoveNext method.
        /// </summary>
        /// <returns>true if iteration can proceed to the next item, false otherwise</returns>
        public bool MoveNext()
        {
            _currentItemIndex = this._leftIndex + (this._rightIndex - this._leftIndex) / 2;

            if (_comparer.Compare(_item, Current) < 0)
            {
                _rightIndex = _currentItemIndex - 1;
            }
            else if (_comparer.Compare(_item, Current) > 0)
            {
                _leftIndex = _currentItemIndex + 1;
            }
            else
            {
                return false;
            }
            return true;
        }

        public void Reset()
        { 
            this._currentItemIndex = -1;
            _leftIndex = 0;
            _rightIndex = _collection.Count - 1;
        }

        public void Dispose()
        { 
           //not implementing this, since there are no managed resources to release 
        }
    }
}