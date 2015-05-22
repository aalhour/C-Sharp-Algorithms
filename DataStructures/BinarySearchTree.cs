using System;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
    /// <summary>
    /// Implements a generic Binary Search Tree data structure.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        //
        // The Tree Node
        public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
        {
            public T Value { get; set; }
            public BSTNode<T> Parent { get; set; }
            public BSTNode<T> Left { get; set; }
            public BSTNode<T> Right { get; set; }

            /// <summary>
            /// CONSTRUCTORS
            /// </summary>
            public BSTNode() : this(default(T), null, null, null) { }
            public BSTNode(T value) : this(value, null, null, null) { }
            public BSTNode(T value, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
            {
                this.Value = value;
                this.Parent = parent;
                this.Left = left;
                this.Right = right;
            }

            // 
            // IComparable CompareTo implementation
            public int CompareTo(BSTNode<T> other)
            {
                if (other == null)
                    return -1;

                return this.Value.CompareTo(other.Value);
            }
        }//end-of-bstnode


        /// <summary>
        /// TREE INSTANCE VARIABLES
        /// </summary>
        /// <returns></returns>
        private BSTNode<T> _root { get; set; }
        private int _count { get; set; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public BinarySearchTree()
        {
            _root = null;
            _count = 0;
        }

        /// <summary>
        /// Return the number of elements in this tree
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _count;
        }


        /// <summary>
        /// Checks if tree is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (_count == 0);
        }


        /// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="key">Key to insert</param>
        public void Insert(T key)
        {

            if (IsEmpty())
            {
                _root = new BSTNode<T>(key);
                _count++;
            }
            else
            {
                // Go left if less than or equal; go right otherwise
                if (key.CompareTo(_root.Value) < 1) // key <= root.Value
                {
                    // insert left
                }
                else
                {
                    // insert right
                }
            }
        }


        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="key">Key to remove.</param>
        public void Delete(T key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public T FindMin()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public T FindMax()
        {
            throw new NotImplementedException();
        }


        public T Find(T key)
        {
            throw new NotImplementedException();
        }


        public ArrayList<T>[] FindAll(Predicate<T> searchPredicate)
        {
            throw new NotImplementedException();
        }


        public void Traverse(Action<T> action)
        {
            throw new NotImplementedException();
        }


        public void BSTSort(out ArrayList<T> outputCollection)
        {
            throw new NotImplementedException();
        }


        public void BSTSort(out IList<T> outputCollection)
        {
            throw new NotImplementedException();
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

    }

}
