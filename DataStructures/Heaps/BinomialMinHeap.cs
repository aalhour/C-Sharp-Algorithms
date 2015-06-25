using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Heaps
{
    /// <summary>
    /// BINOMIAL MIN HEAP Data Structure
    /// </summary>
    public class BinomialMinHeap<T> : IMinHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// The Heap Node class.
        /// </summary>
        private class BinomialNode<T>
        {
            public T Value { get; set; }
            public int Degree { get; set; }
            public BinomialNode<T> Parent { get; set; }
            public BinomialNode<T> Sibling { get; set; }    // Right-Sibling
            public BinomialNode<T> Child { get; set; }      // Left-Child

            // Constructors
            public BinomialNode() : this(default(T), null, null, null) { }
            public BinomialNode(T value) : this(value, null, null, null) { }
            public BinomialNode(T value, BinomialNode<T> parent, BinomialNode<T> sibling, BinomialNode<T> child)
            {
                Value = value;
                Parent = parent;
                Sibling = sibling;
                Child = child;
            }

            // Degree increment and decrement
            public void IncrementDegree() { ++this.Degree; }
            public void DecrementDegree() { --this.Degree; }

            // Helper boolean flags
            public bool HasSiblings
            {
                get { return this.Sibling != null; }
            }

            public bool HasChildren
            {
                get { return this.Child != null; }
            }
        }


        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private int _size { get; set; }
        private const int _defaultCapacity = 8;
        private ArrayList<BinomialNode<T>> _forest { get; set; }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public BinomialMinHeap() : this(0) { }
        public BinomialMinHeap(int capacity)
        {
            if (capacity <= 0)
                _forest = new ArrayList<BinomialNode<T>>();
            else
                _forest = new ArrayList<BinomialNode<T>>(capacity);

            _size = 0;
        }


        /************************************************************************************************/
        /** PRIVATE HELPER FUNCTIONS                                                                    */

        /// <summary>
        /// Returns index of the tree with the minimum root's value.
        /// </summary>
        private int _findMinIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Combines two trees and returns the new tree root node.
        /// </summary>
        private BinomialNode<T> _combineTrees(BinomialNode<T> firstTreeRoot, BinomialNode<T> secondTreeRoot)
        {
            if(firstTreeRoot == null || secondTreeRoot == null)
                throw new ArgumentNullException("Either one of the nodes or both are null.");

            if (secondTreeRoot.Value.IsLessThan(firstTreeRoot.Value))
                return _combineTrees(secondTreeRoot, firstTreeRoot);

            secondTreeRoot.Sibling = firstTreeRoot.Child;
            firstTreeRoot.Child = secondTreeRoot;
            return firstTreeRoot;
        }

        /// <summary>
        /// Clones a tree, given it's root node.
        /// </summary>
        private BinomialNode<T> _cloneTree(BinomialNode<T> treeRoot)
        {
            throw new NotImplementedException();
        }


        /************************************************************************************************/
        /** PUBLIC API FUNCTIONS                                                                        */


        /// <summary>
        /// Returns count of elements in heap.
        /// </summary>
        public int Count()
        {
            return _size;
        }

        /// <summary>
        /// Checks if heap is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (_size == 0);
        }

        public void Heapify(IList<T> newCollection)
        {
            throw new NotImplementedException();
        }

        public void Insert(T heapKey)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public void RemoveMin()
        {
            throw new NotImplementedException();
        }

        public T ExtractMin()
        {
            throw new NotImplementedException();
        }

        public void Merge(BinomialMinHeap<T> otherHeap)
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public List<T> ToList()
        {
            throw new NotImplementedException();
        }

        public IMaxHeap<T> ToMaxHeap()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

    }

}
