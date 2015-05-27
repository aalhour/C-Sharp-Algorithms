using System;
using System.Collections;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Trees
{
    /// <summary>
    /// The binary search tree node.
    /// </summary>

    public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
    {
        private T _value;
        private BSTNode<T> _parent;
        private BSTNode<T> _left;
        private BSTNode<T> _right;

        public BSTNode() : this(default(T), 0, null, null, null) { }
        public BSTNode(T value) : this(value, 0, null, null, null) { }
        public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
        {
            Value = value;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual T Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public virtual BSTNode<T> Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public virtual BSTNode<T> LeftChild
        {
            get { return this._left; }
            set { this._left = value; }
        }

        public virtual BSTNode<T> RightChild
        {
            get { return this._right; }
            set { this._right = value; }
        }

        /// <summary>
        /// Checks whether this node has any children.
        /// </summary>
        public virtual bool HasChildren
        {
            get { return (this.ChildrenCount > 0); }
        }

        /// <summary>
        /// Checks whether this node has left child.
        /// </summary>
        public virtual bool HasLeftChild
        {
            get { return (this.LeftChild != null); }
        }

        /// <summary>
        /// Checks whether this node has right child.
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (this.RightChild != null); }
        }

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsLeftChild
        {
            get { return (this.Parent != null && this.Parent.LeftChild == this); }
        }

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsRightChild
        {
            get { return (this.Parent != null && this.Parent.RightChild == this); }
        }

        /// <summary>
        /// Checks whether this node is a leaf node.
        /// </summary>
        public virtual bool IsLeafNode
        {
            get { return (this.ChildrenCount == 0); }
        }

        /// <summary>
        /// Returns number of direct descendents: 0, 1, 2 (none, left or right, or both).
        /// </summary>
        /// <returns>Number (0,1,2)</returns>
        public virtual int ChildrenCount
        {
            get
            {
                int count = 0;

                if (this.HasLeftChild)
                    count++;
                if (this.HasRightChild)
                    count++;

                return count;
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        public virtual int CompareTo(BSTNode<T> other)
        {
            if (other == null)
                return -1;

            return this.Value.CompareTo(other.Value);
        }
    }//end-of-bstnode


    /******************************************************************************/


    /// <summary>
    /// Implements a generic Binary Search Tree data structure.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// Specifies the mode of travelling through the tree.
        /// </summary>
        public enum TraversalMode
        {
            InOrder = 0,
            PreOrder = 1,
            PostOrder = 2
        }


        /// <summary>
        /// TREE INSTANCE VARIABLES
        /// </summary>
        /// <returns></returns>
        protected int _count { get; set; }
        private BSTNode<T> _root { get; set; }

        public virtual BSTNode<T> Root
        {
            get { return this._root; }
            set { this._root = value; }
        }

        public BinarySearchTree()
        {
            Root = null;
            _count = 0;
        }


        /// <summary>
        /// Replaces the node's value from it's parent node object with the newValue.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <param name="node">BST node.</param>
        /// <param name="newNode">New value.</param>
        protected virtual void _replaceNodeInParent(BSTNode<T> node, BSTNode<T> newNode = null)
        {
            if (node.Parent != null)
            {
                if (node.IsLeftChild)
                    node.Parent.LeftChild = newNode;
                else
                    node.Parent.RightChild = newNode;
            }

            if (newNode != null)
                newNode.Parent = node.Parent;
        }

        /// <summary>
        /// Remove the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <returns>>True if removed successfully; false if node wasn't found.</returns>
        protected virtual bool _remove(BSTNode<T> node)
        {
            if (node == null)
                return false;

            var parent = node.Parent;

            if (node.ChildrenCount == 2) // if both children are present
            {
                var successor = node.RightChild;
                node.Value = successor.Value;
                return (true && _remove(successor));
            }
            else if (node.HasLeftChild) // if the node has only a LEFT child
            {
                _replaceNodeInParent(node, node.LeftChild);
                _count--;

            }
            else if (node.HasRightChild) // if the node has only a RIGHT child
            {
                _replaceNodeInParent(node, node.RightChild);
                _count--;
            }
            else //this node has no children
            {
                _replaceNodeInParent(node, null);
                _count--;
            }

            return true;
        }

        /// <summary>
        /// Inserts a new node to the tree.
        /// </summary>
        /// <param name="currentNode">Current node to insert afters.</param>
        /// <param name="newNode">New node to be inserted.</param>
        protected virtual void _insertNode(BSTNode<T> newNode)
        {
            // Handle empty trees
            if (this.Root == null)
            {
                Root = newNode;
                _count++;
                return;
            }
            else
            {
                if (newNode.Parent == null)
                    newNode.Parent = this.Root;

                // Go Left
                if (newNode.Parent.Value.IsGreaterThan(newNode.Value)) // newNode < parent
                {
                    if (newNode.Parent.HasLeftChild == false)
                    {
                        newNode.Parent.LeftChild = newNode;
                        _count++;
                        return;
                    }
                    else
                    {
                        newNode.Parent = newNode.Parent.LeftChild;
                        _insertNode(newNode);
                    }
                }
                // Go Right
                else // new node > parent
                {
                    if (newNode.Parent.HasRightChild == false)
                    {
                        newNode.Parent.RightChild = newNode;
                        _count++;
                        return;
                    }
                    else
                    {
                        newNode.Parent = newNode.Parent.RightChild;
                        _insertNode(newNode);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the tree height from a specific node, recursively.
        /// Time-complexity: O(n), where n = number of nodes.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Height of node's longest subtree</returns>
        protected virtual int _getTreeHeight(BSTNode<T> node)
        {
            if (node == null || node.IsLeafNode == true)
                return 0;

            if (node.ChildrenCount == 2) // it has both a right child and a left child
            {
                return (1 + Math.Max(_getTreeHeight(node.LeftChild), _getTreeHeight(node.RightChild)));
            }
            else if (node.HasLeftChild)
            {
                return (1 + _getTreeHeight(node.LeftChild));
            }
            else if (node.HasRightChild)
            {
                return (1 + _getTreeHeight(node.RightChild));
            }

            // return-functions-fix
            return 0;
        }

        /// <summary>
        /// Finds a node inside another node's subtrees, given it's value.
        /// </summary>
        /// <param name="currentNode">Node to start search from.</param>
        /// <param name="item">Search value</param>
        /// <returns>Node if found; otherwise null</returns>
        protected virtual BSTNode<T> _findNode(BSTNode<T> currentNode, T item)
        {
            if (currentNode == null)
                return currentNode;

            if (item.IsEqualTo(currentNode.Value))
            {
                return currentNode;
            }
            else if (currentNode.HasLeftChild && item.IsLessThan(currentNode.Value))
            {
                return _findNode(currentNode.LeftChild, item);
            }
            else if (currentNode.HasRightChild && item.IsGreaterThan(currentNode.Value))
            {
                return _findNode(currentNode.RightChild, item);
            }

            // Return-functions-fix
            return null;
        }

        /// <summary>
        /// Returns the min-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The minimum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
        protected virtual BSTNode<T> _findMinNode(BSTNode<T> node)
        {
            if (node == null)
                return node;

            var currentNode = node;

            while (currentNode.HasLeftChild)
                currentNode = currentNode.LeftChild;

            return currentNode;
        }

        /// <summary>
        /// Returns the max-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The maximum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
        protected virtual BSTNode<T> _findMaxNode(BSTNode<T> node)
        {
            if (node == null)
                return node;

            var currentNode = node;

            while (currentNode.HasRightChild)
                currentNode = currentNode.RightChild;

            return currentNode;
        }

        /// <summary>
        /// Finds the next smaller node in value compared to the specified node.
        /// </summary>
        protected virtual BSTNode<T> _findNextSmaller(BSTNode<T> node)
        {
            if (node == null)
                return node;

            if (node.HasLeftChild)
                return _findMaxNode(node.LeftChild);

            var currentNode = node;
            while (currentNode.Parent != null && currentNode.IsLeftChild)
                currentNode = currentNode.Parent;

            return currentNode.Parent;
        }

        /// <summary>
        /// Finds the next larger node in value compared to the specified node.
        /// </summary>
        protected virtual BSTNode<T> _findNextLarger(BSTNode<T> node)
        {
            if (node == null)
                return node;

            if (node.HasRightChild)
                return _findMinNode(node.RightChild);

            var currentNode = node;
            while (currentNode.Parent != null && currentNode.IsRightChild)
                currentNode = currentNode.Parent;

            return currentNode.Parent;
        }

        /// <summary>
        /// A recursive private method. Used in the public FindAll(predicate) functions.
        /// Implements in-order traversal to find all the matching elements in a subtree.
        /// </summary>
        /// <param name="currentNode">Node to start searching from.</param>
        /// <param name="match"></param>
        protected virtual void _findAll(BSTNode<T> currentNode, Predicate<T> match, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _findAll(currentNode.LeftChild, match, ref list);

            if (match(currentNode.Value)) // match
            {
                list.Add(currentNode.Value);
            }

            // call the right child
            _findAll(currentNode.RightChild, match, ref list);
        }

        /// <summary>
        /// In-order traversal of the subtrees of a node. Returns every node it vists.
        /// </summary>
        /// <param name="currentNode">Node to traverse the tree from.</param>
        /// <param name="list">List to add elements to.</param>
        protected virtual void _inOrderTraverse(BSTNode<T> currentNode, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.LeftChild, ref list);

            // visit node
            list.Add(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.RightChild, ref list);
        }


        /// <summary>
        /// Return the number of elements in this tree
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return _count;
        }

        /// <summary>
        /// Checks if tree is empty.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEmpty()
        {
            return (_count == 0);
        }

        /// <summary>
        /// Returns the height of the tree.
        /// Time-complexity: O(n), where n = number of nodes.
        /// </summary>
        /// <returns>Hight</returns>
        public virtual int Height()
        {
            if (IsEmpty())
                return 0;

            var currentNode = Root;
            return _getTreeHeight(currentNode);
        }

        /// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="item">Item to insert</param>
        public virtual void Insert(T item)
        {
            var newNode = new BSTNode<T>(item);

            // Insert node recursively starting from the root.
            _insertNode(newNode);
        }

        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
        public virtual void Remove(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var node = _findNode(Root, item);
            bool status = _remove(node);

            // If the element was found, remove it.
            if (status == false)
                throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
        public virtual void RemoveMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var node = _findMinNode(Root);
            _remove(node);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public virtual void RemoveMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var node = _findMaxNode(Root);
            _remove(node);
        }

        /// <summary>
        /// Clears all elements from tree.
        /// </summary>
        public virtual void Clear()
        {
            Root = null;
            _count = 0;
        }

        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public virtual T FindMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            return _findMinNode(Root).Value;
        }

        /// <summary>
        /// Finds the next smaller element in tree, compared to the specified item.
        /// </summary>
        public virtual T FindNextSmaller(T item)
        {
            var node = _findNode(Root, item);
            var nextSmaller = _findNextSmaller(node);

            if (nextSmaller == null)
                throw new Exception("Item was not found.");

            return nextSmaller.Value;
        }

        /// <summary>
        /// Finds the next larger element in tree, compared to the specified item.
        /// </summary>
        public virtual T FindNextLarger(T item)
        {
            var node = _findNode(Root, item);
            var nextLarger = _findNextLarger(node);

            if (nextLarger == null)
                throw new Exception("Item was not found.");

            return nextLarger.Value;
        }

        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public virtual T FindMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            return _findMaxNode(Root).Value;
        }

        /// <summary>
        /// Find the item in the tree. Throws an exception if not found.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Item.</returns>
        public virtual T Find(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var node = _findNode(Root, item);

            if (node != null)
                return node.Value;
            else
                throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Given a predicate function, find all the elements that match it.
        /// </summary>
        /// <param name="searchPredicate">The search predicate</param>
        /// <returns>ArrayList<T> of elements.</returns>
        public virtual List<T> FindAll(Predicate<T> searchPredicate)
        {
            var list = new List<T>();
            _findAll(Root, searchPredicate, ref list);

            return list;
        }

        /// <summary>
        /// Sort the elements in this tree, using in-order traversal, and returns them.
        /// </summary>
        public virtual List<T> Sort()
        {
            var list = new List<T>();
            _inOrderTraverse(Root, ref list);

            return list;
        }

        /// <summary>
        /// Returns an array of nodes' values.
        /// </summary>
        /// <returns>The array.</returns>
        public virtual T[] ToArray()
        {
            // the array version of binary search tree is the sorted arrangement of nodes.
            return Sort().ToArray();
        }

        /// <summary>
        /// Returns a list of the nodes' value.
        /// </summary>
        public virtual List<T> ToList()
        {
            // the list version of binary search tree is the sorted arrangement of nodes.
            return Sort();
        }


        /*********************************************************************/


        /// <summary>
        /// Returns an enumerator that visits node in the order: parent, left child, right child
        /// </summary>
        public virtual IEnumerator<T> GetPreOrderEnumerator()
        {
            return new BinarySearchTreePreOrderEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that visits node in the order: left child, parent, right child
        /// </summary>
        public virtual IEnumerator<T> GetInOrderEnumerator()
        {
            return new BinarySearchTreeInOrderEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that visits node in the order: left child, right child, parent
        /// </summary>
        public virtual IEnumerator<T> GetPostOrderEnumerator()
        {
            return new BinarySearchTreePostOrderEnumerator(this);
        }


        /*********************************************************************/


        /// <summary>
        /// Returns an preorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreePreOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public BinarySearchTreePreOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTNode<T>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTNode<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    traverseQueue.Enqueue(node);
                    visitNode(node.LeftChild);
                    visitNode(node.RightChild);
                }
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset()
            {
                current = null;
            }

            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    current = traverseQueue.Dequeue();
                else
                    current = null;

                return (current != null);
            }
        }


        /// <summary>
        /// Returns an inorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreeInOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public BinarySearchTreeInOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTNode<T>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTNode<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    visitNode(node.LeftChild);
                    traverseQueue.Enqueue(node);
                    visitNode(node.RightChild);
                }
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset()
            {
                current = null;
            }

            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    current = traverseQueue.Dequeue();
                else
                    current = null;

                return (current != null);
            }
        }

        /// <summary>
        /// Returns a postorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreePostOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public BinarySearchTreePostOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTNode<T>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTNode<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    visitNode(node.LeftChild);
                    visitNode(node.RightChild);
                    traverseQueue.Enqueue(node);
                }
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset()
            {
                current = null;
            }

            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    current = traverseQueue.Dequeue();
                else
                    current = null;

                return (current != null);
            }
        }

    }//end-of-binary-search-tree

}
