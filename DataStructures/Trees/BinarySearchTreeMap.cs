using System;
using System.Collections;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Trees
{
    /// <summary>
    /// Implements a generic Binary Search Tree Map data structure.
    /// </summary>
    public class BinarySearchTreeMap<TKey, TValue> : IBinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
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
        protected int _count { get; set; }
        protected bool _allowDuplicates { get; set; }
        protected virtual BSTMapNode<TKey, TValue> _root { get; set; }

        public virtual BSTMapNode<TKey, TValue> Root
        {
            get { return this._root; }
            internal set { this._root = value; }
        }


        /// <summary>
        /// CONSTRUCTOR.
        /// Allows duplicates by default.
        /// </summary>
        public BinarySearchTreeMap()
        {
            _count = 0;
            _allowDuplicates = true;
            Root = null;
        }

        /// <summary>
        /// CONSTRUCTOR.
        /// If allowDuplictes is set to false, no duplicate items will be inserted.
        /// </summary>
        public BinarySearchTreeMap(bool allowDuplicates)
        {
            _count = 0;
            _allowDuplicates = allowDuplicates;
            Root = null;
        }


        /// <summary>
        /// Calculates the tree height from a specific node, recursively.
        /// Time-complexity: O(n), where n = number of nodes.
        /// </summary>
        protected virtual int _getTreeHeight(BSTMapNode<TKey, TValue> node)
        {
            if (node == null)
                return 0;
            // Is leaf node
            if (node.IsLeafNode)
                return 1;
            // Has two children
            if (node.ChildrenCount == 2)
                return (1 + Math.Max(_getTreeHeight(node.LeftChild), _getTreeHeight(node.RightChild)));
            // Has only left
            if (node.HasLeftChild)
                return (1 + _getTreeHeight(node.LeftChild));
            // Has only right
            return (1 + _getTreeHeight(node.RightChild));
        }

        /// <summary>
        /// Inserts a new node to the tree.
        /// </summary>
        protected virtual bool _insertNode(BSTMapNode<TKey, TValue> newNode)
        {
            // Handle empty trees
            if (this.Root == null)
            {
                Root = newNode;
                _count++;
                return true;
            }

            if (newNode.Parent == null)
                newNode.Parent = this.Root;

            // Check for value equality and whether inserting duplicates is allowed
            if (_allowDuplicates == false && newNode.Parent.Key.IsEqualTo(newNode.Key))
            {
                return false;
            }

            // Go Left
            if (newNode.Parent.Key.IsGreaterThan(newNode.Key)) // newNode < parent
            {
                if (newNode.Parent.HasLeftChild == false)
                {
                    newNode.Parent.LeftChild = newNode;

                    // Increment count.
                    _count++;

                    return true;
                }

                newNode.Parent = newNode.Parent.LeftChild;
                return _insertNode(newNode);
            }
            // Go Right

            if (newNode.Parent.HasRightChild == false)
            {
                newNode.Parent.RightChild = newNode;

                // Increment count.
                _count++;

                return true;
            }

            newNode.Parent = newNode.Parent.RightChild;
            return _insertNode(newNode);
        }

        /// <summary>
        /// Replaces the node's value from it's parent node object with the newValue.
        /// Used in the recusive _remove function.
        /// </summary>
        protected virtual void _replaceNodeInParent(BSTMapNode<TKey, TValue> node, BSTMapNode<TKey, TValue> newNode = null)
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
        protected virtual bool _remove(BSTMapNode<TKey, TValue> node)
        {
            if (node == null)
                return false;

            var parent = node.Parent;

            if (node.ChildrenCount == 2) // if both children are present
            {
                var successor = node.RightChild;
                node.Key = successor.Key;
                node.Value = successor.Value;
                return (true && _remove(successor));
            }

            if (node.HasLeftChild) // if the node has only a LEFT child
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
        /// Finds a node inside another node's subtrees, given it's value.
        /// </summary>
        protected virtual BSTMapNode<TKey, TValue> _findNode(BSTMapNode<TKey, TValue> currentNode, TKey key)
        {
            if (currentNode == null)
                return currentNode;

            if (key.IsEqualTo(currentNode.Key))
            {
                return currentNode;
            }

            if (currentNode.HasLeftChild && key.IsLessThan(currentNode.Key))
            {
                return _findNode(currentNode.LeftChild, key);
            }

            if (currentNode.HasRightChild && key.IsGreaterThan(currentNode.Key))
            {
                return _findNode(currentNode.RightChild, key);
            }

            // Return-functions-fix
            return null;
        }

        /// <summary>
        /// Returns the min-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        protected virtual BSTMapNode<TKey, TValue> _findMinNode(BSTMapNode<TKey, TValue> node)
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
        protected virtual BSTMapNode<TKey, TValue> _findMaxNode(BSTMapNode<TKey, TValue> node)
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
        protected virtual BSTMapNode<TKey, TValue> _findNextSmaller(BSTMapNode<TKey, TValue> node)
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
        protected virtual BSTMapNode<TKey, TValue> _findNextLarger(BSTMapNode<TKey, TValue> node)
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
        protected virtual void _findAll(BSTMapNode<TKey, TValue> currentNode, Predicate<TKey> match, ref List<KeyValuePair<TKey, TValue>> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _findAll(currentNode.LeftChild, match, ref list);

            if (match(currentNode.Key)) // match
                list.Add(new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value));

            // call the right child
            _findAll(currentNode.RightChild, match, ref list);
        }

        /// <summary>
        /// In-order traversal of the subtrees of a node. Returns every node it vists.
        /// </summary>
        protected virtual void _inOrderTraverse(BSTMapNode<TKey, TValue> currentNode, ref List<KeyValuePair<TKey, TValue>> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.LeftChild, ref list);

            // visit node
            list.Add(new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value));

            // call the right child
            _inOrderTraverse(currentNode.RightChild, ref list);
        }


        /// <summary>
        /// Return the number of elements in this tree
        /// </summary>
        public virtual int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Checks if tree is empty.
        /// </summary>
        public virtual bool IsEmpty
        {
            get { return (_count == 0); }
        }

        /// <summary>
        /// Returns the height of the tree.
        /// Time-complexity: O(n), where n = number of nodes.
        /// </summary>
        public virtual int Height
        {
            get
            {
                if (IsEmpty)
                    return 0;

                var currentNode = Root;
                return _getTreeHeight(currentNode);
            }
        }

        public virtual bool AllowsDuplicates
        {
            get { return _allowDuplicates; }
        }

        /// <summary>
        /// Inserts a key-value pair to the tree
        /// </summary>
        public virtual void Insert(TKey key, TValue value)
        {
            var newNode = new BSTMapNode<TKey, TValue>(key, value);

            // Insert node recursively starting from the root. check for success status.
            var success = _insertNode(newNode);

            if (success == false && _allowDuplicates == false)
                throw new InvalidOperationException("Tree does not allow inserting duplicate elements.");
        }

        /// <summary>
        /// Inserts a key-value pair to the tree
        /// </summary>
        public virtual void Insert(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Insert(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary>
        /// Inserts an array of elements to the tree.
        /// </summary>
        public virtual void Insert(TKey[] collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Length > 0)
            {
                for (int i = 0; i < collection.Length; ++i)
                {
                    this.Insert(collection[i], default(TValue));
                }
            }
        }

        /// <summary>
        /// Inserts an array of key-value pairs to the tree.
        /// </summary>
        public virtual void Insert(KeyValuePair<TKey, TValue>[] collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Length > 0)
            {
                for (int i = 0; i < collection.Length; ++i)
                {
                    this.Insert(collection[i].Key, collection[i].Value);
                }
            }
        }

        /// <summary>
        /// Inserts a list of elements to the tree.
        /// </summary>
        public virtual void Insert(List<TKey> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Count > 0)
            {
                for (int i = 0; i < collection.Count; ++i)
                {
                    this.Insert(collection[i], default(TValue));
                }
            }
        }

        /// <summary>
        /// Inserts a list of elements to the tree.
        /// </summary>
        public virtual void Insert(List<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Count > 0)
            {
                for (int i = 0; i < collection.Count; ++i)
                {
                    this.Insert(collection[i].Key, collection[i].Value);
                }
            }
        }

        /// <summary>
        /// Updates the node of a specific key with a new value.
        /// </summary>
        public virtual void Update(TKey key, TValue newValue)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = _findNode(Root, key);

            if (node == null)
                throw new KeyNotFoundException("Key doesn't exist in tree.");

            node.Value = newValue;
        }

        /// <summary>
        /// Deletes an element from the tree with a specified key.
        /// </summary>
        public virtual void Remove(TKey key)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = _findNode(Root, key);

            if (node == null)
                throw new KeyNotFoundException("Key doesn't exist in tree.");
            
            _remove(node);
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
        public virtual void RemoveMin()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = _findMinNode(Root);
            _remove(node);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public virtual void RemoveMax()
        {
            if (IsEmpty)
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
        /// Checks for the existence of an item
        /// </summary>
        public virtual bool Contains(TKey key)
        {
            return (_findNode(_root, key) != null);
        }

        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public virtual KeyValuePair<TKey, TValue> FindMin()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var minNode =_findMinNode(Root);
            return new KeyValuePair<TKey, TValue>(minNode.Key, minNode.Value);
        }

        /// <summary>
        /// Finds the next smaller element in tree, compared to the specified item.
        /// </summary>
        public virtual KeyValuePair<TKey, TValue> FindNextSmaller(TKey key)
        {
            var node = _findNode(Root, key);
            var nextSmaller = _findNextSmaller(node);

            if (nextSmaller == null)
                throw new Exception("Item was not found.");

            return new KeyValuePair<TKey, TValue>(nextSmaller.Key, nextSmaller.Value);
        }

        /// <summary>
        /// Finds the next larger element in tree, compared to the specified item.
        /// </summary>
        public virtual KeyValuePair<TKey, TValue> FindNextLarger(TKey item)
        {
            var node = _findNode(Root, item);
            var nextLarger = _findNextLarger(node);

            if (nextLarger == null)
                throw new Exception("Item was not found.");

            return new KeyValuePair<TKey, TValue>(nextLarger.Key, nextLarger.Value);
        }

        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public virtual KeyValuePair<TKey, TValue> FindMax()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var maxNode = _findMaxNode(Root);
            return new KeyValuePair<TKey, TValue>(maxNode.Key, maxNode.Value);
        }

        /// <summary>
        /// Find the item in the tree. Throws an exception if not found.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Item.</returns>
        public virtual KeyValuePair<TKey, TValue> Find(TKey key)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = _findNode(Root, key);

            if (node != null)
                return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            throw new KeyNotFoundException("Item was not found.");
        }

        /// <summary>
        /// Given a predicate function, find all the elements that match it.
        /// </summary>
        /// <param name="searchPredicate">The search predicate</param>
        /// <returns>ArrayList<T> of elements.</returns>
        public virtual IEnumerable<KeyValuePair<TKey, TValue>> FindAll(Predicate<TKey> searchPredicate)
        {
            var list = new List<KeyValuePair<TKey, TValue>>();
            _findAll(Root, searchPredicate, ref list);
            return list;
        }

        /// <summary>
        /// Returns an array of nodes' values.
        /// </summary>
        /// <returns>The array.</returns>
        public virtual KeyValuePair<TKey, TValue>[] ToArray()
        {
            return this.ToList().ToArray();
        }

        /// <summary>
        /// Returns a list of the nodes' value.
        /// </summary>
        public virtual List<KeyValuePair<TKey, TValue>> ToList()
        {
            var list = new List<KeyValuePair<TKey, TValue>>();
            _inOrderTraverse(Root, ref list);
            return list;
        }


        /*********************************************************************/


        /// <summary>
        /// Returns an enumerator that visits node in the order: parent, left child, right child
        /// </summary>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetPreOrderEnumerator()
        {
            return new BinarySearchTreePreOrderEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that visits node in the order: left child, parent, right child
        /// </summary>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetInOrderEnumerator()
        {
            return new BinarySearchTreeInOrderEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that visits node in the order: left child, right child, parent
        /// </summary>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetPostOrderEnumerator()
        {
            return new BinarySearchTreePostOrderEnumerator(this);
        }


        /*********************************************************************/


        /// <summary>
        /// Returns an preorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreePreOrderEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private BSTMapNode<TKey, TValue> current;
            private BinarySearchTreeMap<TKey, TValue> tree;
            internal Queue<BSTMapNode<TKey, TValue>> traverseQueue;

            public BinarySearchTreePreOrderEnumerator(BinarySearchTreeMap<TKey, TValue> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTMapNode<TKey, TValue>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTMapNode<TKey, TValue> node)
            {
                if (node == null)
                    return;
                traverseQueue.Enqueue(node);
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return new KeyValuePair<TKey, TValue>(current.Key, current.Value); }
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
        internal class BinarySearchTreeInOrderEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private BSTMapNode<TKey, TValue> current;
            private BinarySearchTreeMap<TKey, TValue> tree;
            internal Queue<BSTMapNode<TKey, TValue>> traverseQueue;

            public BinarySearchTreeInOrderEnumerator(BinarySearchTreeMap<TKey, TValue> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTMapNode<TKey, TValue>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTMapNode<TKey, TValue> node)
            {
                if (node == null)
                    return;
                visitNode(node.LeftChild);
                traverseQueue.Enqueue(node);
                visitNode(node.RightChild);
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return new KeyValuePair<TKey, TValue>(current.Key, current.Value); }
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
        internal class BinarySearchTreePostOrderEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private BSTMapNode<TKey, TValue> current;
            private BinarySearchTreeMap<TKey, TValue> tree;
            internal Queue<BSTMapNode<TKey, TValue>> traverseQueue;

            public BinarySearchTreePostOrderEnumerator(BinarySearchTreeMap<TKey, TValue> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTMapNode<TKey, TValue>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTMapNode<TKey, TValue> node)
            {
                if (node == null)
                    return;
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
                traverseQueue.Enqueue(node);
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return new KeyValuePair<TKey, TValue>(current.Key, current.Value); }
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
