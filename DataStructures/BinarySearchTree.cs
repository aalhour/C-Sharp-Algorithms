using System;
using System.Collections;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
	/// <summary>
	/// The binary search tree node.
	/// </summary>
	public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
	{
		private T _value { get; set; }
		private int _subtreeSize { get; set; } // a.k.a Rank of node.
		private BSTNode<T> _parent { get; set; }
		private BSTNode<T> _left { get; set; }
		private BSTNode<T> _right { get; set; }


		public BSTNode() : this(default(T), 0, null, null, null) { }
		public BSTNode(T value) : this(value, 0, null, null, null) { }
		public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
		{
			Value = value;
			SubtreeSize = 0;
			Parent = parent;
			Left = left;
			Right = right;
		}

		public virtual T Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		// Rank of node
		public virtual int SubtreeSize
		{
			get { return this._subtreeSize; }
			set { this._subtreeSize = value; }
		}

		public virtual BSTNode<T> Parent
		{
			get { return this._parent; }
			set { this._parent = value; }
		}

		public virtual BSTNode<T> Left
		{
			get { return this._left; }
			set { this._left = value; }
		}

		public virtual BSTNode<T> Right
		{
			get { return this._right; }
			set { this._right = value; }
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
			get { return (this.HasLeftChild); } 
		}

		/// <summary>
		/// Checks whether this node has right child.
		/// </summary>
		public virtual bool HasRightChild 
		{ 
			get { return (this.HasRightChild ); } 
		}

		/// <summary>
		/// Checks whether this node is the left child of it's parent.
		/// </summary>
		public virtual bool IsLeftChild 
		{ 
			get { return (this.Parent != null && this.Parent.Left == this); } 
		}

		/// <summary>
		/// Checks whether this node is the left child of it's parent.
		/// </summary>
		public virtual bool IsRightChild 
		{ 
			get { return (this.Parent != null && this.Parent.Right == this); } 
		}

		/// <summary>
		/// Checks whether this node is a leaf node.
		/// </summary>
		public virtual bool IsLeafNode
		{
			get { return (this.ChildrenCount == 0); }
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
        private BSTNode<T> _root { get; set; }
        private int _count { get; set; }

		public virtual BSTNode<T> Root
		{
			get { return this._root; }
			set { this._root = value; }
		}

		public BinarySearchTree()
        {
            _root = null;
            _count = 0;
        }

		/// <summary>
        /// Calculates the tree height from a specific node, recursively.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Height of node's longest subtree</returns>
        private int _getTreeHeight(BSTNode<T> node)
        {
            if (node == null)
                return 0;
			else if (node.HasChildren == false)
                return 0 ;

			if (node.ChildrenCount == 2) // it has both a right child and a left child
            {
                if (node.Left.SubtreeSize > node.Right.SubtreeSize)
                    node = node.Left;
                else
                    node = node.Right;
            }
			else if (node.HasLeftChild)
            {
                node = node.Left;
            }
			else if (node.HasRightChild)
            {
                node = node.Right;
            }

            return (1 + _getTreeHeight(node));
        }

		/// <summary>
        /// Returns the Subtrees size for a tree node if node exists; otherwise 0 (left and right nodes of leafs).
        /// This is used in the recursive function UpdateSubtreeSize.
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name="node">BST Node.</param>
        private int _subtreeSize(BSTNode<T> node)
        {
            if (node == null)
                return 0;
            else
                return node.SubtreeSize;
        }

		/// <summary>
        /// Updates the Subtree Size of a tree node.
        /// Used in recusively calculating the Subtrees Sizes of nodes.
        /// </summary>
        /// <param name="node">BST Node.</param>
        private void _updateSubtreeSize(BSTNode<T> node)
        {
            if (node == null)
                return;

            node.SubtreeSize = _subtreeSize(node.Left) + _subtreeSize(node.Right) + 1;
            node = node.Parent;
            _updateSubtreeSize(node);
        }

		/// <summary>
        /// Finds a node inside another node's subtrees, given it's value.
        /// </summary>
        /// <param name="currentNode">Node to start search from.</param>
        /// <param name="item">Search value</param>
        /// <returns>Node if found; otherwise null</returns>
        private BSTNode<T> _findNode(BSTNode<T> currentNode, T item)
        {
            if (currentNode == null)
                return null;

            if (item.IsEqualTo(currentNode.Value))
            {
                return currentNode;
            }
			else if (currentNode.HasLeftChild && item.IsLessThan(currentNode.Value))
            {
                currentNode = currentNode.Left;
                return _findNode(currentNode, item);
            }
			else if (currentNode.HasRightChild && item.IsGreaterThan(currentNode.Value))
            {
                currentNode = currentNode.Right;
                return _findNode(currentNode, item);
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
        private BSTNode<T> _findMinNode(BSTNode<T> node)
        {
            var currentNode = node;

			while (currentNode.HasLeftChild)
                currentNode = currentNode.Left;

            return currentNode;
        }

		/// <summary>
        /// Returns the max-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The maximum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
        private BSTNode<T> _findMaxNode(BSTNode<T> node)
        {
            var currentNode = node;

			while (currentNode.HasRightChild)
                currentNode = currentNode.Right;

            return currentNode;
        }

		/// <summary>
        /// A recursive private method. Used in the public FindAll(predicate) functions.
        /// Implements in-order traversal to find all the matching elements in a subtree.
        /// </summary>
        /// <param name="match"></param>
        private void _findAll(BSTNode<T> currentNode, Predicate<T> match, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _findAll(currentNode.Left, match, ref list);

            if (match(currentNode.Value)) // match
            {
                list.Add(currentNode.Value);
            }

            // call the right child
            _findAll(currentNode.Right, match, ref list);
        }

		/// <summary>
        /// Replaces the node's value from it's parent node object with the newValue.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <param name="node">BST node.</param>
        /// <param name="newNode">New value.</param>
        private void _replaceNodeInParent(BSTNode<T> node, BSTNode<T> newNode = null)
        {
            if (node.Parent != null)
            {
				if (node.IsLeftChild)
                    node.Parent.Left = newNode;
                else
                    node.Parent.Right = newNode;
            }

            if (newNode != null)
                newNode.Parent = node.Parent;
        }

		/// <summary>
        /// /// A private method used in the public Remove function.
        /// Removes a given tree node from the tree.
        /// Handles nodes with sub-trees.
        /// </summary>
        /// <param name="node">Tree node to delete.</param>
        /// <param name="node">Tree node to delete.</param>
        private void _remove(BSTNode<T> node, T item)
        {
            var parent = node.Parent;

			if (node.ChildrenCount == 2) // if both children are present
            {
                var successor = node.Right;
                node.Value = successor.Value;
                _remove(successor, successor.Value);
            }
            else if (node.HasLeftChild) //if the node has only a *left* child
            {
                _replaceNodeInParent(node, node.Left);
                _updateSubtreeSize(parent);
                _count--;

            }
            else if (node.HasRightChild) //if the node has only a *right* child
            {
                _replaceNodeInParent(node, node.Right);
                _updateSubtreeSize(parent);
                _count--;
            }
            else //this node has no children
            {
                _replaceNodeInParent(node, null);
                _updateSubtreeSize(parent);
                _count--;
            }
        }

		/// <summary>
        /// In-order traversal of the subtrees of a node. Returns every node it vists.
        /// </summary>
        /// <param name="currentNode">Node to traverse the tree from.</param>
        private void _inOrderTraverse(BSTNode<T> currentNode, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.Left, ref list);

            // visit node
            list.Add(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.Right, ref list);
        }

		/// <summary>
        /// In-order traversal of the subtrees of a node, and applies an action to the value of every visited node.
        /// </summary>
        /// <param name="currentNode">Node to traverse the tree from.</param>
        /// <param name="action">Action to apply to every node's value.</param>
        private void _inOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.Left, action);

            // visit node
            action(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.Right, action);
        }

		/// <summary>
        /// Implements pre-order traversal of the subtrees of a given node. Applies an action to every visited node (value).
        /// </summary>
        /// <param name="currentNode">Node to traverse from.</param>
        /// <param name="action">Action.</param>
        private void _preOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // visit node
            action(currentNode.Value);

            // call the left child
            _preOrderTraverse(currentNode.Left, action);

            // call the right child
            _preOrderTraverse(currentNode.Right, action);
        }

		/// <summary>
        /// Implements post-order traversal of the subtrees of a given node. Applies an action to every visited node (value).
        /// </summary>
        /// <param name="currentNode">Node to traverse from.</param>
        /// <param name="action">Action.</param>
        private void _postOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // call the left child
            _postOrderTraverse(currentNode.Left, action);

            // call the right child
            _postOrderTraverse(currentNode.Right, action);

            // visit node
            action(currentNode.Value);
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
        /// Returns the height of the tree.
        /// </summary>
        /// <returns>Hight</returns>
        public int Height()
        {
            if (IsEmpty())
                return 0;

            var currentNode = _root;
            return _getTreeHeight(currentNode);
        }

		/// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="item">Item to insert</param>
        public void Insert(T item)
        {
            if (IsEmpty())
            {
                _root = new BSTNode<T>() { Value = item, SubtreeSize = 1 };
                _count++;
            }
            else
            {
                var currentNode = _root;
                var newNode = new BSTNode<T>(item);

                // 
                // Get the currentNode to refer to the appropriate node.
                while (true)
                {
                    if (item.IsLessThan(currentNode.Value))
                    {
						if (currentNode.HasLeftChild == false)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Left = newNode;
                            _count++;
                            break;
                        }

                        currentNode = currentNode.Left;
                    }
                    else
                    {
						if (currentNode.HasRightChild == false)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Right = newNode;
                            _count++;
                            break;
                        }

                        currentNode = currentNode.Right;
                    }
                }//end-while


                //
                // Update the subtrees-sizes
                var node = newNode.Parent;
                _updateSubtreeSize(node);

            }//end-else
        }

        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
        public void Remove(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var currentNode = _root;

            while (currentNode != null)
            {
                if (item.IsEqualTo(currentNode.Value))
                {
                    break;
                }
                else if (item.IsLessThan(currentNode.Value))
                {
                    currentNode = currentNode.Left;
                }
                else if (item.IsGreaterThan(currentNode.Value))
                {
                    currentNode = currentNode.Right;
                }
            }

            //
            // If the element was found, remove it.
            if (currentNode != null)
            {
                _remove(currentNode, item);
            }
            else
            {
                throw new Exception("Item was not found.");
            }
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            BSTNode<T> parent = null;
            var currentNode = _root;

            //
            // Keep going left
            currentNode = _findMinNode(currentNode);

            //
            // Remove the node
            if (currentNode.HasRightChild)
            {
                parent = currentNode.Parent;
                var right = currentNode.Right;

                right.Parent = parent;
                parent.Left = right;
                _count--;
            }
            else
            {
                parent = currentNode.Parent;
                parent.Left = null;
                _count--;
            }

            //
            // Update the subtrees-sizes
            _updateSubtreeSize(parent);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public void RemoveMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            BSTNode<T> parent = null;
            var currentNode = _root;

            //
            // Keep going right
            currentNode = _findMaxNode(currentNode);

            //
            // Remove the node
            if (currentNode.HasLeftChild)
            {
                parent = currentNode.Parent;
                var left = currentNode.Left;

                left.Parent = parent;
                parent.Right = left;
                _count--;
            }
            else
            {
                parent = currentNode.Parent;
                parent.Right = null;
                _count--;
            }

            //
            // Update the subtrees-sizes
            _updateSubtreeSize(parent);
        }

		/// <summary>
		/// Clears all elements from tree.
		/// </summary>
		public void Clear()
		{
			_root = null;
			_count = 0;
		}

        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public T FindMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var currentNode = _root;
            return _findMinNode(currentNode).Value;
        }

        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public T FindMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var currentNode = _root;
            return _findMaxNode(currentNode).Value;
        }

        /// <summary>
        /// Find the item in the tree. Throws an exception if not found.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Item.</returns>
        public T Find(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            var currentNode = _root;
            var node = _findNode(currentNode, item);

            if (node != null)
                return node.Value;
            else
                throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Returns the rank of the specified element
        /// </summary>
        /// <param name="item">Tree element</param>
        /// <returns>Rank(item) if found; otherwise throws an exception.</returns>
        public int Rank(T item)
        {
            var currentNode = _root;
            var node = _findNode(currentNode, item);

            if (node == null)
                throw new Exception("Item was not found.");
            else
                return node.SubtreeSize;
        }

        /// <summary>
        /// Given a predicate function, find all the elements that match it.
        /// </summary>
        /// <param name="searchPredicate">The search predicate</param>
        /// <returns>ArrayList<T> of elements.</returns>
        public List<T> FindAll(Predicate<T> searchPredicate)
        {
            var currentNode = _root;
            var list = new List<T>();
            _findAll(currentNode, searchPredicate, ref list);

            return list;
        }

        /// <summary>
        /// Traverses the tree and applies the action to every node.
        /// </summary>
        /// <param name="action">Action to apply to every node's value.</param>
        public void Traverse(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("Null actions are not allowed.");

            var currentNode = _root;
            _inOrderTraverse(currentNode, action);
        }

        /// <summary>
        /// Sort the elements in this tree, using in-order traversal, and returns them.
        /// </summary>
        public List<T> BSTSort()
        {
            var currentNode = _root;
            var list = new List<T>();

            _inOrderTraverse(currentNode, ref list);

            return list;
        }

		/// <summary>
		/// Returns an array of nodes' values.
		/// </summary>
		/// <returns>The array.</returns>
		public T[] ToArray()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns a list of the nodes' value.
		/// </summary>
		public List<T> ToList()
		{
			throw new NotImplementedException ();
		}


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
					visitNode(node.Left);
					visitNode(node.Right);
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
					visitNode(node.Left);
					traverseQueue.Enqueue(node);
					visitNode(node.Right);
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
					visitNode(node.Left);
					visitNode(node.Right);
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
