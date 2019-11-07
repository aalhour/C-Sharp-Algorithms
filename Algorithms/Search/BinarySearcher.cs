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

        private int _currentItemIndex;
        private int _leftIndex;
        private int _rightIndex;

        /// <summary>
        /// The value of the current item
        /// </summary>
        public T Current {

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

            this._collection = collection;
            this._comparer = comparer;
           
        }

     
        /// <summary>
        /// Apply Binary Search in a list.
        /// </summary>
        /// <param name="item">The item we search for</param>
        /// <returns>If item found, its' index, -1 otherwise</returns>
        public int BinarySearch(T item)
        {
            _currentItemIndex = -1;
            _leftIndex = 0;
            _rightIndex = _collection.Count - 1;
            HeapSorter.HeapSort(_collection);
            InternalBinarySearch(item);
            return _currentItemIndex;
        }


        /// <summary>
        /// An implementation of binary search algorithm.
        /// </summary>
        /// <param name="item">The item we search for</param>
        /// <returns></returns>
        private void InternalBinarySearch(T item)
        {
            bool found = false;
           
            while (MoveNext() && !found)
            {
                if (_comparer.Compare(item, Current) < 0)
                {
                    _rightIndex = _currentItemIndex - 1;
                }
                else if (_comparer.Compare(item, Current) > 0)
                {
                    _leftIndex = _currentItemIndex + 1;
                }
                else
                {
                    found = true;
                }
            }
            
            if (!found)
            {
                this.Reset();
            }

        }

        /// <summary>
        /// An implementation of IEnumerator's MoveNext method.
        /// </summary>
        /// <returns>true if iteration can proceed to the next item, false otherwise</returns>
        public bool MoveNext()
        {
            _currentItemIndex = this._leftIndex + (this._rightIndex - this._leftIndex) / 2;

            if (_leftIndex <= _rightIndex)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        
        public void Reset(){ this._currentItemIndex = -1; }

        
        public void Dispose(){}

    }

}